using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class morg : MonoBehaviour
{
    public GameObject plyr;
    private Vector3 pos;
    private float timer = 0;
    private float seconds;
    public GameObject poça_obj;
    public Transform spawn_p;
    public Transform spawn_b;
    public GameObject bullet_obj;
    public float speed = 10;
    private bool trig = false;

    public static int life_morg = 5;
    public Slider lifebar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifebar.value = life_morg;
        if(life_morg <= 0)
        {
            SceneManager.LoadScene("win");
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        seconds = timer % 60;
        pos = new Vector3(plyr.transform.position.x, 0f, plyr.transform.position.z);
        transform.LookAt(pos);
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        if(seconds >= 3)
        {
            timer = 0;
            var bullet = Instantiate(bullet_obj, spawn_b.position, spawn_b.rotation);
            bullet.GetComponent<Rigidbody>().velocity = spawn_b.forward * speed;
        }

        if(CharacterControll.stun && !trig && !CharacterControll.isjump)
        {
            trig = true;
            StartCoroutine(create_poça());
        }

        if(!CharacterControll.stun)
        {
            trig = false;
        }
    }

    private IEnumerator create_poça()
    {
        for_CC.life --;
        yield return new WaitForSeconds(0.2f);
        var poça = Instantiate(poça_obj, spawn_p.position, spawn_p.rotation);
        StartCoroutine(Destroy_poça(poça));
    }
    private IEnumerator Destroy_poça(GameObject poça)
    {
        yield return new WaitForSeconds(3);
        Destroy(poça);
    }
}
