using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnObstacle : MonoBehaviour
{
    // Prefab del objeto a instanciar
    public GameObject objectPrefab;

    // Intervalo entre la aparición de las instancias
    public float spawnInterval = 2f;

    // Duración del movimiento y animación
    public float moveDuration = 5f;

    // Duración de la rotación
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
        // Iniciamos el ciclo de instanciación
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (keepSpawning)
        {
            // Instanciamos el objeto
            GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

            // Iniciamos la animación de rotación
            newObject.transform.DORotate(new Vector3(0, 360, 0), rotateDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);

            // Elegimos una dirección aleatoria hacia el eje X o Z
            Vector3 targetPosition = transform.position;
            targetPosition.x += Random.Range(-xMovementRange, xMovementRange);
            targetPosition.z += Random.Range(-zMovementRange, zMovementRange);

            // Movemos el objeto a la posición final
            newObject.transform.DOMove(targetPosition, moveDuration).SetEase(Ease.InOutQuad);

            // Esperamos antes de que el objeto desaparezca
            Destroy(newObject, objectLifetime);

            // Esperamos hasta el próximo spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
