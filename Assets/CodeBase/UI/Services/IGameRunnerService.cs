using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services
{
    public interface IGameRunnerService : IService
    {
        void GoLoadScene();
        void Restart();
    }
}