using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var motion = new Vector3(0,-4,0);
        cc.Move(motion * Time.deltaTime);
    }
}
