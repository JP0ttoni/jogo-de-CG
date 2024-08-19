using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mumia_follow : MonoBehaviour
{
    public Transform plyr;
    private Rigidbody controller;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CharacterControll.move_mumia)
        {
            anim.SetTrigger("walk");

            transform.position = new Vector3(plyr.position.x, 5.9f, plyr.position.z - 5.35f);
        }
    }
}
