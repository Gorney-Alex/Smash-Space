using Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace Logic.Enemies
{
    public sealed class AlienShipMovement : IFixedTickable
    {
        private float _moveForce;
        private float _rotateForce;

        private bool _isReady;
        
        private readonly IPlayer _player;
        
        private AlienShipView _view;

        public AlienShipMovement(IPlayer player)
        {
            _isReady = false;
            
            _player = player;
        }
    
        public void SetParameters(AlienShipView view, AlienShipData data)
        {
            _moveForce = data.MoveForce;
            _rotateForce = data.RotateForce;
            
            _view = view;

            if (_view == null) Debug.LogError("View is null");

            _isReady = true;
        }
        
        public void FixedTick()
        {
            if (!_isReady) return;
                
            DoFollow();
        }

        private void DoFollow()
        {
            _view.OwnerTransform.position = Vector2.MoveTowards(_view.OwnerTransform.position, _player.OwnerTransform.position, _moveForce * Time.deltaTime);
            float currentRotation = _rotateForce * Time.deltaTime;
            _view.OwnerTransform.Rotate(0, 0, currentRotation);
        }
    }
}