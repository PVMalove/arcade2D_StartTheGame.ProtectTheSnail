using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        private IHealth _health;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDisable()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHealthBar;
        }

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar() => 
            _healthBar.SetValue(_health.Current, _health.Max);
    }
}