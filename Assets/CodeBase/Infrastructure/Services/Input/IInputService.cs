namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        bool RightTop { get; }
        bool RightDown { get; }
        bool LeftTop { get; }
        bool LeftDown { get; }
        
        bool Top { get; }
        bool Down { get; }
        bool Left { get; }
        bool Right { get; }
    }
}