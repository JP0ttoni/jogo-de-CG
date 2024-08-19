using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
   public GameObject Camera_1;
    public GameObject Camera_2;
    public GameObject CameraToRun;
    public static int Manager = SwitchCamera.Manager;

    void Start()
    {
        CameraToRun.SetActive(false);
    }


    public void Update()
    {
    }

    public void ChangeCameraToRun()
    {
        GetComponent<Animator>().SetTrigger("ChangeToRun");
    }


    public void ManageCameraToRun()
    {
        Cam_311();

    }

    void Cam_311()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(false);
        CameraToRun.SetActive(true);
    }

    public static Run Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
