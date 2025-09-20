using UnityEngine;

public class PhysicSystem : MonoBehaviour
{
    [SerializeField] private float _mass = 4f;

    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 100f;
    [SerializeField] private float _friction = 5;

    private Vector2 _velocity;
    private Vector2 _accumulatedForce;
    private Vector2 _acceleration;

    private void FixedUpdate()
    {
        CalculateAcceleration();

        _velocity += _acceleration * Time.fixedDeltaTime;
        _velocity *= 1f - _friction * Time.fixedDeltaTime;

        CheckSpeed();

        transform.position += (Vector3)_velocity * Time.fixedDeltaTime;

        _accumulatedForce = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out PhysicSystem spaceObject))
        {
            Vector2 normal = other.contacts[0].normal;
            
            _velocity = Vector2.Reflect(_velocity, normal);
        }
    }

    public void AddForce(Vector2 direction, float force) => _accumulatedForce += direction * force;

    private void CalculateAcceleration() => _acceleration = _accumulatedForce / _mass;

    private void CheckSpeed()
    {
        if (_velocity.magnitude < _minSpeed)
        {
            _velocity = _velocity.normalized * _minSpeed;
        }

        if (_velocity.magnitude > _maxSpeed)
        {
            _velocity = _velocity.normalized * _maxSpeed;
        }
    }
}