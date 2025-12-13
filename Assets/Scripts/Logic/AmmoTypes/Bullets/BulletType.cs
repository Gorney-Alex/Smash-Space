using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Logic.Interfaces;

namespace Logic.Bullets
{
    [RequireComponent(typeof(PhysicSystem))]
    public abstract class BulletType : MonoBehaviour, IObjectPoolable, ICanBeBordered, ICanBeAmmo
    {
        public Transform OwnerTransform => transform;
        public GameObject OwnerGameObject => gameObject;

        protected int Damage;
        protected float LiveTime;

        private CancellationTokenSource _cancelLiveCycle;
        
        public abstract int GetDamage();

        private void OnEnable()
        {
            _cancelLiveCycle = new CancellationTokenSource();
            OnLiveCycle(_cancelLiveCycle.Token).Forget();
        }

        private void OnDisable()
        {
            _cancelLiveCycle?.Cancel();
            _cancelLiveCycle?.Dispose();
        }
        
        private async UniTask OnLiveCycle(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(LiveTime), cancellationToken: token);
            
            _cancelLiveCycle.Cancel();
            
            gameObject.SetActive(false);
        }
    }
}