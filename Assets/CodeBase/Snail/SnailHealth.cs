using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Snail
{
    public class SnailHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _currentHealth;
        [SerializeField, Range(0, 5)] private int _maxHealth;
        public int Current
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }
        public int Max
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public event Action HealthChanged;

        public void TakeDamage(int damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            HealthChanged?.Invoke();
        }
    }
}