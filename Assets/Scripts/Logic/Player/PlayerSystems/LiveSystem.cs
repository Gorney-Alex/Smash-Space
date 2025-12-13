using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public sealed class LiveSystem : IDisposable
    {
        private int _startAmount;
        private int _currentAmount;
        
        private float _ivulnerabilitySeconds;
        private bool _isInvulnerable;
        private float _blinkSpeed;
        private float _alphaMin;
        private float _alphaMax;
        
        private CancellationTokenSource _blinkCancel;
        private CancellationTokenSource _invulnerabilityCancel;
        private CancellationTokenSource _lifeTimeCancel;
        
        public int StartAmount => _startAmount;
    
        public event Action<int> OnHealthChanged;
        
        private readonly SignalBus _signalBus;
        private Collider2D _collider;
        private SpriteRenderer _sprite;
    
        public LiveSystem(SignalBus signal)
        {
            _signalBus = signal;
            _isInvulnerable = false;
            _lifeTimeCancel = new CancellationTokenSource();
        }

        public void SetParameters(PlayerData data, Collider2D collider, SpriteRenderer sprite)
        {
            _startAmount = data.StartHealth;
            _currentAmount = _startAmount;
            
            _ivulnerabilitySeconds = data.InvulnerabilitySeconds;
            _blinkSpeed = data.BlinkSpeed;
            _alphaMin = data.AlphaMin;
            _alphaMax = data.AlphaMax;
            
            _collider = collider;
            _sprite = sprite;
        }

        public void Dispose()
        {
            _lifeTimeCancel?.Cancel();
            _lifeTimeCancel?.Dispose();
            
            _blinkCancel?.Cancel();
            _blinkCancel?.Dispose();
            
            _invulnerabilityCancel?.Cancel();
            _invulnerabilityCancel?.Dispose();
        }
        
        public void TakeDamage(int amount)
        {
            if (_isInvulnerable || _lifeTimeCancel.IsCancellationRequested) return;
            
            _currentAmount -= amount;
            
            OnHealthChanged?.Invoke(_currentAmount);

            DoInvulnerability().Forget();

            if (_currentAmount <= 0)
            {
                Die();
            }
        }

        private async UniTask DoInvulnerability()
        {
            if (_lifeTimeCancel.IsCancellationRequested) return;
            
            _invulnerabilityCancel?.Cancel();
            _invulnerabilityCancel?.Dispose();
            
            _invulnerabilityCancel = new CancellationTokenSource();
            
            var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(
                _invulnerabilityCancel.Token, 
                _lifeTimeCancel.Token
            );
            
            _isInvulnerable = true;

            if (_collider == null)
            {
                Debug.LogError("_collider == null");
                
                _isInvulnerable = false;
                linkedToken?.Dispose();
                return;
            }

            _collider.enabled = false;
            
            _blinkCancel?.Cancel();
            _blinkCancel?.Dispose();
            
            _blinkCancel = new CancellationTokenSource();
            
            var blinkLinkedToken = CancellationTokenSource.CreateLinkedTokenSource(
                _blinkCancel.Token,
                _lifeTimeCancel.Token
            );
            
            try
            {
                BlinkAlphaLerp(blinkLinkedToken.Token).Forget();
                
                await UniTask.Delay(
                    TimeSpan.FromSeconds(_ivulnerabilitySeconds), 
                    cancellationToken: linkedToken.Token
                );
                
                _blinkCancel?.Cancel();
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                blinkLinkedToken.Dispose();
                linkedToken.Dispose();
                
                if (!_lifeTimeCancel.IsCancellationRequested)
                {
                    if (_collider != null)
                    {
                        _collider.enabled = true;
                    }
                    
                    _isInvulnerable = false;
                    
                    if (_sprite != null)
                    {
                        Color final = _sprite.color;
                        final.a = 1f;
                        _sprite.color = final;
                    }
                }
            }
        }

        private async UniTask BlinkAlphaLerp(CancellationToken token)
        {
            bool fadingOut = true;

            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (_sprite == null) return;
                    
                    float targetAlpha = fadingOut ? _alphaMin : _alphaMax;
                    
                    Color color = _sprite.color;
                    color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * _blinkSpeed);
                    _sprite.color = color;

                    if (Mathf.Abs(color.a - targetAlpha) < 0.05f)
                    {
                        fadingOut = !fadingOut;
                    }
                    
                    await UniTask.Yield(token);
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                if (_sprite != null && !_lifeTimeCancel.IsCancellationRequested)
                {
                    Color final = _sprite.color; 
                    final.a = 1f; 
                    _sprite.color = final;
                }
            }
        }

        private void Die() => _signalBus.Fire(new PlayerDieSignal());
    }
}