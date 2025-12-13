using System;
using Logic.Player;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModel
{
    public class HealthViewModel : IInitializable, IDisposable
    {
        [Data("ViewText")]
        public readonly ReactiveProperty<string> Health = new();
        
        private readonly LiveSystem _liveSystem;

        public HealthViewModel(LiveSystem liveSystem)
        {
            _liveSystem = liveSystem;
        }

        public void Initialize()
        {
            _liveSystem.OnHealthChanged += OnHealthChanged;

            OnHealthChanged(_liveSystem.StartAmount);
        }

        public void Dispose()
        {
            _liveSystem.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int amount)
        {
            Health.Value = amount + " HP";
        }
    }
}