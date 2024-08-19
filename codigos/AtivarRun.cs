using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarRun : MonoBehaviour
{
    [Range(0.1f, 10.0f)] public float distancia = 3;
    public GameObject Jogador;
    public static bool ativarUmaVez = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!ativarUmaVez && Vector3.Distance(transform.position, Jogador.transform.position) < distancia)
        {
            ativarUmaVez = true; // Garantir que a ativação ocorra apenas uma vez
            Run.Instance.ChangeCameraToRun();
        }
    }
}
