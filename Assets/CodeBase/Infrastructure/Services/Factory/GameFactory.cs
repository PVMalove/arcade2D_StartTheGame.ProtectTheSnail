using CodeBase.Gameplay.Arrow;
using CodeBase.Gameplay.Player;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.UI.Elements.View;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IRandomService _randomService;
        private readonly IPauseService _pauseService;

        private GameObject SpawnerObject { get; set; }

        public GameFactory(IAssetProvider assets, IRandomService randomService, IPauseService pauseService)
        {
            _assets = assets;
            _randomService = randomService;
            _pauseService = pauseService;
        }

        public void CreatePoolEntry() =>
            _assets.Instantiate(AssetAddress.PoolEntryPath);

        public GameObject CreateSpawner()
        {
            SpawnerObject = _assets.Instantiate(AssetAddress.SpawnerPath);
            SpawnerObject.GetComponent<Spawner>().Construct(_randomService);
            Register(SpawnerObject);
            return SpawnerObject;
        }

        public GameObject CreatePlayer()
        {
            GameObject player = _assets.Instantiate(AssetAddress.PlayerPath);
            player.GetComponent<PlayerCheckAttack>().Construct(SpawnerObject.GetComponent<Spawner>());
            player.GetComponent<PlayerDead>().Construct(SpawnerObject.GetComponent<Spawner>(), _pauseService);
            Register(player);
            return player;
        }

        public GameObject CreateHUD() =>
            _assets.Instantiate(AssetAddress.HUDPath);

        public MainMenuView CreateMainMenuUI()
        {
            GameObject mainMenuObject = _assets.Instantiate(AssetAddress.MainMenuPath);
            return mainMenuObject.GetComponent<MainMenuView>();
        }

        public TutorialPanel CreateTutorialPanelUI()
        {
            GameObject tutorialPanelObject = _assets.Instantiate(AssetAddress.TutorialPanelPath);
            return tutorialPanelObject.GetComponent<TutorialPanel>();
        }

        private void Register(GameObject gameObject)
        {
            RegisterPauseHandler(gameObject);
        }

        private void RegisterPauseHandler(GameObject gameObject)
        {
            foreach (IPauseHandler pauseHandler in gameObject.GetComponentsInChildren<IPauseHandler>())
                _pauseService.Register(pauseHandler);
        }
    }
}