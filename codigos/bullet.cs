using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<InimigoScript>().enemy_life -= 1;
        }

        if(other.gameObject.tag == "boss1")
        {
            boss_fight1.life_boss --;
        }

        if(other.gameObject.tag == "morgana")
        {
            morg.life_morg --;
        }
        
        Destroy(gameObject);
        
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
