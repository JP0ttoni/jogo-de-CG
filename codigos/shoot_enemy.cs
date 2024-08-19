using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_enemy : MonoBehaviour
{
    public GameObject bullet_obj;
    private bool shoot_bullet = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot_bullet)
        {
            var bala = Instantiate(bullet_obj, transform.position, transform.rotation);
            bala.GetComponent<Rigidbody>().velocity = transform.forward * 8;
            StartCoroutine(shoot());
        }
        
    }

    private IEnumerator shoot()
    {
        shoot_bullet = false;
        yield return new WaitForSeconds(2);
        shoot_bullet = true;
    }
}
