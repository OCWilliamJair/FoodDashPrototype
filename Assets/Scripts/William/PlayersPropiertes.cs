using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersPropiertes : MonoBehaviour
{
    
    [SerializeField] public float _speed = 10;
    [NonSerialized]  public float _currentSpeed;
    [SerializeField] public float friction = 15;
    [SerializeField] public float maxSpeed = 15;
    [SerializeField] public float _throwForce = 20;
    [SerializeField] public Transform[] _positionObjects;
    [SerializeField] public int MAX_PICKUP_OBJECT = 3;

    [SerializeField] public bool _canJump;

    public List<GameObject> _pickedObjects = new List<GameObject>();

    public  bool _isTakenObject = false;

    public bool _rangeQueve = false;

    private void Awake()
    {
        _currentSpeed = _speed;
    }
}
