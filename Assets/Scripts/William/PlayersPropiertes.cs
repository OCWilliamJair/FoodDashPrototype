using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPropiertes", menuName = "FoodDash/Player Propiertes")]
public class PlayersPropiertes : ScriptableObject
{
    

    [SerializeField] public float _speed = 10;
    public static float _currentSpeed;
    [SerializeField] public float friction = 15;
    [SerializeField] public float maxSpeed = 15;
    [SerializeField] public float _throwForce = 20;

    [SerializeField] public Vector3 _positionObjects;
    public int MAX_PICKUP_OBJECT = 3;

    [SerializeField] public bool _canJump;

    public List<GameObject> _pickedObjects = new List<GameObject>();

    public  bool _isTakenObject = false;

    private void Awake()
    {
        _currentSpeed = _speed;
    }
}
