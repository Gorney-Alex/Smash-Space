using Logic.EmptyClasses;
using UnityEngine;

namespace Logic.Player
{
    public sealed class EffectMovementSystem
    {
        private ParticleSystem _centerNozzle;

        public void SetParameters(EffectNozzleCenter centerNozzle)
        {
            if (centerNozzle.TryGetComponent(out ParticleSystem center))
            {
                _centerNozzle = center;
            }
        }

        public void OnCenterNozzle() => _centerNozzle.Play();

        public void StopPlayingNozzle() => _centerNozzle.Stop();
    }
}