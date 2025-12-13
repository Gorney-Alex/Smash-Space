using System;
using Logic.Player;
using UnityEngine;
using Zenject;

namespace Logic.InputSystems
{
    public sealed class DesktopInputSystem : ITickable, IPlayerInput, IInitializable, IDisposable
    {
        const string AXIS_HORIZONTAL = "Horizontal";
        
        private readonly PlayerMovement _playerMovement;
        private readonly ArsenalSystem _arsenalSystem;
        private readonly SignalBus _signalBus;
        private readonly EffectMovementSystem _effectMovementSystem;

        private bool _isInputLocked;

        public DesktopInputSystem(PlayerMovement movement, ArsenalSystem arsenal, SignalBus signalBus, EffectMovementSystem effectMovementSystem)
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _signalBus.Fire(new TogglePauseSignal());
            }

            if (!_isInputLocked)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_arsenalSystem == null)
                    {
                        Debug.LogError("LaserGun is null");
                    }

                    _arsenalSystem.GetFireMachineGun();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (_arsenalSystem == null)
                    {
                        Debug.LogError("LaserGun is null");
                    }

                    _arsenalSystem.GetFireLaserGun();
                }

                if (Input.GetKey(KeyCode.W))
                {
                    _playerMovement.DoMove();
                    _effectMovementSystem.OnCenterNozzle();
                }

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
                {
                    _playerMovement.DoFastMove();
                    _effectMovementSystem.OnCenterNozzle();
                }

                float horizontalValue = Input.GetAxis(AXIS_HORIZONTAL);
                if (Mathf.Abs(horizontalValue) > 0.01f)
                {
                    _playerMovement.DoRotate(horizontalValue);
                    _effectMovementSystem.StopPlayingNozzle();
                }
            }
        }

        private void OnInputLockChanged(ToggleLockGameInputSignal signal)
        {
            _isInputLocked = signal.IsLocked;
        }
    }
}