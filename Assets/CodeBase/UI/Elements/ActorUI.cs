using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private BalanceBar _balance;
        
        private IHealth _health;
        private IDiamond _diamond;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
            IDiamond diamond = GetComponent<IDiamond>();

            if (health != null && diamond != null)
                Construct(health, diamond);
        }

        private void OnDisable()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHealthBar;
            if (_diamond != null)
                _diamond.ValueChanged -= MoneyOnValueChanged;
        }

        public void Construct(IHealth health, IDiamond diamond)
        {
            _health = health;
            _diamond = diamond;
            _health.HealthChanged += UpdateHealthBar;
            _diamond.ValueChanged += MoneyOnValueChanged;
        }

        private void UpdateHealthBar() => 
            _healthBar.SetValue(_health.Current, _health.Max);

        private void MoneyOnValueChanged() => 
            _balance.SetValue(_diamond.Value);
    }
}