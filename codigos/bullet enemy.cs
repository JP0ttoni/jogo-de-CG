using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletenemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dead());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "CC")
        {
            for_CC.life -=6;
        }
        Destroy(gameObject);
    }

    private IEnumerator dead()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
