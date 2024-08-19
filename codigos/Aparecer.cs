using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aparecer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objeto3D; // Objeto 3D que você deseja habilitar/desabilitar o Mesh Renderer
    [Range(0.1f, 10.0f)] public float distancia = 3;
    public GameObject Jogador;
    private MeshRenderer meshRenderer; // Referência ao MeshRenderer do objeto3D

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = objeto3D.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("O objeto3D não possui um componente MeshRenderer!");
        }

        meshRenderer.enabled = false; // Desativa o MeshRenderer no início
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Jogador.transform.position) < distancia)
        {
            meshRenderer.enabled = true; // Ativa o MeshRenderer se o jogador estiver dentro da distância especificada
        }
        else
        {
            meshRenderer.enabled = false; // Desativa o MeshRenderer se o jogador estiver fora da distância especificada
        }
    }
}
