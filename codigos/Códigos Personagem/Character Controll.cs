using System.Collections;
using System.Collections.Generic;
using TMPro;



//using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterControll : MonoBehaviour
{
    private bool spawn_1 = true; 
    public bool trigger_stun = true;
    public  GameObject obj;
    public for_CC codigo_CC;

    private CharacterController moving_obj;

    public GameObject message;

    private bool can_show = false;

    private CharacterController character;
    private Animator animator;
    private Vector3 inputs;

    public Transform playerBody;

    public static bool stun = false;
    private float xRotation = 0f;

    public GameObject Camera_1;
    public GameObject Camera_2;
    public GameObject CameraFirstPerson;
    public GameObject CameraRun;

    public Transform _transform;
    public Transform cameraTransform;
    public float velocidade = 2f;
    public static bool podeMover = true;
    private bool press_t = false;
    private bool podeClicarB = true;
    private bool podeClicarF = true;
    public static bool isjump = false;
    public float jump = 10f;

    public float gravity = 4;
    private Vector3 vect;

    public bool grounded;
    public static bool move_mumia = false;

    private bool estavaCaindo;
    private bool aterrisando = false;

    private float initialYPosition;

    private bool isRolling = false;
    private float lastRollTime = 0f;
    public float rollCooldown = 3f; // Cooldown de 3 segundos para o rolamento
    public float rollSpeed = 3.7f;
    public float rollDuration = 0.5f;
    private float rollTimer = 0f;
    private bool canRoll = true;
    private bool canJump = true;
    public float rotationSpeed = 100f;
    private Quaternion originalRotation;
    public LayerMask specialGroundLayer;
    public static bool possoclicarF = false;

    //PARTE DO RUN
    public float for_speed =5f;
    private int current_pos = 0;

    public Transform Center_pos;
    public Transform left_pos;
    public Transform right_pos;
    public RawImage hudImageAD; // Referência à imagem no HUD
    public RawImage hudImageCTRL; // Referência à imagem no HUD
    public RawImage hudImageSPACE; // Referência à imagem no HUD
    public TMP_Text textoAD; // Referência ao texto AD
    public TMP_Text textoCTRL; // Referência ao texto CTRL
    public TMP_Text textoSPACE; // Referência ao texto SPACE
    private bool isCoroutineRunning = false;
    public Transform spawn_tiro;
    public GameObject tiro_obj;
    public int vel_bala;

    
    void Start()
    {
        podeMover = true;
        character = obj.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        estavaCaindo = false;
        initialYPosition = transform.position.y;
        originalRotation = CameraFirstPerson.transform.localRotation;
        current_pos = 0;
    }

    void Update()
    {
        grounded = character.isGrounded;
        can_show = codigo_CC.can_show;
        possoclicarF = CanClickF();

        animator.SetBool("push", for_CC.grab);

        //MECÂNICA CÂMERA PRIMEIRA PESSOA
        if (Input.GetKeyDown(KeyCode.F) && podeClicarF && possoclicarF)
        {
            inputs = Vector3.zero;
            StartCoroutine(AtrasoParaClicarF(3.0f));
            podeClicarB = !podeClicarB;
            if (podeMover)
            {
                StartCoroutine(AtrasoParaMover(2f));    
            }
        
        }
        //MECÂNICA INVERTER CÂMERA DE LADO
        if (Input.GetKeyDown(KeyCode.B) && podeClicarB)
        {
            inputs = Vector3.zero;
            StartCoroutine(AtrasoParaClicarB(3.0f));
            if (podeMover)
            {
                StartCoroutine(AtrasoParaMover(2f));
                
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

        if(Input.GetMouseButton(0) && !stun && podeMover && character.isGrounded && !isRolling && (SceneManager.GetActiveScene().name == "Fase_3"||SceneManager.GetActiveScene().name == "Fase_4" || SceneManager.GetActiveScene().name == "Boss fight"))
        {
            podeMover = false;
            inputs = new Vector3(0,0,0);
            animator.SetTrigger("shoot");
            StartCoroutine(cd_shoot());
        }

        if(stun)
        {
            podeMover = false;
            inputs = new Vector3(0,0,0);
            if(trigger_stun){
            StartCoroutine(stunado());
            }
        }

        if (podeMover)
        {
            if (Camera_1.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                character.transform.Translate(Vector3.forward * Time.deltaTime);
                inputs.Set(-Input.GetAxis("Vertical") * velocidade, inputs.y, Input.GetAxis("Horizontal") * velocidade);
                CameraFirstPerson.transform.localRotation = originalRotation;
                
            }
            else if (Camera_2.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                character.transform.Translate(Vector3.forward * Time.deltaTime);
                inputs.Set(Input.GetAxis("Vertical") * velocidade, inputs.y, -Input.GetAxis("Horizontal") * velocidade);
                CameraFirstPerson.transform.localRotation = originalRotation;
            }
            if (CameraFirstPerson.activeSelf)
            {        

                //MOVIMENTAÇÃO COM O MOUSE:
                Cursor.lockState = CursorLockMode.Locked;   
                float mouseX = Input.GetAxis("Mouse X") * 300f * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * 300f * Time.deltaTime;

                float yRotation = CameraFirstPerson.transform.localEulerAngles.y + mouseX;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                CameraFirstPerson.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            }
            else if (CameraRun.activeSelf)
            {
                move_mumia = true;
                if (!isCoroutineRunning)
                {
                    StartCoroutine(ShowHudElementsForLimitedTime(15f, 3f)); // Mostrar por 15 segundos e sumir em 2 segundos
                }
                StartCoroutine(AtrasoParaClicarB(70f));
                
                // Movimento para frente
                Vector3 moveDirection = Vector3.forward * for_speed * Time.deltaTime;

                // Atualiza a posição do jogador baseado na faixa atual
                Vector3 targetPosition = transform.position;
                if (current_pos == 0) // Centro
                {
                    targetPosition.x = -5.9f;
                }
                else if (current_pos == 1) // Esquerda
                {
                    targetPosition.x = -8.4f; // Ajuste a posição de acordo com sua necessidade
                }
                else if (current_pos == 2) // Direita
                {
                    targetPosition.x = -3.4f; // Ajuste a posição de acordo com sua necessidade
                }

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Calcula a direção de movimento lateral
                Vector3 lateralMoveDirection = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 6.5f) - transform.position;
                
                // Combina movimento para frente e lateral
                Vector3 combinedMoveDirection = moveDirection + lateralMoveDirection;

                // Move o CharacterController
                character.Move(combinedMoveDirection);

                // Reset rotation to face forward
                transform.rotation = Quaternion.Euler(0, 0, 0);

                // Verifica entrada do usuário para mudar de faixa
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (current_pos == 0)
                    {
                        current_pos = 1; // Muda para a esquerda
                    }
                    else if (current_pos == 2)
                    {
                        current_pos = 0; // Muda para o centro
                    }
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (current_pos == 0)
                    {
                        current_pos = 2; // Muda para a direita
                    }
                    else if (current_pos == 1)
                    {
                        current_pos = 0; // Muda para o centro
                    }
                }
            }

        }

        if(character.isGrounded && !for_CC.grab)
        {
            if(aterrisando)
            {
                animator.SetBool("BatendonoChao", false);
                aterrisando = false;
                StartCoroutine(AtrasoParaRolar(0.5f));
            }
            //Debug.Log("no chão");
            isjump = false;
            if(Input.GetKeyDown("space") && canJump && !stun)
            {
                //Debug.Log("pressionado");  
                isjump = true;
                inputs.y = jump;
            }

        }


        // animação pulando parado
        if(!(inputs.x != 0) && !(inputs.z != 0) && !CameraRun.activeSelf){ //essa animação acontece apenas para o pulo parado
            animator.SetBool("Pulando", !character.isGrounded);
        }
        else{
            animator.SetBool("Pulando", false);
        }

        //animação pulo correndo
        if((inputs.x != 0) || (inputs.z != 0) && !CameraRun.activeSelf){ //essa animação acontece apenas para o pulo correndo
            animator.SetBool("Pulando e correndo", !character.isGrounded);
        }
        else{
            animator.SetBool("Pulando e correndo", false);
        }


        if(isjump)
        {
            gravity = 15;
        } else{
            gravity = 7.5f;
        }

        //animação descendo
        if (!character.isGrounded && inputs.y < 0 && !CameraRun.activeSelf)
        {
            // Se o personagem não está no chão e está se movendo para baixo, então está caindo
            if (!estavaCaindo && !CameraRun.activeSelf) // Se não estava caindo antes
            {
                animator.SetBool("Descendo", true); // Ativa a animação "descendo"
                estavaCaindo = true;
            }
        }
        else
        {
            if (character.isGrounded && estavaCaindo && !CameraRun.activeSelf)
            {
                animator.SetBool("BatendonoChao", true);
                estavaCaindo = false;
                aterrisando = true;

                // Se está no chão ou não está mais se movendo para baixo, não está mais caindo

                animator.SetBool("Descendo", false); // Desativa a animação "descendo"
                estavaCaindo = false;
            }
        }

        inputs.y -= gravity *Time.deltaTime;
        if(inputs.y < -gravity)
        {
            inputs.y = -gravity;
        }


        //ROLAMENTO COM CTRL
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isRolling && Time.time >= lastRollTime + rollCooldown && character.isGrounded && canRoll)
        {
            StartCoroutine(HandleRoll());
        }
        if (isRolling)
        {
            rollTimer += Time.deltaTime;
            // Calcula a direção de rolamento (usando a direção em que o personagem está virado)
            Vector3 rollDirection = transform.forward;

            // Aplica o movimento de rolamento
            character.Move(rollDirection * rollSpeed * Time.deltaTime);
            if (rollTimer >= rollDuration)
            {
                isRolling = false;
                animator.SetBool("Rolando", false);
                Physics.IgnoreLayerCollision(character.gameObject.layer, LayerMask.NameToLayer("IgnoreCollision"), false); // Reativar colisões ao terminar o rolamento
            }
        }



        if(for_CC.grab)
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
            if(!for_CC.grab)
            {
                transform.forward = Vector3.Slerp(transform.forward, vect, Time.deltaTime * 10);
            }
            //Debug.Log(transform.forward);
        }
        else
        {
            if(CameraRun.activeSelf)
            {
                animator.SetBool("Andando", true);
            }
            else
                animator.SetBool("Andando", false);
        }

        //transform.rotation = Quaternion.Euler(0, transform.forward.y, transform.forward.z);
        

        if(SceneManager.GetActiveScene().name == "fase_2" && spawn_1 == true)
        {
            character.enabled = false;
            character.transform.position = new Vector3(0,0,0); //spawnpoint
            spawn_1 = false;
            character.enabled = true;
        }

        if (CameraRun.activeSelf)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetBool("Andando", false);
                    animator.SetBool("Pulando", true);
                    animator.SetBool("Parado", true);
                    
                }
                if(character.isGrounded)
                {
                    animator.SetBool("Andando", true);
                    animator.SetBool("Parado", false);
                    animator.SetBool("Pulando", false);
                }
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

    IEnumerator AtrasoParaClicarF(float delay)
    {
        podeClicarF = false;
        yield return new WaitForSeconds(delay);
        podeClicarF = true;
    }

    IEnumerator AtrasoParaRolar(float delay)
    {
        canRoll = false;
        yield return new WaitForSeconds(delay);
        canRoll = true;
    }

    IEnumerator AtrasoParaPular(float delay)
    {
        canJump = false;
        yield return new WaitForSeconds(delay);
        canJump = true;
    }

    public bool CanClickF()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, specialGroundLayer))
        {
            return true;
        }
        return false;
    }

    IEnumerator ShowHudElementsForLimitedTime(float displayTime, float fadeDuration)
    {
        isCoroutineRunning = true;
        float elapsedTime = 2f;

        // Mostrar a imagem e o texto
        Color imageColorAD = hudImageAD.color;
        Color imageColorCTRL = hudImageCTRL.color;
        Color imageColorSPACE = hudImageSPACE.color;
        Color textColorAD = textoAD.color;
        Color textColorCTRL = textoCTRL.color;
        Color textColorSPACE = textoSPACE.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            imageColorAD.a = alpha;
            imageColorCTRL.a = alpha;
            imageColorSPACE.a = alpha;
            textColorAD.a = alpha;
            textColorCTRL.a = alpha;
            textColorSPACE.a = alpha;
            hudImageAD.color = imageColorAD;
            hudImageCTRL.color = imageColorCTRL;
            hudImageSPACE.color = imageColorSPACE;
            textoAD.color = textColorAD;
            textoCTRL.color = textColorCTRL;
            textoSPACE.color = textColorSPACE;
            yield return null;
        }
        imageColorAD.a = 1f;
        imageColorCTRL.a = 1f;
        imageColorSPACE.a = 1f;
        textColorAD.a = 1f;
        textColorCTRL.a = 1f;
        textColorSPACE.a = 1f;
        hudImageAD.color = imageColorAD;
        hudImageCTRL.color = imageColorCTRL;
        hudImageSPACE.color = imageColorSPACE;
        textoAD.color = textColorAD;
        textoCTRL.color = textColorCTRL;
        textoSPACE.color = textColorSPACE;
        hudImageAD.enabled = true;
        hudImageCTRL.enabled = true;
        hudImageSPACE.enabled = true;
        textoAD.enabled = true;
        textoCTRL.enabled = true;
        textoSPACE.enabled = true;

        // Esperar pelo tempo especificado
        yield return new WaitForSeconds(displayTime);

        // Desaparecer a imagem e o texto lentamente
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            imageColorAD.a = alpha;
            imageColorCTRL.a = alpha;
            imageColorSPACE.a = alpha;
            textColorAD.a = alpha;
            textColorCTRL.a = alpha;
            textColorSPACE.a = alpha;
            hudImageAD.color = imageColorAD;
            hudImageCTRL.color = imageColorCTRL;
            hudImageSPACE.color = imageColorSPACE;
            textoAD.color = textColorAD;
            textoCTRL.color = textColorCTRL;
            textoSPACE.color = textColorSPACE;
            yield return null;
        }

        // Desativar a imagem e o texto e resetar a flag da coroutine
        hudImageAD.enabled = false;
        hudImageCTRL.enabled = false;
        hudImageSPACE.enabled = false;
        textoAD.enabled = false;
        textoCTRL.enabled = false;
        textoSPACE.enabled = false;
    }

    private IEnumerator HandleRoll()
    {
        isRolling = true;
        rollTimer = 0f;
        animator.SetBool("Rolamento", true);
        lastRollTime = Time.time;
        // Ignorar colisões durante o rolamento
        Physics.IgnoreLayerCollision(character.gameObject.layer, LayerMask.NameToLayer("IgnoreCollision"), true);
        yield return new WaitForSeconds(rollDuration);
        isRolling = false;
        animator.SetBool("Rolamento", false);
        // Reativar colisões após o rolamento
        Physics.IgnoreLayerCollision(character.gameObject.layer, LayerMask.NameToLayer("IgnoreCollision"), false);
    } 

    private IEnumerator cd_shoot()
    {
        yield return new WaitForSeconds(0.45f);
        var bala = Instantiate(tiro_obj, spawn_tiro.position, spawn_tiro.rotation);
            bala.GetComponent<Rigidbody>().velocity = spawn_tiro.forward * vel_bala;
        yield return new WaitForSeconds(0.65f);
        podeMover = true;
    }

    private IEnumerator stunado()
    {
        trigger_stun = false;
        for_CC.life--;
        yield return new WaitForSeconds(2f);
        podeMover = true;
        stun = false;
        trigger_stun = true;
    }

}

