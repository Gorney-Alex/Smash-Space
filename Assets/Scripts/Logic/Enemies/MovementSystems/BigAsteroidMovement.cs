using UnityEngine;
using Zenject;

namespace Logic.Enemies
{
    public sealed class BigAsteroidMovement : IFixedTickable
    {
        private float _moveForce;
        private float _rotateForce;

        private bool _isReady;

        private PhysicSystem _physicSystem;
        private BigAsteroidView _view;

        public void SetParameters(PhysicSystem physicSystem, BigAsteroidView view, BigAsteroidData data, LimitsData limits)
        {
            _isReady = false;
                
            _rotateForce = data.RotateForce;

            _physicSystem = physicSystem;

            if (_physicSystem == null) Debug.LogError("PhysicSystem is null");

            _view = view;

            if (_view == null) Debug.LogError("View is null");

            _physicSystem.SetParametrs(data.Mass, limits.MinSpeed, limits.MaxSpeed, data.Friction, limits.BounceForce);

            _isReady = true;
        }

        public void FixedTick()
        {
            if (!_isReady) return;

            DoMove();
        }

        private void DoMove()
        {
            Vector2 currentDirection = _view.OwnerGameObject.transform.up;
            _physicSystem.AddForce(currentDirection, _moveForce);

            float currentRotation = _rotateForce * Time.deltaTime;

            _view.OwnerGameObject.transform.Rotate(0, 0, currentRotation);
        }
    }
}