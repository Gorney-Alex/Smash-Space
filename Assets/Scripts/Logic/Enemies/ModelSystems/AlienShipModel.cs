using Zenject;

namespace Logic.Enemies
{
    public sealed class AlienShipModel
    {
        [Inject] private TickableManager _tickableManager;
        
        private AlienShipLive _live;
        private AlienShipMovement _movement;
        
        public AlienShipModel(AlienShipLive live, AlienShipMovement movement)
        {
            _live = live;
            _movement = movement;
        }
        
        public void SetParameters(AlienShipView view, AlienShipData data)
        {
            _movement.SetParameters(view, data);
            _live.SetParameters(data, view);
            
            _tickableManager.AddFixed(_movement);
        }

        public void OnHit(int damage) => _live.TakeDamage(damage);
        
        public void OnDie() => _live.Die();
    }
}