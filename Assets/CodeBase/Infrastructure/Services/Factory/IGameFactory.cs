using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreatePoolEntry();
        GameObject CreateSpawner();
        GameObject CreatePlayer();
        GameObject CreateHUD();
    }
}