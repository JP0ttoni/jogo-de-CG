using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class for_CC : MonoBehaviour
{   
    public CharacterController obj;
    public bool can_show = false;
    public static bool grab = false;
    public Transform move_obj;
    public Transform player;
    public float distance_1;
    public float distance_2;
    private bool take_damage = true;

    public Animator life_anim;

    public GameObject moving_obj, bt_password;

    private Vector3 keep;
    private string analise;
    public Renderer characterRenderer; // O Renderer do personagem
    public Renderer characterRendererHead;
    public Renderer characterRendererHat;
    public float blinkDuration = 0.1f; // Duração de cada piscar
    public float blinkDelay = 0.1f; // Tempo entre cada piscar

    private CharacterController characterCollider;
    public ScreenFlash screenFlash;
    public static int life = 6;
    public static Transform plyr_pos;

    public static bool isjumping;
    // Start is called before the first frame update
    void Start()
    {
        characterCollider = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isjumping = characterCollider.isGrounded;
        plyr_pos = transform;
        if(life <= 0)
        {
            life = 6;
            AtivarRun.ativarUmaVez = false;
            CharacterControll.move_mumia = false;
            ControladorDasChaves.chavesDoJogador.Clear();
            if(SceneManager.GetActiveScene().name == "Fase_1")
            {
                SceneManager.LoadScene("game_over");
            } else if(SceneManager.GetActiveScene().name == "Fase_3")
            {
                SceneManager.LoadScene("game_over_3");
            } else if(SceneManager.GetActiveScene().name == "Fase_4")
            {
                SceneManager.LoadScene("game_over_4");
            } else if(SceneManager.GetActiveScene().name == "Boss fight")
            {
                SceneManager.LoadScene("game_over_Boss");
            }
            Destroy(gameObject);
        }
        life_anim.SetInteger("life", life);
        if((Input.GetMouseButtonUp(1) && grab) || (Input.GetAxis("Vertical") != 0 && analise == "side" && grab) || (Input.GetAxis("Horizontal") != 0 && analise == "front" && grab))
            {
                grab = false;
                
            }
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if(col.gameObject.name == "end_to3")
        {
            SceneManager.LoadScene("Fase_3");
        }

        if(col.gameObject.name == "end_toboss")
        {
            SceneManager.LoadScene("Boss fight");
        }

        if(col.gameObject.CompareTag("hitkill") && take_damage)
        {
            life -= 6;
            StartCoroutine(HandleDamage(col.gameObject));
        }

        if((col.gameObject.CompareTag("damage") || col.gameObject.CompareTag("enemy")) && take_damage)
        {
            life -= 2;
            StartCoroutine(HandleDamage(col.gameObject));
        }
        
        if(col.gameObject.name == "col")
        {
            Destroy(col.gameObject);
            can_show = true;
        }

        if(col.gameObject.name == "password")
        {
            bt_password.SetActive(true);
            CharacterControll.podeMover = false;
        }

        if(grab == false && col.gameObject.tag == "move_0" && Input.GetMouseButton(1) && obj.isGrounded )
        {
            moving_obj = col.gameObject.transform.parent.gameObject;
            move_obj = col.gameObject.transform.parent;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            analise = "side";
            grab = true;
        }

        if(grab == false && col.gameObject.tag == "move_180" && Input.GetMouseButton(1) && obj.isGrounded)
        {
            moving_obj = col.gameObject.transform.parent.gameObject;
            move_obj = col.gameObject.transform.parent;
            player.rotation = Quaternion.Euler(0f, 180f, 0f);
            analise = "side";
            grab = true;
        }

        if(grab == false && col.gameObject.tag == "move_270" && Input.GetMouseButton(1) && obj.isGrounded )
        {
            moving_obj = col.gameObject.transform.parent.gameObject;
            move_obj = col.gameObject.transform.parent;
            player.rotation = Quaternion.Euler(0f, 270f, 0f);
            analise = "front";
            grab = true;
        }

        if(grab == false && col.gameObject.tag == "move_90" && Input.GetMouseButton(1) && obj.isGrounded )
        {
            moving_obj = col.gameObject.transform.parent.gameObject;
            move_obj = col.gameObject.transform.parent;
            player.rotation = Quaternion.Euler(0f, 90f, 0f);
            analise = "front";
            grab = true;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("enemy"))
        {
            if(other.gameObject.name == "mini_boss")
            {
                life-=2;
            }
            else{
                life--;
            }
            StartCoroutine(HandleDamage(other.gameObject));
        }
    }

     private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("enemy"))
        {
            if(other.gameObject.name == "mini_boss")
            {
                life-=2;
            }
            else{
                life--;
            }
            StartCoroutine(HandleDamage(other.gameObject));
        }
    }

    private IEnumerator HandleDamage(GameObject damageObject)
        {
            // Desabilita o Collider do objeto de dano
            Collider damageCollider = damageObject.GetComponent<Collider>();
            if (damageCollider != null)
            {
                damageCollider.enabled = false;
            }

            // Desativa o dano temporariamente
            take_damage = false;

            // Animação de piscar do personagem
            StartCoroutine(BlinkCharacter());

            // Tela de flash (se houver)
            if (screenFlash != null)
            {
                screenFlash.FlashScreen();
            }

            // Espera o tempo definido para o efeito de piscar e flash
            yield return new WaitForSeconds(1);

            // Habilita o Collider do objeto de dano novamente
            if (damageCollider != null)
            {
                damageCollider.enabled = true;
            }

            // Ativa o dano novamente
            take_damage = true;
        }

    private IEnumerator BlinkCharacter()
    {
        if (characterRenderer != null)
        {
            Material[] materials = { characterRenderer.material, characterRendererHead.material, characterRendererHat.material };
            foreach (Material material in materials)
            {
                Color originalColor = material.color;

                for (float t = 0; t < 1; t += Time.deltaTime / blinkDuration)
                {
                    float lerpValue = Mathf.PingPong(t / blinkDuration, 1);
                    material.color = Color.Lerp(originalColor, Color.clear, lerpValue);
                    yield return null;
                }
                material.color = originalColor;
            }
        }
    }
}
