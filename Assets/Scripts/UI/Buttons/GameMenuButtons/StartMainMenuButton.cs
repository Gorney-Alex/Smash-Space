using System;
using Logic.App;
using UnityEngine.UI;
using Zenject;

namespace Buttons
{
    public sealed class StartMainMenuButton : IInitializable, IDisposable
    {
        private readonly Button _button;
        private readonly GameLauncher _gameLauncher;

        public StartMainMenuButton(Button button, GameLauncher gameLauncher)
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

        private void OnButtonClicked() => _gameLauncher.StartMainMenu();
    }
}