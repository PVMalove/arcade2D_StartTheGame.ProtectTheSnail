using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Randomizer;
using Plugins.Yandex.CodeBase;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Preloader";
        private const string GameScene = "GameScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly LoadingYandexSDK _yandex;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingYandexSDK yandex, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _yandex = yandex;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Loud(InitialScene, onLoaded: EnterLoadLevel);
            _yandex.Loud();
        }

        public void Exit(){}

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(GameScene);

        private void RegisterServices()
        {
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IRandomService>()));
        }

        private static IInputService InputService() => 
            new KeyboardInputService();
    }
}