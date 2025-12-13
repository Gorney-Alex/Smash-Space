using Zenject;

namespace Logic.Lasers
{
    public sealed class RedlaserModel
    {
        [Inject] private TickableManager _tickableManager;
        
        private LaserMovement _movement;

        public RedlaserModel(LaserMovement movement)
        {
            _movement = movement;
        }

        public void SetParametrs(PhysicSystem physic, RedLaserView view, RedLaserData data, LimitsData limits)
        {
            _movement.SetParametrs(physic, view, data, limits);
            _tickableManager.AddFixed(_movement);
        }
    }
}