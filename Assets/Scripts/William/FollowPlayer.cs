using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;  // Referencia al transform del jugador
    public Vector3 offset;    // Offset de la cámara respecto al jugador
    public float smoothSpeed = 0.1f;  // Velocidad de suavizado de la cámara

    void FixedUpdate()
    {
        // Posición deseada: la posición del jugador + el offset
        Vector3 desiredPosition = player.position + offset;

        // Posición suavizada: suavizamos el movimiento para que no sea brusco
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplicamos la posición suavizada a la cámara
        transform.position = smoothedPosition;

        // Mantener la rotación de la cámara actual
        // No se hace nada aquí para que la cámara mantenga su rotación original
    }
}
