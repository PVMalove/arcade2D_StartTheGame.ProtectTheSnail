namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract bool RightTop { get; }
        public abstract bool RightDown { get; }
        public abstract bool LeftTop { get; }
        public abstract bool LeftDown { get; }
        
        public abstract bool Top { get; }
        public abstract bool Down { get; }
        public abstract bool Left { get; }
        public abstract bool Right { get; }
    }
}