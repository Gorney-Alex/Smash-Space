using Zenject;

namespace Logic.Enemies
{
    public sealed class BigAsteroidLive
    {
        private readonly SignalBus _signalBus;
        
        private int _maxHeartAmount;
        private int _currentHeartAmount;

        private int _points;
        
        private BigAsteroidView _view;
        
        public BigAsteroidLive(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void SetParameters(BigAsteroidData data, BigAsteroidView view)
        {
            _maxHeartAmount = data.StartHealth;
            _view = view;
            _currentHeartAmount = _maxHeartAmount;
            _points = data.Points;
        }

        public void TakeDamage(int amount)
        {
            _currentHeartAmount -= amount;

            if (_currentHeartAmount <= 0)
            {
                Die();
            }
        }
        
        public void CompletelyDie()
        {
            _signalBus.Fire(new BigAsteroidCompletelyDieSignal(_points));
            _view.OwnerGameObject.SetActive(false);
        }
        
        private void Die()
        {
            _signalBus.Fire(new BigAsteroidDieSignal(_points, _view.OwnerTransform.position));
            _view.OwnerGameObject.SetActive(false);
        }
    }
}