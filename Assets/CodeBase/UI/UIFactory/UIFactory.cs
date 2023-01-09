using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PauseService;
using UnityEngine;

namespace CodeBase.UI.UIFactory
{
    public class UIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPauseService _pauseService;

        private Transform _uiRoot;
        
        public UIFactory(IAssetProvider assetProvider, IPauseService pauseService)
        {
            _assetProvider = assetProvider;
            _pauseService = pauseService;
        }

        public void CreateUIRoot()
        {
            GameObject uiRootObject = _assetProvider.Instantiate(AssetAddress.UICanvas);
            _uiRoot = uiRootObject.transform;
        }
    }
}