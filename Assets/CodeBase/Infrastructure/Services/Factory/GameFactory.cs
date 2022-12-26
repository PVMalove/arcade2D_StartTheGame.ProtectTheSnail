using CodeBase.Gameplay.Arrow;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IRandomService _randomService;

        public GameFactory(IAssetProvider assets, IRandomService randomService)
        {
            _assets = assets;
            _randomService = randomService;
        }
        
        public GameObject CreatePlayer() => 
            _assets.Instantiate(AssetAddress.PlayerPath);

        public void CreateSpawner()
        {
          GameObject spawner = _assets.Instantiate(AssetAddress.SpawnerPath);
          spawner.GetComponent<Spawner>().Construct(_randomService);
        }

        public void CreateSnail() => 
            _assets.Instantiate(AssetAddress.SnailPath);

        public GameObject CreateHUD() =>
            _assets.Instantiate(AssetAddress.HUDPath);
    }
}