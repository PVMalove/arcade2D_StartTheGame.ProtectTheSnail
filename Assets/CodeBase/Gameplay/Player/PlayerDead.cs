using CodeBase.Gameplay.Arrow;
using CodeBase.Infrastructure.Services.Pool;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class PlayerDead : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private FX _fx;
        
        private Spawner _spawner;
        private bool _isDead;

        public void Construct(Spawner spawner) => 
            _spawner = spawner;

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
            
            _movement.enabled = false;
            _fx.enabled = false;
            _spawner.enabled = false;
            
            Debug.Log("<Interface>.LosePanel.Enable();");
        }
    }
}