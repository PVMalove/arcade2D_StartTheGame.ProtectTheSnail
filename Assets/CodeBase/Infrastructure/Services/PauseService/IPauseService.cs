namespace CodeBase.Infrastructure.Services.PauseService
{
    public interface IPauseService : IService
    {
        void Register(IPauseHandler pauseHandler);
        void Unregister(IPauseHandler pauseHandler);
        void SetPause(bool isPaused);
    }
}