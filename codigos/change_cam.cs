using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_cam : MonoBehaviour
{
    public static bool inicia_fight = false;
    // Start is called before the first frame update
    void Start()
    {
        var cameras = Camera.allCameras;
            foreach(Camera cam_scene in cameras)
            {
                if(cam_scene.name == "Camera Boss Fase3")
                {
                    cam_scene.targetDisplay = 1;
                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "CC")
        {
            StartCoroutine(cd());
            var cameras = Camera.allCameras;
            foreach(Camera cam_scene in cameras)
            {
                if(cam_scene.name == "Camera Boss Fase3")
                {
                    cam_scene.targetDisplay = 0;
                } else{
                    cam_scene.targetDisplay = 1;
                }
            }
        }
    }

    private IEnumerator cd()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("iniciou");
        inicia_fight = true;
    }

}
