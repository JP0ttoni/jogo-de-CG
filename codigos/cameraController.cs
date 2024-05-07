using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform target; // O objeto que a câmera deve seguir
    public float smoothSpeed = 0.125f; // A velocidade suave de movimento da câmera

    
    public Vector3 offset; // A distância da câmera em relação ao alvo



    
    void LateUpdate()
    {

        if (target == null)
            return;

        // Calcula a posição desejada da câmera, apenas alterando a coordenada Z
        Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y + 2, target.position.z) + offset;

        // Calcula a posição suavizada da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;
    }
}