using System;
using CodeBase.Gameplay.Logic;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class Diamond : MonoBehaviour, IDiamond
    {
        public int Value { get; private set; }

        public event Action ValueChanged;
        
        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Invalid amount");

            Value += amount;
            ValueChanged?.Invoke();
        }
    }
}