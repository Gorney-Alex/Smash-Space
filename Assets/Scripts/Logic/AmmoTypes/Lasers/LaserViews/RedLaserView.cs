using UnityEngine;
using Zenject;

namespace Logic.Lasers
{
    public sealed class RedLaserView : LaserType
    {
        private RedlaserModel _model;
        
        [Inject]
        public void Construct(RedlaserModel model, RedLaserData data, LimitsData limits)
        {
            LiveTime = data.LiveTime;
            Length = data.Length;

            SetLength();
            
            PhysicSystem physic = GetComponent<PhysicSystem>();
            
            _model = model;
            _model.SetParametrs(physic, this, data, limits);
        }
        
        private void SetLength()
        {
            Vector3 scale = transform.localScale;
            scale.y = Length;
            transform.localScale = scale;
        }
    }
}