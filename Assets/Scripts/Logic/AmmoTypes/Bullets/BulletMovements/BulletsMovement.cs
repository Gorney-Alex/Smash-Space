using UnityEngine;
using Zenject;

namespace Logic.Bullets
{
    public sealed class BulletsMovement : IFixedTickable
    {
        private PhysicSystem _physicSystem;
        private LargeCaliberView _view;
        
        private float _moveForce;

        public void SetParametrs(PhysicSystem physic, LargeCaliberView view, LargeCaliberData data, LimitsData limits)
        {
            _moveForce = data.MoveForce;
            _physicSystem = physic;
            _view = view;
            
            if (_physicSystem == null)
            {
                Debug.LogError("PhysicSystem is null in Update!");
            }
            
            _physicSystem.SetParametrs(
                data.Mass, 
                limits.MinSpeed, 
                limits.MaxSpeed, 
                data.Friction, 
                limits.BounceForce);
        }

        public void FixedTick()
        {
            DoMove();
        }

        private void DoMove()
        {
            Vector2 currentDirection = _view.OwnerTransform.up;
            _physicSystem.SetInstantVelocity(currentDirection, _moveForce);
        }
    }
}