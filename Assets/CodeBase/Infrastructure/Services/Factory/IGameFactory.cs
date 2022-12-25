using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer();
        void CreateSpawner();
        void CreateSnail();
        GameObject CreateHUD();
    }
}