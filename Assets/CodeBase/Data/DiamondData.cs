using System;

namespace CodeBase.Data
{
    [Serializable]
    public class DiamondData
    {
        public int Value;

        public Action ValueChanged;
        
        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Invalid amount");

            Value += amount;
            ValueChanged?.Invoke();
        }
    }
}