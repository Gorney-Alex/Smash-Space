using System;
using UI.EmptyClasses;
using UnityEngine;
using Zenject;

namespace UI.UIControllers
{
    public class StopMenuController : IInitializable, IDisposable
    {
        private readonly StopMenu _screen;
        private readonly SignalBus _signalBus;
        private bool _isPaused;

        public StopMenuController(StopMenu screen, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _screen = screen;
            _isPaused = false;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<TogglePauseSignal>(OnTogglePause);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<TogglePauseSignal>(OnTogglePause);
        }

        private void OnTogglePause()
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
        public void ResumeGame()
        {
            Time.timeScale = 1f;
            _screen.OnHide();
            _isPaused = false;
            
            _signalBus.Fire(new ToggleLockGameInputSignal(false));
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
            _screen.OnReveal();
            _isPaused = true;
            
            _signalBus.Fire(new ToggleLockGameInputSignal(true));
        }
    }
}