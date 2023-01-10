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

        private Transform _uiRoot;
        
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, IPauseService pauseService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _pauseService = pauseService;
        }

        public void CreateTutorial()
        {
            WindowDesign config = _staticData.GetWindowConfig(WindowType.TutorialWindow);
            TutorialWindow window = Object.Instantiate(config.Prefab, _uiRoot) as TutorialWindow;
            if (window != null) 
                window.Construct(_pauseService);
        }
        
        public void CreateUIRoot()
        {
            GameObject uiRootObject = _assetProvider.Instantiate(AssetAddress.UICanvas);
            _uiRoot = uiRootObject.transform;
        }
    }
}