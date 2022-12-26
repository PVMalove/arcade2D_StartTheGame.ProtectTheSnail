using System;

namespace CodeBase.Logic
{
    public interface IDiamond
    {
        event Action ValueChanged;
        int Value { get; }
    }
}