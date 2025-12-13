using System;
using UI.EmptyClasses;
using UnityEngine;
using Zenject;

namespace UI.UIControllers
{
    public sealed class DeathMenuController : IInitializable, IDisposable
    {
        private readonly DeathMenu _screen;
        private readonly SignalBus _signalBus;

        public DeathMenuController(DeathMenu screen, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _screen = screen;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDieSignal>(ShowDeathScreen);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PlayerDieSignal>(ShowDeathScreen);
        }

        public void ShowDeathScreen()
        {
            Time.timeScale = 0f;
            _screen.OnReveal();
            
            _signalBus.Fire(new ToggleLockGameInputSignal(true));
        }
    }
}