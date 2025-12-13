using Zenject;

namespace Logic.Enemies
{
    public class SmallAsteroidModel
    {
        [Inject] private TickableManager _tickableManager;
        
        private SmallAsteroidLive _live;
        private SmallAsteroidMovement _movement;
        
        public SmallAsteroidModel(SmallAsteroidLive live, SmallAsteroidMovement movement)
        {
            _live = live;
            _movement = movement;
        }
        
        public void SetParameters(PhysicSystem physicSystem, SmallAsteroidView view, SmallAsteroidData data, LimitsData limits)
        {
            _movement.SetParameters(physicSystem, view, data, limits);
            _live.SetParameters(data, view);
            
            _tickableManager.AddFixed(_movement);
        }

        public void OnHit(int damage)
        {
            _live.TakeDamage(damage);
        }
        
        public void OnDie()
        {
            _live.Die();
        }
    }
}