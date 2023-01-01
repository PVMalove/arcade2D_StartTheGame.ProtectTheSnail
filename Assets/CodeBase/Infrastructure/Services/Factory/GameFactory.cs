using CodeBase.Gameplay.Arrow;
using CodeBase.Gameplay.Player;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IRandomService _randomService;
        
        private GameObject SpawnerObject { get; set; }

        public GameFactory(IAssetProvider assets, IRandomService randomService)
        {
            _assets = assets;
            _randomService = randomService;
        }

        public void CreatePoolEntry() =>
            _assets.Instantiate(AssetAddress.PoolEntryPath);

        public GameObject CreateSpawner()
        {
            SpawnerObject = _assets.Instantiate(AssetAddress.SpawnerPath);
            SpawnerObject.GetComponent<Spawner>().Construct(_randomService);
            return SpawnerObject;
        }

        public GameObject CreatePlayer()
        {
            GameObject player = _assets.Instantiate(AssetAddress.PlayerPath);
            player.GetComponent<PlayerCheckAttack>().Construct(SpawnerObject.GetComponent<Spawner>());
            player.GetComponent<PlayerDead>().Construct(SpawnerObject.GetComponent<Spawner>());
            return player;
        }

        public GameObject CreateHUD() =>
            _assets.Instantiate(AssetAddress.HUDPath);
    }
}