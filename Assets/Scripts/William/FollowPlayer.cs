using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;  // Referencia al transform del jugador
    public Vector3 offset;    // Offset de la c�mara respecto al jugador
    public float smoothSpeed = 0.1f;  // Velocidad de suavizado de la c�mara

    void FixedUpdate()
    {
        // Posici�n deseada: la posici�n del jugador + el offset
        Vector3 desiredPosition = player.position + offset;

        // Posici�n suavizada: suavizamos el movimiento para que no sea brusco
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplicamos la posici�n suavizada a la c�mara
        transform.position = smoothedPosition;

        // Mantener la rotaci�n de la c�mara actual
        // No se hace nada aqu� para que la c�mara mantenga su rotaci�n original
    }
}
