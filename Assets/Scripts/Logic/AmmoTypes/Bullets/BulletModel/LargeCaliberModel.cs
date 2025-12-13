using Zenject;

namespace Logic.Bullets
{
    public sealed class LargeCaliberModel
    {
        [Inject] private TickableManager _tickableManager;
        
        private BulletsMovement _movement;

        public LargeCaliberModel(BulletsMovement movement)
        {
            _movement = movement;
        }

        public void SetParametrs(PhysicSystem physic, LargeCaliberView view, LargeCaliberData data, LimitsData limits)
        {
            _movement.SetParametrs(physic, view, data, limits);
            _tickableManager.AddFixed(_movement);
        }
    }
}