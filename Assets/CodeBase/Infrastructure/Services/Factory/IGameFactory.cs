namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreatePlayer();
        void CreateSpawner();
    }
}