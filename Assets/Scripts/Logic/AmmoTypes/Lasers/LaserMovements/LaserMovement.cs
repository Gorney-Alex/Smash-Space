using UnityEngine;
using Zenject;

namespace Logic.Lasers
{
    public sealed class LaserMovement : IFixedTickable
    {
        private PhysicSystem _physicSystem;
        private RedLaserView _view;
        
        private float _moveForce;

        public void SetParametrs(PhysicSystem physic, RedLaserView view, RedLaserData data, LimitsData limits)
        {
            _moveForce = data.MoveForce;
            _physicSystem = physic;
            _view = view;
            
            if (_physicSystem == null) Debug.LogError("PhysicSystem is null");
            
            _physicSystem.SetParametrs(
                data.Mass, 
                limits.MinSpeed, 
                limits.MaxSpeed, 
                data.Friction, 
                limits.BounceForce);
        }

        public void FixedTick() => DoMove();
        
        private void DoMove()
        {
            Vector2 currentDirection = _view.OwnerTransform.up;
            _physicSystem.SetInstantVelocity(currentDirection, _moveForce);
        }
    }
}