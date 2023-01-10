using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.Infrastructure.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPauseService _pauseService;
        private readonly IGameRunnerService _gameRunnerService;

        private Transform _uiRoot;
        
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, IPauseService pauseService,
            IGameRunnerService gameRunnerService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _pauseService = pauseService;
            _gameRunnerService = gameRunnerService;
        }

        public void CreateMainMenu()
        {
            WindowDesign config = _staticData.GetWindowConfig(WindowType.MainMenuWindow);
            MainMenuWindow window = Object.Instantiate(config.Prefab, _uiRoot) as MainMenuWindow;

            if (window != null) 
                 window.Construct(_pauseService, _gameRunnerService, this);
        }
        
        public void CreateTutorial()
        {
            WindowDesign config = _staticData.GetWindowConfig(WindowType.TutorialWindow);
            TutorialWindow window = Object.Instantiate(config.Prefab, _uiRoot) as TutorialWindow;
            
            if (window != null) 
                window.Construct(_pauseService);
        }
        
        public void CreateGameOver()
        {
            WindowDesign config = _staticData.GetWindowConfig(WindowType.GameOverWindow);
            GameOverWindow window = Object.Instantiate(config.Prefab, _uiRoot) as GameOverWindow;
            if (window != null) 
                window.Construct(_pauseService, _gameRunnerService);
        }
        
        public void CreateUIRoot()
        {
            GameObject uiRootObject = _assetProvider.Instantiate(AssetAddress.UICanvas);
            _uiRoot = uiRootObject.transform;
        }
    }
}