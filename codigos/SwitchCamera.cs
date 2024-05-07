using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;

    private bool podeClicarB = true;

    public int Manager;
    // Start is called before the first frame update

    void Start()
    {
        Cam_1();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && podeClicarB)
        {
            StartCoroutine(AtrasoParaClicarB(3.0f));
            ChangeCamera();
        }
    }
    public void ChangeCamera()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }
    public void ManageCamera()
    {
        if(Manager == 0)
        {
            Cam_2();
            Manager = 1;
        }
        else
        {
            Cam_1();
            Manager = 0;
        }
    }
    void Cam_1()
    {
        Camera_1.SetActive(true);
        Camera_2.SetActive(false);
    }

    // Update is called once per frame
    void Cam_2()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(true);
    }

    IEnumerator AtrasoParaClicarB(float delay)
    {
        podeClicarB = false; // Impede que o jogador troque a câmera temporariamente
        yield return new WaitForSeconds(delay); // Aguarda o tempo especificado
        podeClicarB = true; // Permite que o jogador troque a câmera novamente
    }
}
