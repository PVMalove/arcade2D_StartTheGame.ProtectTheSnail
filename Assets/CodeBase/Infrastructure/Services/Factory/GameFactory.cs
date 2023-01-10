using CodeBase.Gameplay.Arrow;
using CodeBase.Gameplay.Player;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.UI.Elements.View;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IRandomService _randomService;
        private readonly IPauseService _pauseService;
        private readonly IWindowService _windowService;

        private GameObject SpawnerObject { get; set; }

        public GameFactory(IAssetProvider assets, IRandomService randomService,
            IPauseService pauseService, IWindowService windowService)
        {
            _assets = assets;
            _randomService = randomService;
            _pauseService = pauseService;
            _windowService = windowService;
        }

        public void CreatePoolEntry() =>
            _assets.Instantiate(AssetAddress.PoolEntryPath);

        public GameObject CreateSpawner()
        {
            SpawnerObject = InstantiateRegistered(AssetAddress.SpawnerPath);
            SpawnerObject.GetComponent<Spawner>().Construct(_randomService);

            return SpawnerObject;
        }

        public GameObject CreatePlayer()
        {
            GameObject player = InstantiateRegistered(AssetAddress.PlayerPath);
            player.GetComponent<PlayerCheckAttack>().Construct(SpawnerObject.GetComponent<Spawner>());
            player.GetComponent<PlayerDead>().Construct(_pauseService, _windowService);

            return player;
        }

        public GameObject CreateHUD() =>
            _assets.Instantiate(AssetAddress.HUDPath);

        public StartGameView CreateMainMenu()
        {
            GameObject mainMenuObject = _assets.Instantiate(AssetAddress.MainMenuPath);
            foreach (var openWindowButton in mainMenuObject.GetComponentsInChildren<OpenWindowButtonView>())
                openWindowButton.Construct(_windowService);

            return mainMenuObject.GetComponent<StartGameView>();
        }

        private void Register(GameObject gameObject) => 
            RegisterPauseHandler(gameObject);

        private void RegisterPauseHandler(GameObject gameObject)
        {
            foreach (IPauseHandler pauseHandler in gameObject.GetComponentsInChildren<IPauseHandler>())
                _pauseService.Register(pauseHandler);
        }
        private GameObject InstantiateRegistered(string prefabPath) {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            Register(gameObject);
            return gameObject;
        }
        
    }
}