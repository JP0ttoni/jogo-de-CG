using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class button_reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reset_fase1()
    {
        SceneManager.LoadScene("Fase_1");
    }

    public void reset_fase3()
    {
        change_cam.inicia_fight = false;
        boss_fight1.life_boss = 5;
        SceneManager.LoadScene("Fase_3");
    }

    public void reset_fase4()
    {
        SceneManager.LoadScene("Fase_4");
    }

    public void reset_boss()
    {
        for_CC.life = 6;
        morg.life_morg = 5;
        CharacterControll.stun = false;
        SceneManager.LoadScene("Boss fight");
    }
}
