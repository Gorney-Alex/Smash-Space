using System;
using Logic.App;
using UnityEngine.UI;
using Zenject;

namespace Buttons
{
    public class RestartGameButton : IDisposable, IInitializable
    {
        private readonly Button _button;
        private readonly GameLauncher _gameLauncher;

        public RestartGameButton(Button button, GameLauncher gameLauncher)
        {
            _button = button;
            _gameLauncher = gameLauncher;
        }

        void IInitializable.Initialize()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        void IDisposable.Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked() => _gameLauncher.RestartGame();
    }
}