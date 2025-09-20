using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicSystem))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    [SerializeField] private float _rotateForce;
    
    PhysicSystem _physicSystem;

    private void Awake()
    {
        _physicSystem = GetComponent<PhysicSystem>();
    }

    public void DoMove()
    {
        Vector2 currentDirection = transform.up;
        _physicSystem.AddForce(currentDirection, _moveForce);
    }

    public void DoRotate(float horizontalInput)
    {
        float rotationAmount = -horizontalInput * _rotateForce * Time.deltaTime;
        transform.Rotate(0, 0, rotationAmount);
    }

    public void DoFastMove()
    {
        
    }
}
