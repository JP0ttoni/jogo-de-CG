using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class life_keeper : MonoBehaviour
{
    public static int keep_life = 6;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(keep_life <= 0)
        {
            keep_life = 6;
            AtivarRun.ativarUmaVez = false;
            CharacterControll.move_mumia = false;
            ControladorDasChaves.chavesDoJogador.Clear();
            Destroy(gameObject);
            SceneManager.LoadScene("game_over");
        }
    }
}
