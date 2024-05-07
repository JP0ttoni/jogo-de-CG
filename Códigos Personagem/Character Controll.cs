/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    private CharacterController character;
    private Animator animator;
    private Vector3 inputs;

    public GameObject Camera_1;
    public GameObject Camera_2;
    private float velocidade = 2f;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera_1.activeSelf)
        {
        inputs.Set(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        }
        if (Camera_2.activeSelf)
        {
        inputs.Set(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        }

        character.Move(inputs * Time.deltaTime * velocidade);
        character.Move(Vector3.down * Time.deltaTime);
        if(inputs != Vector3.zero)
        {
            animator.SetBool("Andando", true);
            transform.forward = Vector3.Slerp(transform.forward, inputs, Time.deltaTime * 10);

        }
        else
        {
            animator.SetBool("Andando", false);
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class CharacterControll : MonoBehaviour
{
    private bool spawn_1 = true;
    public  GameObject obj;
    public for_CC codigo_CC;

    private CharacterController moving_obj;

    public GameObject message;

    private bool can_show = false;

    private CharacterController character;
    private Animator animator;
    private Vector3 inputs;

    public GameObject Camera_1;
    public GameObject Camera_2;
    public float velocidade = 2f;
    private bool podeMover = true;
    private bool press_t = false;
    private bool podeClicarB = true;
    private bool isjump = false;
    public float jump = 10f;

    public float gravity = 4;
    private Vector3 vect;

    public bool grounded;

    void Start()
    {
        character = obj.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        can_show = codigo_CC.can_show;

        if (Input.GetKeyDown(KeyCode.B) && podeClicarB)
        {
            StartCoroutine(AtrasoParaClicarB(3.0f));
            if (podeMover)
            {
                StartCoroutine(AtrasoParaMover(0.8f));
                
            }
        }

        if(Input.GetKeyDown(KeyCode.T) && can_show == true && press_t == false && character.isGrounded)
        {
            Debug.Log("pressionado");
            message.SetActive(true);
            press_t = true;
            podeMover = false;
        }else if(Input.GetKeyDown(KeyCode.T) && can_show == true && press_t == true && character.isGrounded){
            message.SetActive(false);
            press_t = false;
            podeMover = true;
        }

        if (podeMover)
        {
            if (Camera_1.activeSelf)
            {
                inputs.Set(-Input.GetAxis("Vertical") * velocidade, inputs.y, Input.GetAxis("Horizontal") * velocidade);
            }
            else if (Camera_2.activeSelf)
            {
                inputs.Set(Input.GetAxis("Vertical") * velocidade, inputs.y, -Input.GetAxis("Horizontal") * velocidade);
            }

            if(character.isGrounded && !codigo_CC.grab)
            {
                //Debug.Log("no chão");
                isjump = false;
                if(Input.GetKeyDown("space"))
                {
                    //Debug.Log("pressionado");
                    isjump = true;
                    inputs.y = jump;
                }

            } else{
                //Debug.Log("não esta no chão");
            }

            if(isjump)
            {
                gravity = 15;
            } else{
                gravity = 7.5f;
            }

            

            inputs.y -= gravity *Time.deltaTime;
            if(inputs.y < -gravity)
            {
                inputs.y = -gravity;
            }

            if(codigo_CC.grab)
            {
                moving_obj = codigo_CC.moving_obj.GetComponent<CharacterController>();
                moving_obj.Move(inputs * Time.deltaTime);
            }
            character.Move(inputs * Time.deltaTime);
            //character.Move(Vector3.down* gravity * Time.deltaTime);
            //Debug.Log(inputs);
            vect = inputs;
            vect.y = 0;
            if (inputs.x != 0 || inputs.z != 0)
            {
                animator.SetBool("Andando", true);
                if(!codigo_CC.grab)
                {
                    transform.forward = Vector3.Slerp(transform.forward, vect, Time.deltaTime * 10);
                }
                //Debug.Log(transform.forward);
            }
            else
            {
                animator.SetBool("Andando", false);
            }

            //transform.rotation = Quaternion.Euler(0, transform.forward.y, transform.forward.z);
        }

        if(SceneManager.GetActiveScene().name == "fase_2" && spawn_1 == true)
        {
            character.enabled = false;
            character.transform.position = new Vector3(0,0,0); //spawnpoint
            spawn_1 = false;
            character.enabled = true;
        }
    }

    IEnumerator AtrasoParaMover(float delay)
    {
        podeMover = false;
        yield return new WaitForSeconds(delay);
        podeMover = true;
    }

    IEnumerator AtrasoParaClicarB(float delay)
    {
        podeClicarB = false; // Impede que o jogador troque a câmera temporariamente
        yield return new WaitForSeconds(delay); // Aguarda o tempo especificado
        podeClicarB = true; // Permite que o jogador troque a câmera novamente
    }
}



