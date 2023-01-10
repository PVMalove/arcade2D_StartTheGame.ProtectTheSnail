using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.Infrastructure.Services.Pool;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class PlayerDead : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private FX _fx;

        private IPauseService _pauseService;
        private IWindowService _windowService;
        
        private bool _isDead;

        public void Construct(IPauseService pauseService, IWindowService windowService)
        {
            _pauseService = pauseService;
            _windowService = windowService;
        }

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
            ObjectPool.DestroyAllPools();
           // _pauseService.SetPause(true);
            _fx.enabled = false;
            _windowService.Open(WindowType.TutorialWindow);

            
            Debug.Log("<Interface>.LosePanel.Enable();");
        }
    }
}