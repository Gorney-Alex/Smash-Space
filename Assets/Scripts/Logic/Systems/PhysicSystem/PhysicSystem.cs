using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicSystem : MonoBehaviour
{
    private float _mass = 4f;
    private float _thrustForce = 100f;
    private float _rotationSpeed = 200f;
    
    private Vector2 _velocity;
    private float _currentRotation;
    
    void Update()
    {
        float dt = Time.deltaTime;

        // Поворот (только угол, без Rigidbody)
        _currentRotation -= Input.GetAxis("Horizontal") * _rotationSpeed * dt;
        transform.rotation = Quaternion.Euler(0, 0, _currentRotation);

        // Силы
        Vector2 force = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            // Направление по текущему углу
            Vector2 direction = transform.up;
            force += direction * _thrustForce;
        }

        // Ускорение
        Vector2 acceleration = force / _mass;

        // Скорость
        _velocity += acceleration * dt;

        // Позиция
        Vector2 pos = (Vector2)transform.position + _velocity * dt;
        transform.position = pos;
    }
}
