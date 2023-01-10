using CodeBase.Infrastructure.Services.Pool;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class PlayerDead : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private GameFX gameFX;

        private IWindowService _windowService;
        
        private bool _isDead;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Start() => 
            _health.HealthChanged += HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            gameFX.enabled = false;
            ObjectPool.DestroyAllPools();

            _windowService.Open(WindowType.GameOverWindow);
        }
    }
}