using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UI.Services.UIFactory;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.Load(SceneNames.Preloader, onLoaded: OnLoaded);

        public void Exit() { }

        private void OnLoaded() =>
            _stateMachine.Enter<LoadProgressState>();
           

        private void RegisterServices()
        {
            RegisterStaticDataService();

            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPauseService>(new PauseService());
           
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IPauseService>()));
            
            _services.RegisterSingle<IWindowService>( new WindowService(
                _services.Single<IUIFactory>()));
            
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IRandomService>(),
                _services.Single<IPauseService>(),
                _services.Single<IWindowService>()));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private static IInputService InputService() =>
            new KeyboardInputService();
    }
}