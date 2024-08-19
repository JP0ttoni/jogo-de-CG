using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ball_boss : MonoBehaviour
{
    public Transform plyr;
    private bool can_fall = false;
     private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(fall());
    }

    // Update is called once per frame
    void Update()
    {
        if(!can_fall){
            var look = new Vector3(for_CC.plyr_pos.position.x, 0, for_CC.plyr_pos.position.z);
            transform.position = look;
        } else{
            rb.velocity = new Vector3(0,-8,0);
        }

    }

    private IEnumerator fall()
    {
        yield return new WaitForSeconds(3f);
        can_fall = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "CC")
        {
            for_CC.life -=2;
        }
        Destroy(gameObject);
    }
}
