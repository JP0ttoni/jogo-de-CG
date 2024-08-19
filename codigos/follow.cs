using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var follow = new Vector3(transform.position.x, for_CC.plyr_pos.position.y, for_CC.plyr_pos.position.z);      
        transform.position = follow;
    }
}
