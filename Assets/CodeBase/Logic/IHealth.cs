using System;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        int Current { get; }
        int Max { get; }
    }
}