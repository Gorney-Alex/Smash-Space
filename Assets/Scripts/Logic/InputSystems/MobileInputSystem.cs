using System;
using Logic.Player;
using UnityEngine;
using Zenject;

namespace Logic.InputSystems
{
    public sealed class MobileInputSystem : ITickable, IPlayerInput, IDisposable, IInitializable
    {
        private readonly PlayerMovement _playerMovement;
        private readonly ArsenalSystem _arsenalSystem;
        private readonly SignalBus _signalBus;
        private readonly EffectMovementSystem _effectMovementSystem;
        
        private bool _isInputLocked;

        private Vector2 _touchStartPos;
        private bool _isMoving;
        private float _rotationSensitivity = 0.01f;
        private float _fastMoveThreshold = 150f;

        public MobileInputSystem(PlayerMovement movement, ArsenalSystem arsenal, SignalBus signalBus, EffectMovementSystem effectMovementSystem)
        {
            _playerMovement = movement;
            _arsenalSystem = arsenal;
            _signalBus = signalBus;
            _effectMovementSystem = effectMovementSystem;
            
            _isInputLocked = false;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<ToggleLockGameInputSignal>(OnInputLockChanged);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ToggleLockGameInputSignal>(OnInputLockChanged);
        }

        public void Tick()
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            if (!_isInputLocked)
            {

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _touchStartPos = touch.position;
                        _isMoving = true;
                        _arsenalSystem.GetFireMachineGun();
                        break;

                    case TouchPhase.Moved:
                        if (_isMoving)
                        {
                            Vector2 delta = touch.position - _touchStartPos;

                            if (delta.y > _fastMoveThreshold)
                            {
                                _playerMovement.DoFastMove();
                            }
                            else
                            {
                                _playerMovement.DoMove();
                            }

                            float rotationDelta = delta.x * _rotationSensitivity;
                            _playerMovement.DoRotate(rotationDelta);
                            
                            _playerMovement.DoMove();
                            _effectMovementSystem.OnCenterNozzle();
                        }

                        break;

                    case TouchPhase.Ended:
                        _isMoving = false;
                        _effectMovementSystem.StopPlayingNozzle();
                        break;

                    case TouchPhase.Canceled:
                        _isMoving = false;
                        _effectMovementSystem.StopPlayingNozzle();
                        break;
                }

                if (Input.touchCount >= 2)
                {
                    Touch secondTouch = Input.GetTouch(1);
                    if (secondTouch.phase == TouchPhase.Began)
                    {
                        _arsenalSystem.GetFireLaserGun();
                    }
                }
            }
        }
        
        private void OnInputLockChanged(ToggleLockGameInputSignal signal)
        {
            _isInputLocked = signal.IsLocked;
        }
    }
}