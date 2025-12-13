using Logic.EmptyClasses;
using Logic.GeneralClasses;
using Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    [RequireComponent(typeof(PhysicSystem))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerView : GameUnit, IPlayer, ICanBeCollision, ICanBeBordered
    {
        private ParticleSystem _effect;
        public Transform OwnerTransform => transform;

        private PlayerModel _playerModel;

        [Inject]
        public void Construct(PlayerModel playerModel, PlayerData data, LimitsData limits)
        {
            _playerModel = playerModel;

            PhysicSystem physicSystem = GetComponent<PhysicSystem>();
            Collider2D collider = GetComponent<Collider2D>();
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            CollisionEffect _collisionEffect = GetComponentInChildren<CollisionEffect>();

            if (_collisionEffect.TryGetComponent(out ParticleSystem effect))
            {
                _effect = effect;
            }


            EffectNozzleCenter centerNozzle = GetComponentInChildren<EffectNozzleCenter>();

            _playerModel.SetParameters(physicSystem, collider, this, data, limits, sprite, centerNozzle);
        }

        public void OnCustomCollision(GameObject other)
        {
            if (other.TryGetComponent<IEnemy>(out var enemy))
            {
                _effect.Play();
                _playerModel.OnHit(enemy.Damage);
            }
        }
    }
}