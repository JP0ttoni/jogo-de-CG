using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.name == "CC")
        {
            Debug.Log("stun");
            CharacterControll.stun = true;
        }
        Destroy(gameObject);
    }
}
