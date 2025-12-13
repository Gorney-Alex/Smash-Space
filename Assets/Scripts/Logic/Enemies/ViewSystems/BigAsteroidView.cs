using Logic.Bullets;
using Logic.GeneralClasses;
using Logic.Interfaces;
using Logic.Lasers;
using UnityEngine;
using Zenject;

namespace Logic.Enemies
{
    [RequireComponent(typeof(PhysicSystem))]
    public sealed class BigAsteroidView : GameUnit, IEnemy, IObjectPoolable, ICanBeCollision, ICanBeBordered
    {
        public Transform OwnerTransform => transform;
        public GameObject OwnerGameObject => gameObject;

        public int Damage { get; private set; }

        private BigAsteroidModel _model;

        [Inject]
        public void Construct(BigAsteroidData data, LimitsData limits, BigAsteroidModel model)
        {
            PhysicSystem physicSystem = GetComponent<PhysicSystem>();

            Damage = data.Damage;

            _model = model;

            _model.SetParameters(physicSystem, this, data, limits);
        }

        public void OnCustomCollision(GameObject other)
        {
            if (other.TryGetComponent<LaserType>(out _))
            {
                _model.OnCompletelyDie();
            }

            if (other.TryGetComponent<BulletType>(out var bullet))
            {
                bullet.OwnerGameObject.SetActive(false);

                _model.OnHit(bullet.GetDamage());
            }
        }
    }
}