using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeObject : MonoBehaviour
{
    enum _typeObject
    {
        Obstacle,
        supply,
    }

    [SerializeField] _typeObject _currentTypeObject = _typeObject.supply;

}
