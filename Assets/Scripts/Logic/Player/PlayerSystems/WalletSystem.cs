using System;
using Zenject;

namespace Logic.Player
{
    public sealed class WalletSystem : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        private int _startAmount;
        private int _currentAmount;

        public int StartAmount => _startAmount;

        public event Action<int> OnPointsChanged;

        public WalletSystem(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void SetParameters(PlayerData playerData)
        {
            _startAmount = playerData.StartPoints;
            _currentAmount = _startAmount;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BigAsteroidDieSignal>(AddAmount);
            _signalBus.Subscribe<BigAsteroidCompletelyDieSignal>(AddAmount);
            _signalBus.Subscribe<SmallAsteroidDieSignal>(AddAmount);
            _signalBus.Subscribe<AlienShipDieSignal>(AddAmount);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<BigAsteroidDieSignal>(AddAmount);
            _signalBus.TryUnsubscribe<BigAsteroidCompletelyDieSignal>(AddAmount);
            _signalBus.TryUnsubscribe<SmallAsteroidDieSignal>(AddAmount);
            _signalBus.TryUnsubscribe<AlienShipDieSignal>(AddAmount);
        }

        public void AddAmount(EnemyDieSignal signal)
        {
            _currentAmount += signal.Value;

            OnPointsChanged?.Invoke(_currentAmount);
        }
    }
}