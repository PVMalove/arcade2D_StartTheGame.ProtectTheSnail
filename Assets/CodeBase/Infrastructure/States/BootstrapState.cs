using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Randomizer;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Preloader";
        private const string GameScene = "GameScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Loud(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit(){}

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(GameScene);

        private void RegisterServices()
        {
            _services.RegisterSingle(implementation: InputService());
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<ITimerService>(new TimerService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IRandomService>(),
                _services.Single<ITimerService>()));
            
        }

        private static IInputService InputService() => 
            new KeyboardInputService();
    }
}