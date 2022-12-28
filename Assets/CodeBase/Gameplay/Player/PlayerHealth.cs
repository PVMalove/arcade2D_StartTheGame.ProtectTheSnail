using System;
using CodeBase.Gameplay.Logic;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        private const int MaxHealth = 5;
        
        [SerializeField, Range(0, MaxHealth)] private int _currentHealth;

        public int Current
        {
            get => _currentHealth;
            private set => _currentHealth = value;
        }
        public int Max => MaxHealth;

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