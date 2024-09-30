using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectState : MonoBehaviour
{
    InputManager _inputs;

    [SerializeField] PlayersPropiertes _playerPropiertes;

    [SerializeField] float _timeToNextDrop;

    bool _canDropObject = true;

    void DropObject()
    {
        GameObject obj = _playerPropiertes._pickedObjects[_playerPropiertes._pickedObjects.Count - 1];

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        IsTakeable _take = obj.GetComponent<IsTakeable>();

        if (rb != null)
        {
            rb.isKinematic = false;
        }
        if (_take != null)
        {
            _take._isTakeable = true;
            _playerPropiertes._currentSpeed = _playerPropiertes._currentSpeed + (_playerPropiertes._speed * (_take._speedDecrease / 100));
            Debug.Log(_playerPropiertes._currentSpeed);
        }

        obj.transform.parent = null;
        _playerPropiertes._pickedObjects.RemoveAt(_playerPropiertes._pickedObjects.Count - 1);

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
        if (_inputs.IsDropObject && _playerPropiertes._pickedObjects.Count > 0 && _canDropObject)
        {
            DropObject();
        }
    }
}
