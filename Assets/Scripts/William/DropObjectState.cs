using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropObjectState : MonoBehaviour
{
    InputManager _inputs;

    [SerializeField] float _timeToNextDrop;

    [SerializeField] PlayersPropiertes _playersPropiertes;

    bool _canDropObject = true;

    bool DropObjectValue { get; set; }

    public void OnDropObject(InputAction.CallbackContext context)
    {
        DropObjectValue = context.ReadValueAsButton();
    }

    void DropObject()
    {
        GameObject obj = _playersPropiertes._pickedObjects[_playersPropiertes._pickedObjects.Count - 1];

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        IsTakeable _take = obj.GetComponent<IsTakeable>();

        if (rb != null)
        {
            rb.isKinematic = false;
        }
        if (_take != null)
        {
            _take._isTakeable = true;
            _playersPropiertes._currentSpeed = _playersPropiertes._currentSpeed + (_playersPropiertes._speed * (_take._speedDecrease / 100));
            Debug.Log(_playersPropiertes._currentSpeed);
        }

        obj.transform.parent = null;
        obj.transform.localScale = new Vector3(obj.transform.localScale.x * 1.2f, obj.transform.localScale.y * 1.2f, obj.transform.localScale.z * 1.2f);
        _playersPropiertes._pickedObjects.RemoveAt(_playersPropiertes._pickedObjects.Count - 1);

        StartCoroutine(TimeToNextDrop());
    }

    public IEnumerator TimeToNextDrop()
    {
        _canDropObject = false;
        yield return new WaitForSeconds(_timeToNextDrop);
        _canDropObject = true;
    }
    private void Start()
    {
        _inputs = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (DropObjectValue && _playersPropiertes._pickedObjects.Count > 0 && _canDropObject)
        {
            DropObject();
        }
    }
}
