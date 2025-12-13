using System;
using MVVM;
using TMPro;
using UniRx;

namespace UI.Binders
{
    public sealed class TextBinder : IBinder, IObserver<string>
    {
        private readonly TextMeshProUGUI _text;
        private readonly IReadOnlyReactiveProperty<string> _points;
        private IDisposable _handle;

        public TextBinder(TextMeshProUGUI text, IReadOnlyReactiveProperty<string> points)
        {
            _text = text;
            _points = points;
        }

        public void Bind()
        {
            OnNext(_points.Value);
            _handle = _points.Subscribe(this);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnNext(string value) => _text.text = value;
        
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

    }
}