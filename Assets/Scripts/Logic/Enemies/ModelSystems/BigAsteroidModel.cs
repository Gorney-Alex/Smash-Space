using Zenject;

namespace Logic.Enemies
{
    public class BigAsteroidModel
    {
        [Inject] private TickableManager _tickableManager;
        
        private BigAsteroidLive _live;
        private BigAsteroidMovement _movement;
        
        public BigAsteroidModel(BigAsteroidLive live, BigAsteroidMovement movement)
        {
            _live = live;
            _movement = movement;
        }
        
        public void SetParameters(PhysicSystem physicSystem, BigAsteroidView view, BigAsteroidData data, LimitsData limits)
        {
            _movement.SetParameters(physicSystem, view, data, limits);
            _live.SetParameters(data, view);
            
            _tickableManager.AddFixed(_movement);
        }

        public void OnHit(int damage) => _live.TakeDamage(damage);
        
        public void OnCompletelyDie() => _live.CompletelyDie();
    }
}