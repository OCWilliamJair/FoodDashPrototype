using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnObstacle : MonoBehaviour
{
    // Prefab del objeto a instanciar
    public GameObject objectPrefab;

    // Intervalo entre la aparici�n de las instancias
    public float spawnInterval = 2f;

    // Duraci�n del movimiento y animaci�n
    public float moveDuration = 5f;

    // Duraci�n de la rotaci�n
    public float rotateDuration = 3f;

    // Tiempo que dura cada instancia antes de desaparecer
    public float objectLifetime = 10f;

    // Controlar si las instancias deben seguir apareciendo
    public bool keepSpawning = true;

    // Rango de movimiento en el eje X y Z
    public float xMovementRange = 5f;
    public float zMovementRange = 5f;

    private void Start()
    {
        // Iniciamos el ciclo de instanciaci�n
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (keepSpawning)
        {
            // Instanciamos el objeto
            GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

            // Iniciamos la animaci�n de rotaci�n
            newObject.transform.DORotate(new Vector3(0, 360, 0), rotateDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);

            // Elegimos una direcci�n aleatoria hacia el eje X o Z
            Vector3 targetPosition = transform.position;
            targetPosition.x += Random.Range(-xMovementRange, xMovementRange);
            targetPosition.z += Random.Range(-zMovementRange, zMovementRange);

            // Movemos el objeto a la posici�n final
            newObject.transform.DOMove(targetPosition, moveDuration).SetEase(Ease.InOutQuad);

            // Esperamos antes de que el objeto desaparezca
            Destroy(newObject, objectLifetime);

            // Esperamos hasta el pr�ximo spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
