using System;
using SpawnersLogic;
using Zenject;
using Random = UnityEngine.Random;

namespace Logic.Enemies
{
    public class BigAsteroidDeathHandler : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly SmallAsteroidSpawner _smallAsteroidSpawner;
        private readonly BigAsteroidDeathHandlerData _data;

        public BigAsteroidDeathHandler(SignalBus signalBus, BigAsteroidDeathHandlerData data, SmallAsteroidSpawner smallAsteroidSpawner)
        {
            _signalBus = signalBus;
            _smallAsteroidSpawner = smallAsteroidSpawner;
            _data = data;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BigAsteroidDieSignal>(OnBigAsteroidDied);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<BigAsteroidDieSignal>(OnBigAsteroidDied);
        }

        private void OnBigAsteroidDied(BigAsteroidDieSignal signal)
        {
            int spawnCount = Random.Range(_data.MinAmountSmallAsteroids, _data.MaxAmountSmallAsteroids + 1);

            for (int i = 0; i < spawnCount; i++)
            {
                _smallAsteroidSpawner.SpawnAtPosition(signal.Position);
            }
        }
    }
}