using System;
using Logic.App;
using UnityEngine.UI;
using Zenject;

namespace Buttons
{
    public sealed class ExitButton : IInitializable, IDisposable
    {
        private readonly Button _button;
        private readonly ApplicationFinisher _applicationFinisher;

        public ExitButton(Button button, ApplicationFinisher applicationFinisher)
        {
            _button = button;
            _applicationFinisher = applicationFinisher;
        }

        void IInitializable.Initialize()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        void IDisposable.Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked() => _applicationFinisher.Finish();
    }
}