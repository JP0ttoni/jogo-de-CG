using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trigger : MonoBehaviour
{

    public for_CC codigo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        
        if(codigo.moving_obj.name == transform.parent.gameObject.name && col.gameObject.name == "CC")
        {
            codigo.grab = false;
        }
        
    }
}
