using Zenject;

namespace Logic.Enemies
{
    public sealed class AlienShipLive
    {
        private readonly SignalBus _signalBus;
        
        private int _maxHeartAmount;
        private int _currentHeartAmount;
        private int _points;
        
        private AlienShipView _view;
        
        public AlienShipLive(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void SetParameters(AlienShipData data, AlienShipView view)
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

        public void Die()
        {
            _signalBus.Fire(new AlienShipDieSignal(_points));
            _view.OwnerGameObject.SetActive(false);
        }
    }
}