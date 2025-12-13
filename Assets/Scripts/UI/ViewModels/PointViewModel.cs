using System;
using Logic.Player;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class PointViewModel : IInitializable, IDisposable
    {
        [Data("ViewText")]
        public readonly ReactiveProperty<string> Points = new();
        
        private readonly WalletSystem _walletSystem;

        public PointViewModel(WalletSystem walletSystem)
        {
            _walletSystem = walletSystem;
        }

        public void Initialize()
        {
            _walletSystem.OnPointsChanged += OnPointsChanged;
            
            OnPointsChanged(_walletSystem.StartAmount);
        }

        public void Dispose()
        {
            _walletSystem.OnPointsChanged -= OnPointsChanged;
        }

        private void OnPointsChanged(int amount)
        {
            Points.Value = amount + " P";
        }
    }
}