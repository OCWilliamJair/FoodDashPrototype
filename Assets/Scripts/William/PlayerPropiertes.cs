using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropiertes : MonoBehaviour
{
    [SerializeField] public static float _speed = 10;
    public static float _currentSpeed;
    [SerializeField] public static float friction = 15;
    [SerializeField] public static float maxSpeed = 15;

    [SerializeField] public Transform _positionObjects;
    public int MAX_PICKUP_OBJECT = 3;

    [SerializeField] public static bool _canJump;

    public static List<GameObject> _pickedObjects = new List<GameObject>();

    public static bool _isTakenObject = false;

    private void Awake()
    {
        _currentSpeed = _speed;
    }
}
