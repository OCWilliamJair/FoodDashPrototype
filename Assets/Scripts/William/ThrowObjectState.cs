using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ThrowObjectState : MonoBehaviour
{
    InputManager _inputs;

    [SerializeField] public Slider _sliderPlayer;

    [SerializeField] public GameObject _canvasSlider;

    bool _canThrowObject = true;

    [SerializeField] float _timeToNextThrow;
    [SerializeField] public static float _currentThrowForce;
    [SerializeField] public static float minForce = 1f;
    [SerializeField] public static float maxForce = 20f;
    [SerializeField] public float _incrementSpeed = 1.5f;
    [SerializeField] public float _decrementSpeed = 3f;
    [SerializeField] public float throwAngle = 45f;
    private bool _inCreasingForce;
    private bool _isCharging = false;
    private void Update()
    {

        if (_inputs.IsThrowObject && PlayerPropiertes._pickedObjects.Count > 0 && _canThrowObject)
        {
            ChargingForce();
        }   
        else if(!_inputs.IsThrowObject && _isCharging)
        {
            ThrowObject(_currentThrowForce);
            _currentThrowForce = minForce;
        }
    }

    private void Awake()
    {
        _inputs = GetComponent<InputManager>();
        _sliderPlayer.maxValue = maxForce;
        _sliderPlayer.minValue = minForce;
    }

    void ChargingForce()
    {            
        _isCharging = true;
        _canvasSlider.SetActive(true);

        ChangedSliderValue(_currentThrowForce);

        if (_inCreasingForce)
        {
            _currentThrowForce += _incrementSpeed * Time.deltaTime;

            if (_currentThrowForce >= maxForce)
            {
                _inCreasingForce = false;
            }
        }
        else
        {
            _currentThrowForce -= _decrementSpeed * Time.deltaTime;

            if (_currentThrowForce <= minForce)
            {
                _inCreasingForce = true;
            }
        }       
    }

    void ThrowObject(float throwForce)
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

        StartCoroutine(_take.TimeToTake());
        PlayerPropiertes._pickedObjects.RemoveAt(PlayerPropiertes._pickedObjects.Count - 1);
        _isCharging = false;
        _canvasSlider.SetActive(false);
        StartCoroutine(TimeToNextThrow());
    }

    public IEnumerator TimeToNextThrow()
    {
        _canThrowObject = false;
        yield return new WaitForSeconds(_timeToNextThrow);
        _canThrowObject = true;
    }

    void ChangedSliderValue(float forceValue)
    {
        _sliderPlayer.value = _currentThrowForce; 
    }
}
