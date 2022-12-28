using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer();
        GameObject CreateSpawner();
        void CreateSnail();
        GameObject CreateHUD();
    }
}