using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class for_CC : MonoBehaviour
{   
    public CharacterController obj;
    public bool can_show = false;
    public bool grab = false;
    public Transform move_obj;
    public Transform player;
    public float distance_1;
    public float distance_2;

    public GameObject moving_obj;

    private Vector3 keep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && grab)
            {
                grab = false;
                
            }
        /*if(grab)
        {
            //Debug.Log(Vector3.Distance(move_obj.position, transform.position));
            if(player.position.z > move_obj.position.z + 0.741 && player.position.x > move_obj.position.x + 67)
            {
                grab = false;
            }
        }*/
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if(col.gameObject.name == "col")
        {
            Destroy(col.gameObject);
            can_show = true;
        }

        if(grab == false && col.gameObject.tag == "move_0" && Input.GetMouseButton(0) && obj.isGrounded )
        {
            moving_obj = col.gameObject.transform.parent.gameObject;
            move_obj = col.gameObject.transform.parent;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            grab = true;
        }

        if(grab == false && col.gameObject.tag == "move_180" && Input.GetMouseButton(0) && obj.isGrounded)
        {
            moving_obj = col.gameObject.transform.parent.gameObject;
            move_obj = col.gameObject.transform.parent;
            player.rotation = Quaternion.Euler(0f, 180f, 0f);
            grab = true;
        }

        if(grab == false && col.gameObject.tag == "move_270" && Input.GetMouseButton(0) && obj.isGrounded )
        {
            moving_obj = col.gameObject.transform.parent.gameObject;
            move_obj = col.gameObject.transform.parent;
            player.rotation = Quaternion.Euler(0f, 270f, 0f);
            grab = true;
        }

        if(grab == false && col.gameObject.tag == "move_90" && Input.GetMouseButton(0) && obj.isGrounded )
        {
            moving_obj = col.gameObject.transform.parent.gameObject;
            move_obj = col.gameObject.transform.parent;
            player.rotation = Quaternion.Euler(0f, 90f, 0f);
            grab = true;
        }

    }
}
