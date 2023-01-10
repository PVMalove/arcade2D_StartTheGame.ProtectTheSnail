using System.Diagnostics.CodeAnalysis;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.UI.Services.UIFactory;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
    public class MainMenuState : IPayloadedState<SceneNames>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;

        public MainMenuState(SceneLoader sceneLoader, LoadingCurtain curtain,
            IUIFactory uiFactory, IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _uiFactory = uiFactory;
            _windowService = windowService;
        }

        public void Enter(SceneNames sceneNames)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneNames, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLoaded()
        {
            _curtain.Hide();
            InitializeUIRoot();
            _windowService.Open(WindowType.MainMenuWindow);
        }

        private void InitializeUIRoot() => 
            _uiFactory.CreateUIRoot();
    }
}