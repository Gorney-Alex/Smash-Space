using Zenject;

namespace Logic.Bullets
{
    public sealed class LargeCaliberView : BulletType
    {
        private LargeCaliberModel _model;
        
        [Inject]
        public void Construct(LargeCaliberModel model, LargeCaliberData data, LimitsData limits)
        {
            Damage = data.Damage;
            LiveTime = data.LiveTime;
            
            PhysicSystem physic = GetComponent<PhysicSystem>();
            
            _model = model;
            _model.SetParametrs(physic, this, data, limits);
        }
        
        public override int GetDamage() => Damage;
    }
}