using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayers : MonoBehaviour
{
    public Transform player1; // Referencia al primer jugador
    public Transform player2; // Referencia al segundo jugador

    public float smoothSpeed = 0.125f; // Suavizado de la cámara
    public Vector3 offset; // Offset de la cámara respecto al centro de ambos jugadores

    public float minZoom = 5f; // Zoom máximo de acercamiento (cuando los jugadores están juntos)
    public float maxZoom = 15f; // Zoom mínimo de alejamiento (cuando los jugadores están lejos)
    public float zoomLimiter = 10f; // Factor limitador de zoom (cuanto más bajo, más rápido se ajusta el zoom)

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
            return;

        // Centrar la cámara entre los dos jugadores
        Vector3 centerPoint = GetCenterPoint();

        // Ajustar la posición de la cámara suavemente
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);

        // Ajustar el zoom de la cámara según la distancia entre los jugadores
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
