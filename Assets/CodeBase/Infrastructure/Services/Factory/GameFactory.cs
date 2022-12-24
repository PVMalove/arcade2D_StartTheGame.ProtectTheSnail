using CodeBase.Arrow;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IRandomService _randomService;
        private readonly ITimerService _timerService;

        public GameFactory(IAssetProvider assets, IRandomService randomService, ITimerService timerService)
        {
            _assets = assets;
            _randomService = randomService;
            _timerService = timerService;
        }
        
        public void CreatePlayer() => 
            _assets.Instantiate(AssetAddress.PlayerPath);

        public void CreateSpawner()
        {
          GameObject spawner = _assets.Instantiate(AssetAddress.SpawnerPath);
          spawner.GetComponent<Spawner>().Construct(_randomService, _timerService);
        }
    }
}