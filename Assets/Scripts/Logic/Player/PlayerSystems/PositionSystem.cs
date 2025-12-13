using System;
using Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public sealed class PositionSystem : ITickable
    {
        private IPlayer _player;
        public event Action<Vector2> OnPositionChanged;

        private Vector2 _lastPosition;
        public Vector2 LastPosition => _lastPosition;
        
        public void SetParameters(IPlayer player)
        {
            _player = player;
            
            if (_player == null) Debug.LogWarning("Player is null");
            
            _lastPosition = _player.OwnerTransform.position;
        }
        
        public void Tick()
        {
            Vector2 currentPos = _player.OwnerTransform.position;
            
            if (currentPos != _lastPosition)
            {
                _lastPosition = currentPos;
                OnPositionChanged?.Invoke(currentPos);
            }
        }
    }
}