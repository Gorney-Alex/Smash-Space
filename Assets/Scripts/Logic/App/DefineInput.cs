using System;
using Logic.Classes;
using Logic.InputSystems;
using UnityEngine;
using Zenject;

public class DefineInput : ITickable, IDisposable, IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly DesktopInputSystem _desktopInput;
    private readonly MobileInputSystem _mobileInput;
    private readonly InputProvider _inputProvider;

    private bool _inputDefined;
    private bool _isLocked;

    public DefineInput(
        DesktopInputSystem desktopInput,
        MobileInputSystem mobileInput,
        InputProvider inputProvider,
        SignalBus signalBus)
    {
        _desktopInput = desktopInput;
        _mobileInput = mobileInput;
        _inputProvider = inputProvider;
        _signalBus = signalBus;

        _inputDefined = false;
        _isLocked = false;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<ToggleLockGameInputSignal>(OnLockStateChanged);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<ToggleLockGameInputSignal>(OnLockStateChanged);
    }

    public void Tick()
    {
        if (_isLocked || _inputDefined) return;

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            _inputProvider.SetInput(_desktopInput);
            _inputDefined = true;
            HideCursor();

            Debug.Log($"Input: {_desktopInput}");
        }
        else if (Input.touchCount > 0)
        {
            _inputProvider.SetInput(_mobileInput);
            _inputDefined = true;

            Debug.Log($"Input: {_mobileInput}");
        }
    }

    private void OnLockStateChanged(ToggleLockGameInputSignal signal)
    {
        _isLocked = signal.IsLocked;

        if (_isLocked)
        {
            ShowCursor();
        }
        else
        {
            HideCursor();
        }
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
