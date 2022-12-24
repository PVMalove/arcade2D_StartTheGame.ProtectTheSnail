namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        bool RightTop { get; }
        bool RightDown { get; }
        bool LeftTop { get; }
        bool LeftDown { get; }
    }
}