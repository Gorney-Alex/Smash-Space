using System;
using UI.UIControllers;
using UnityEngine.UI;
using Zenject;

namespace Buttons
{
    public class ContinueButton : IDisposable, IInitializable
    {
        private readonly Button _button;
        private readonly StopMenuController _controller;

        public ContinueButton(Button button, StopMenuController controller)
        {
            _button = button;
            _controller = controller;
        }

        void IInitializable.Initialize()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        void IDisposable.Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked() => _controller.ResumeGame();
    }
}