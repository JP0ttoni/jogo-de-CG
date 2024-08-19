using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController2 : MonoBehaviour
{
    public Transform playerTransform; // Referência ao Transform do jogador
    private Vector3 offset; // Deslocamento inicial da câmera em relação ao jogador

    void Start()
    {
        // Calcular o deslocamento inicial da câmera em relação ao jogador
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // Atualizar a posição da câmera para acompanhar apenas o eixo Z do jogador
        Vector3 newPosition = transform.position;
        newPosition.z = playerTransform.position.z + offset.z;
        transform.position = newPosition;
    }
}
