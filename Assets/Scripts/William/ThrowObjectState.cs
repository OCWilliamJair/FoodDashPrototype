using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ThrowObjectState : MonoBehaviour
{
    InputManager _inputs;

    bool _canThrowObject = true;

    [SerializeField] float _timeToNextThrow;
    [SerializeField] public float throwForce = 10f;
    [SerializeField] public float throwAngle = 45f;
    private void Update()
    {
        if (_inputs.IsThrowObject && PlayerPropiertes._pickedObjects.Count > 0 && _canThrowObject)
        {
            ThrowObject();
        }

    }

    private void Awake()
    {
        _inputs = GetComponent<InputManager>();
    }

    void ThrowObject()
    {
        GameObject obj = PlayerPropiertes._pickedObjects[PlayerPropiertes._pickedObjects.Count - 1];
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        IsTakeable _take = obj.GetComponent<IsTakeable>();

        if (rb != null)
        {
            rb.isKinematic = false;
            obj.transform.parent = null;

            Vector3 throwDirection = (transform.forward + Vector3.up * Mathf.Tan(throwAngle * Mathf.Deg2Rad)).normalized;
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
        if (_take != null)
        {
            _take._isTakeable = true;
            PlayerPropiertes._currentSpeed = PlayerPropiertes._currentSpeed + (PlayerPropiertes._speed * (_take._speedDecrease / 100));
            Debug.Log(PlayerPropiertes._currentSpeed);
        }

        PlayerPropiertes._pickedObjects.RemoveAt(PlayerPropiertes._pickedObjects.Count - 1);
        StartCoroutine(TimeToNextThrow());
    }

    public IEnumerator TimeToNextThrow()
    {
        _canThrowObject = false;
        yield return new WaitForSeconds(_timeToNextThrow);
        _canThrowObject = true;
    }

}
