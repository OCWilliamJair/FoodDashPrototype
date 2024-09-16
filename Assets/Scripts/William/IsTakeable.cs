using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTakeable : MonoBehaviour
{
    [Range(0,100)] public float _speedDecrease;
    [SerializeField] private float _timeToTakeAgain;
    [SerializeField] public bool _isTakeable;



    public IEnumerator TimeToTake()
    {
        _isTakeable = false;
        yield return new WaitForSeconds(_timeToTakeAgain);
        _isTakeable = true;
    }
}
