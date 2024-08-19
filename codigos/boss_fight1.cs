using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class boss_fight1 : MonoBehaviour
{
    public GameObject ball_obj, ghost_obj;
    public Transform plyr, spawn1, spawn2, spawn_top;
    private bool trigger = true, trigger_pos = false, is_down = false, is_up = true;
    public static int life_boss = 3;
    private int current_pos = 2;
    public Slider lifebar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifebar.value = life_boss;
        transform.LookAt(plyr);
        if(life_boss <= 0)
        {
            StartCoroutine(kill());
        }
        if(change_cam.inicia_fight && trigger && !is_down)
        {
            lifebar.gameObject.SetActive(true);
            Debug.Log("criou");
            StartCoroutine(create_ghost());
            StartCoroutine(create());
            trigger_pos = true;
        }   

        if(is_down && trigger_pos)
        {
            if(current_pos == 2)
            {
                transform.position = spawn2.position;
                current_pos = 1;
            }else{
                transform.position = spawn1.position;
                current_pos = 2;   
            }
            trigger_pos = false;
            StartCoroutine(set_up());
        }

        if(is_up && trigger_pos)
        {
            transform.position = spawn_top.position;
            trigger_pos = false;
            StartCoroutine(set_down());
        }
    }

    private IEnumerator create()
    {
        var look = new Vector3(plyr.position.x, transform.position.y, plyr.position.z);
        var ball = Instantiate(ball_obj, look, transform.rotation);
        trigger = false;
        yield return new WaitForSeconds(5f);
        trigger = true;
    }

    private IEnumerator set_down()
    {
        yield return new WaitForSeconds(7);
        trigger_pos = true;
        is_up = false;
        is_down = true;
    }

    private IEnumerator create_ghost()
    {
        Instantiate(ghost_obj, spawn1.position, spawn1.rotation);
        Instantiate(ghost_obj, spawn2.position, spawn2.rotation);
        yield return new WaitForSeconds(8);
        StartCoroutine(create_ghost());
    }

    private IEnumerator kill()
    {

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Fase_4");
        Destroy(gameObject);
    }

    private IEnumerator set_up()
    {
        yield return new WaitForSeconds(4);
        trigger_pos = true;
        is_up = true;
        is_down = false;
    }
}
