using Logic.Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public sealed class PlayerMovement
    {
        private float _moveForce;
        private float _rotateForce;

        private PhysicSystem _physicSystem;

        private IPlayer _view;

        public void SetParameters(PhysicSystem physicSystem, IPlayer view, PlayerData data, LimitsData limits)
        {
            _view = view;
            
            if(_view == null) Debug.LogError("view == null");

            _moveForce = data.MoveForce;
            _rotateForce = data.RotateForce;

            _physicSystem = physicSystem;
            
            if(_physicSystem == null) Debug.LogError("physicSystem == null");

            _physicSystem.SetParametrs(data.Mass, limits.MinSpeed, limits.MaxSpeed, data.Friction, limits.BounceForce);
        }

        public void DoMove()
        {
            Vector2 currentDirection = _view.OwnerTransform.transform.up;
            _physicSystem.AddForce(currentDirection, _moveForce);
        }

        public void DoRotate(float horizontalInput)
        {
            float rotationAmount = -horizontalInput * _rotateForce * Time.deltaTime;
            _view.OwnerTransform.transform.Rotate(0, 0, rotationAmount);
        }

        public void DoFastMove()
        {
            Vector2 currentDirection = _view.OwnerTransform.transform.up;
            _physicSystem.AddForce(currentDirection, _moveForce * 2);
        }
    }
}