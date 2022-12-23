namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        bool RightTop { get; }
        bool RightDown { get; }
        bool LeftTop { get; }
        bool LeftDown { get; }
        
        bool GameStart { get; }
    }
}