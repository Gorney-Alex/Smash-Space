using System.Collections.Generic;
using Logic.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public sealed class PhysicSystem : MonoBehaviour
{
    public static readonly List<PhysicSystem> AllObjects = new List<PhysicSystem>();

    private float _mass;
    private float _minSpeed;
    private float _maxSpeed;
    private float _friction;
    private float _bounceForce;

    private Vector2 _velocity;
    private Vector2 _accumulatedForce;
    private Vector2 _acceleration;

    private Collider2D _collider;
    
    public void SetParametrs(float mass, float minSpeed, float maxSpeed, float friction, float bounceForce)
    {
        _mass = mass;
        _friction = friction;

        _minSpeed = minSpeed;
        _maxSpeed = maxSpeed;
        _bounceForce = bounceForce;
    }
    
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable() => AllObjects.Add(this);

    private void OnDisable() => AllObjects.Remove(this);

    private void FixedUpdate()
    {
        CalculateAcceleration();
        CalculateVelocity();
        CheckSpeed();

        transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
        _accumulatedForce = Vector2.zero;

        CheckCollisions();
    }

    public void AddForce(Vector2 direction, float force) => _accumulatedForce += direction * force;
    
    public void SetInstantVelocity(Vector2 direction, float force)
    {
        _velocity = direction.normalized * Mathf.Clamp(force, _minSpeed, _maxSpeed);
    }

    private void CalculateAcceleration()
    {
        _acceleration = _accumulatedForce / _mass;
        
        if (float.IsNaN(_acceleration.x) || float.IsNaN(_acceleration.y))
        {
            _acceleration = Vector2.zero;
        }
    }
    
    private void CalculateVelocity()
    {
        _velocity += _acceleration * Time.fixedDeltaTime;
        _velocity *= 1f - _friction * Time.fixedDeltaTime;
        
        if (float.IsNaN(_velocity.x) || float.IsNaN(_velocity.y))
        {
            _velocity = Vector2.zero;
        }
    }

    private void CheckSpeed()
    {
        if (_velocity.sqrMagnitude > 0.0001f && _velocity.magnitude < _minSpeed)
        {
            _velocity = _velocity.normalized * _minSpeed;
        }
        
        if (_velocity.sqrMagnitude > 0.0001f && _velocity.magnitude > _maxSpeed)
        {
            _velocity = _velocity.normalized * _maxSpeed;
        }
    }

    private void CheckCollisions()
    {
        var snapshot = new List<PhysicSystem>(AllObjects);
        
        foreach (PhysicSystem other in snapshot)
        {
            if (other == this) continue;
            
            HandleCollision(other);
        }
    }

    private void HandleCollision(PhysicSystem other)
    {
        if (_collider == null || other._collider == null) return;

        bool thisIsAmmo = GetComponent<ICanBeAmmo>() != null;
        bool otherIsAmmo = other.GetComponent<ICanBeAmmo>() != null;

        bool thisIgnoresAmmo = GetComponent<ICanBeAmmo>() != null;
        bool otherIgnoresAmmo = other.GetComponent<ICanBeAmmo>() != null;
        
        if ((thisIsAmmo && otherIgnoresAmmo) || (otherIsAmmo && thisIgnoresAmmo)) return;

        if (_collider is CircleCollider2D && other._collider is CircleCollider2D)
        {
            HandleCircleCircle(other);
        }
        else if (_collider is BoxCollider2D && other._collider is BoxCollider2D)
        {
            HandleBoxBox(other);
        }
        else
        {
            HandleCircleBox(this, other);
        }
    }
    
    private void HandleCircleCircle(PhysicSystem other)
    {
        Vector2 posA = transform.position;
        Vector2 posB = other.transform.position;

        float rA = GetColliderExtent(_collider);
        float rB = GetColliderExtent(other._collider);

        float dist = Vector2.Distance(posA, posB);
        float sumR = rA + rB;

        if (dist < sumR)
        {
            Vector2 normal = (posA - posB).normalized;
            float overlap = sumR - dist;

            ResolveCollision(this, other, normal, overlap);
        }
    }
    
    private void HandleBoxBox(PhysicSystem other)
    {
        var boxA = (BoxCollider2D)_collider;
        var boxB = (BoxCollider2D)other._collider;

        Vector2 aPos = transform.position;
        Vector2 bPos = other.transform.position;

        Vector2 aSize = Vector2.Scale(boxA.size, transform.localScale);
        Vector2 bSize = Vector2.Scale(boxB.size, other.transform.localScale);

        Vector2 delta = bPos - aPos;
        Vector2 overlap = new Vector2(
            (aSize.x + bSize.x) * 0.5f - Mathf.Abs(delta.x),
            (aSize.y + bSize.y) * 0.5f - Mathf.Abs(delta.y)
        );

        if (overlap.x > 0 && overlap.y > 0)
        {
            if (overlap.x < overlap.y)
            {
                float sign = Mathf.Sign(delta.x);
                Vector2 normal = new Vector2(sign, 0);
                ResolveCollision(this, other, normal, overlap.x);
            }
            else
            {
                float sign = Mathf.Sign(delta.y);
                Vector2 normal = new Vector2(0, sign);
                ResolveCollision(this, other, normal, overlap.y);
            }
        }
    }
    
    private void HandleCircleBox(PhysicSystem a, PhysicSystem b)
    {
        PhysicSystem circle = a._collider is CircleCollider2D ? a : b;
        PhysicSystem box = a._collider is BoxCollider2D ? a : b;

        var circleCol = (CircleCollider2D)circle._collider;
        var boxCol = (BoxCollider2D)box._collider;

        Vector2 circlePos = circle.transform.position;
        Vector2 boxPos = box.transform.position;

        Vector2 halfSize = Vector2.Scale(boxCol.size, box.transform.localScale) * 0.5f;
        
        Vector2 localPoint = circlePos - boxPos;
        Vector2 closestPoint = new Vector2(
            Mathf.Clamp(localPoint.x, -halfSize.x, halfSize.x),
            Mathf.Clamp(localPoint.y, -halfSize.y, halfSize.y)
        );

        Vector2 closestWorld = boxPos + closestPoint;
        Vector2 delta = circlePos - closestWorld;
        float dist = delta.magnitude;

        float radius = circleCol.radius * Mathf.Max(circle.transform.localScale.x, circle.transform.localScale.y);

        if (dist < radius)
        {
            Vector2 normal = dist > 0.0001f ? delta.normalized : Vector2.up;
            float overlap = radius - dist;
            ResolveCollision(circle, box, normal, overlap);
        }
    }
    
    private void ResolveCollision(PhysicSystem a, PhysicSystem b, Vector2 normal, float overlap)
    {
        float massA = Mathf.Max(0.0001f, a._mass);
        float massB = Mathf.Max(0.0001f, b._mass);

        Vector2 separation = normal * overlap * 0.5f;
        
        a.transform.position += (Vector3)separation;
        b.transform.position -= (Vector3)separation;
        
        a._velocity -= normal * a._bounceForce / massA;
        b._velocity += normal * b._bounceForce / massB;

        a._velocity *= 0.95f;
        b._velocity *= 0.95f;

        a.GetComponent<ICanBeCollision>()?.OnCustomCollision(b.gameObject);
        b.GetComponent<ICanBeCollision>()?.OnCustomCollision(a.gameObject);
    }
    
    private float GetColliderExtent(Collider2D collider)
    {
        switch (collider)
        {
            case CircleCollider2D circle:
                return circle.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
            case BoxCollider2D box:
                Vector3 scale = transform.localScale;
                return Mathf.Max(box.size.x * scale.x, box.size.y * scale.y) * 0.5f;
            default:
                return 0.5f;
        }
    }
}