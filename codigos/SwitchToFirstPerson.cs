using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchToFirstPerson : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public GameObject CameraFirstPerson;
    private bool podeClicarF = true;
    public int ManagerFirst = 0;

    public static bool podeClicarB = SwitchCamera.podeClicarB;
    public static int Manager = SwitchCamera.Manager;

    public LayerMask specialGroundLayer;

    void Start()
    {
        Cam_11();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && podeClicarF && CharacterControll.possoclicarF)
        {
            podeClicarB = !podeClicarB;
            StartCoroutine(AtrasoParaClicarF(3.0f));
            ChangeCameraFirstPerson();
        }
    }

    public void ChangeCameraFirstPerson()
    {
        GetComponent<Animator>().SetTrigger("ChangeFirstPerson");
    }


    public void ManageCameraFirstPerson()
    {
        if(ManagerFirst == 0)
        {
            Cam_31();
            ManagerFirst = 1;
        }
        else
        {
            Cam_11();
            ManagerFirst = 0;
            Manager = 0;
        }

    }
    void Cam_11()
    {
        Camera_1.SetActive(true);
        Camera_2.SetActive(false);
        CameraFirstPerson.SetActive(false);
    }

    void Cam_31()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(false);
        CameraFirstPerson.SetActive(true);
    }
    IEnumerator AtrasoParaClicarF(float delay)
    {
        podeClicarF = false; // Impede que o jogador troque a câmera temporariamente
        yield return new WaitForSeconds(delay); // Aguarda o tempo especificado
        podeClicarF = true; // Permite que o jogador troque a câmera novamente
    }


}
