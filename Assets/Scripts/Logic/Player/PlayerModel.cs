using Logic.Classes;
using Logic.EmptyClasses;
using Logic.Interfaces;
using UnityEngine;

namespace Logic.Player
{
    public sealed class PlayerModel
    {
        private LiveSystem _live;
        private WalletSystem _wallet;
        private PlayerMovement _movement;
        private PositionSystem _position;
        private EffectMovementSystem _effectMovement;
        private readonly InputProvider _inputProvider;
        
        public PlayerModel(
            LiveSystem live, 
            WalletSystem wallet, 
            PlayerMovement movement, 
            PositionSystem  position, 
            EffectMovementSystem effectMovement,
            InputProvider inputProvider)
        {
            _wallet = wallet;
            _movement = movement;
            _live = live;
            _position = position;
            _effectMovement = effectMovement;
            _inputProvider = inputProvider;
        }
        
        public void SetParameters(
            PhysicSystem physicSystem, 
            Collider2D collider, 
            IPlayer view, 
            PlayerData data, 
            LimitsData limits, 
            SpriteRenderer sprite, 
            EffectNozzleCenter centerNozzle)
        {
            _movement.SetParameters(physicSystem, view, data, limits);
            _live.SetParameters(data, collider, sprite);
            _wallet.SetParameters(data);
            _position.SetParameters(view);
            _effectMovement.SetParameters(centerNozzle);
            var input = _inputProvider.CurrentInput;
        }
        
        public void OnHit(int damage) => _live.TakeDamage(damage);
    }
}