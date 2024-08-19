using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstpersoncameraController : MonoBehaviour
{
    public Transform target; // O objeto que a câmera deve seguir
    public float smoothSpeed = 0.125f; // A velocidade suave de movimento da câmera
    public Vector3 offset; // A distância da câmera em relação ao alvo
    public bool isFirstPerson = false; // Flag para indicar se é a câmera em primeira pessoa

    void LateUpdate()
    {
        if (target == null)
            return;

        if (isFirstPerson)
        {
            // Para a câmera em primeira pessoa, posicionamos diretamente no target
            transform.position = target.position + offset;
            transform.rotation = target.rotation;
        }
        else
        {
            // Para a câmera em terceira pessoa, calculamos a posição desejada
            Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y + 2, target.position.z) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
