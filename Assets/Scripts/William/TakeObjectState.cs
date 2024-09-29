using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Windows;

public class TakeObjectState : MonoBehaviour
{
    InputManager _inputs;

    bool _canTakeObject = true;

    PlayerPropiertes _playerPropiertes;

    [SerializeField] Transform _areaToTakeObject;
    [SerializeField] float _timeToNextTake;

    private GameObject _currentObjectColission;

    private void Awake()
    {
        _inputs = GetComponent<InputManager>();
        _playerPropiertes = GetComponent<PlayerPropiertes>();
    }
    private void Update()
    {
        if
        (_inputs.IsTakenObject
         && PlayerPropiertes._pickedObjects.Count < _playerPropiertes.MAX_PICKUP_OBJECT
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
        PlayerPropiertes._pickedObjects.Add(obj);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        IsTakeable _take = obj.GetComponent<IsTakeable>();

        if (rb != null)
        {
            rb.isKinematic = true;
        }
        if (_take != null)
        {
            _take._isTakeable = false;
            PlayerPropiertes._currentSpeed = PlayerPropiertes._currentSpeed - (PlayerPropiertes._speed * (_take._speedDecrease / 100));
            Debug.Log(PlayerPropiertes._currentSpeed);
        }
        obj.transform.position = _playerPropiertes._positionObjects.position + new Vector3(0, PlayerPropiertes._pickedObjects.Count * 0.5f, 0);
        obj.transform.rotation = _playerPropiertes._positionObjects.rotation;
        obj.transform.parent = _playerPropiertes._positionObjects;

        

        StartCoroutine(TimeToNextTake());
    }
    
    public IEnumerator TimeToNextTake()
    {
        _canTakeObject = false;
        yield return new WaitForSeconds(_timeToNextTake);
        _canTakeObject = true;
    }
}
