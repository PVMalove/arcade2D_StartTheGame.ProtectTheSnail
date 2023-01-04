using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreatePoolEntry();
        void CreateTutorial();
        GameObject CreateSpawner();
        GameObject CreatePlayer();
        GameObject CreateHUD();
    }
}