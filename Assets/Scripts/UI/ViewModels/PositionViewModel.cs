using System;
using Logic.Player;
using MVVM;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.ViewModels
{
    public class PositionViewModel : IInitializable, IDisposable
    {
        [Data("ViewText")]
        public readonly ReactiveProperty<string> Position = new();

        private readonly PositionSystem _positionSystem;

        public PositionViewModel(PositionSystem positionSystem)
        {
            _positionSystem = positionSystem;
        }

        public void Initialize()
        {
            _positionSystem.OnPositionChanged += OnPositionChanged;
            
            var start = _positionSystem.LastPosition;
            OnPositionChanged(start);
        }

        public void Dispose()
        {
            _positionSystem.OnPositionChanged -= OnPositionChanged;
        }

        private void OnPositionChanged(Vector2 pos)
        {
            Position.Value = $"X: {pos.x:F0} | Y: {pos.y:F0}";
        }
    }
}