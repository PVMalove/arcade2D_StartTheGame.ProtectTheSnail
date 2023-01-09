namespace CodeBase.Infrastructure.Services.PauseService
{
    public interface IPauseHandler
    {
        void OnPauseChanged(bool isPaused);
    }
}