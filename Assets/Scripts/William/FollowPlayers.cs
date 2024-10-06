using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayers : MonoBehaviour
{
    public Transform player1; // Referencia al primer jugador
    public Transform player2; // Referencia al segundo jugador

    public float smoothSpeed = 0.125f; // Suavizado de la c�mara
    public Vector3 offset; // Offset de la c�mara respecto al centro de ambos jugadores

    public float minZoom = 5f; // Zoom m�ximo de acercamiento (cuando los jugadores est�n juntos)
    public float maxZoom = 15f; // Zoom m�nimo de alejamiento (cuando los jugadores est�n lejos)
    public float zoomLimiter = 10f; // Factor limitador de zoom (cuanto m�s bajo, m�s r�pido se ajusta el zoom)

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
            return;

        // Centrar la c�mara entre los dos jugadores
        Vector3 centerPoint = GetCenterPoint();

        // Ajustar la posici�n de la c�mara suavemente
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);

        // Ajustar el zoom de la c�mara seg�n la distancia entre los jugadores
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    // Calcula el punto medio entre los dos jugadores
    Vector3 GetCenterPoint()
    {
        return (player1.position + player2.position) / 2f;
    }

    // Obtiene la mayor distancia entre los jugadores para ajustar el zoom
    float GetGreatestDistance()
    {
        return Vector3.Distance(player1.position, player2.position);
    }
}
