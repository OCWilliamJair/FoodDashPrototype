using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectState : MonoBehaviour
{
    InputManager _inputs;

    [SerializeField] float _timeToNextDrop;

    bool _canDropObject = true;

    void DropObject()
    {
        GameObject obj = PlayerPropiertes._pickedObjects[PlayerPropiertes._pickedObjects.Count - 1];

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        IsTakeable _take = obj.GetComponent<IsTakeable>();

        if (rb != null)
        {
            rb.isKinematic = false;
        }
        if (_take != null)
        {
            _take._isTakeable = true;
            PlayerPropiertes._currentSpeed = PlayerPropiertes._currentSpeed + (PlayerPropiertes._speed * (_take._speedDecrease / 100));
            Debug.Log(PlayerPropiertes._currentSpeed);
        }

        obj.transform.parent = null;
        PlayerPropiertes._pickedObjects.RemoveAt(PlayerPropiertes._pickedObjects.Count - 1);

        StartCoroutine(TimeToNextDrop());
    }

    public IEnumerator TimeToNextDrop()
    {
        _canDropObject = false;
        yield return new WaitForSeconds(_timeToNextDrop);
        _canDropObject = true;
    }
    private void Awake()
    {
        _inputs = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (_inputs.IsDropObject && PlayerPropiertes._pickedObjects.Count > 0 && _canDropObject)
        {
            DropObject();
        }
    }
}
