using System;
using System.Threading;
using Logic.Interfaces;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Logic.Lasers
{
    [RequireComponent(typeof(PhysicSystem))]
    public abstract class LaserType : MonoBehaviour, IObjectPoolable, ICanBeBordered, ICanBeAmmo
    {
        public Transform OwnerTransform => transform;
        public GameObject OwnerGameObject => gameObject;

        private CancellationTokenSource _cancelLiveSystem;

        protected float Length;
        protected float LiveTime;

        private void OnEnable()
        {
            _cancelLiveSystem = new CancellationTokenSource();
            OnLiveCycle(_cancelLiveSystem.Token).Forget(); 
        }

        private void OnDisable()
        {
            _cancelLiveSystem?.Cancel();
            _cancelLiveSystem?.Dispose();
        }
        
        private async UniTask OnLiveCycle(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(LiveTime), cancellationToken : token);
            gameObject.SetActive(false);
        }
    }
}