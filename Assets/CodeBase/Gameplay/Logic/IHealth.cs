using System;

namespace CodeBase.Gameplay.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        int Current { get; }
        int Max { get; }
    }
}