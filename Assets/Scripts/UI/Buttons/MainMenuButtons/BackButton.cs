using System;
using UI.EmptyClasses;
using UnityEngine.UI;
using Zenject;

namespace Buttons
{
    public sealed class BackButton : IInitializable, IDisposable
    {
        private readonly Button _button;
        private readonly MainMenu _mainMenu;
        private readonly KeysMenu _keysMenu;

        public BackButton(Button button, MainMenu mainMenu, KeysMenu keysMenu)
        {
            _button = button;
            _mainMenu = mainMenu;
            _keysMenu = keysMenu;
        }

        void IInitializable.Initialize()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        void IDisposable.Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _mainMenu.OnReveal();
            _keysMenu.OnHide();
        }
    }
}