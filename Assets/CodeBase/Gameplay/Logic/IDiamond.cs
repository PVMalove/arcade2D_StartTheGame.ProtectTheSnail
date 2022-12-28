using System;

namespace CodeBase.Gameplay.Logic
{
    public interface IDiamond
    {
        event Action ValueChanged;
        int Value { get; }
    }
}