using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Windows;

public class TakeObjectState : MonoBehaviour
{
    InputManager _inputs;

    bool _canTakeObject = true;

    [SerializeField] PlayersPropiertes _playerPropiertes;

    [SerializeField] float _timeToNextTake;

    private GameObject _currentObjectColission;

    private void Awake()
    {
        _inputs = GetComponent<InputManager>();
    }
    private void Update()
    {
        if
        (_inputs.IsTakenObject
         && _playerPropiertes._pickedObjects.Count < _playerPropiertes.MAX_PICKUP_OBJECT
         && _currentObjectColission != null
         && _currentObjectColission.GetComponent <IsTakeable>()._isTakeable   
         && _canTakeObject
        )
        {
            TakeObject(_currentObjectColission);
        }
               
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IsTakeable>() != null)
        {
            _currentObjectColission = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IsTakeable>() != null)
        {
            _currentObjectColission = null;
        }       
    }

    void TakeObject(GameObject obj)
    {
        _playerPropiertes._pickedObjects.Add(obj);

        int _currentPosition = _playerPropiertes._pickedObjects.Count;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        IsTakeable _take = obj.GetComponent<IsTakeable>();

        if (rb != null)
        {
            rb.isKinematic = true;

        }
        if (_take != null)
        {
            _take._isTakeable = false;
            _playerPropiertes._currentSpeed = _playerPropiertes._currentSpeed - (_playerPropiertes._speed * (_take._speedDecrease / 100));
        }
        obj.transform.position = _playerPropiertes._positionObjects[_currentPosition].transform.position;
        obj.transform.rotation = _playerPropiertes._positionObjects[_currentPosition].transform.rotation;
        obj.transform.parent = _playerPropiertes._positionObjects[_currentPosition];
        obj.transform.localScale = new Vector3(obj.transform.localScale.x / 1.2f, obj.transform.localScale.y / 1.2f, obj.transform.localScale.z / 1.2f);
       
        StartCoroutine(TimeToNextTake());
    }
    
    public IEnumerator TimeToNextTake()
    {
        _canTakeObject = false;
        yield return new WaitForSeconds(_timeToNextTake);
        _canTakeObject = true;
    }
}
