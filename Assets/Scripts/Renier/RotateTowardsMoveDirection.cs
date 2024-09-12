using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class RotateTowardsMoveDirection : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 50f; // Velocidad de rotación en grados por segundo
    private Vector3 _previousPosition;

    private void Start()
    {
        // Inicializa _previousPosition al valor actual de la posición
        _previousPosition = transform.position;
    }

    private void Update()
    {
        if(transform.position != _previousPosition)
        {
            Vector3 currentDirection = transform.position - _previousPosition;

            // Evita rotar si no hay movimiento
            if (currentDirection.magnitude > Mathf.Epsilon)
            {
                currentDirection.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }

        // Actualiza _previousPosition
        _previousPosition = transform.position;
    }
}
