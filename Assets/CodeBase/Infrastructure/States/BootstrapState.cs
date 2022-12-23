using CodeBase.Infrastructure.Services.Input;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Preloader";
        private const string GameScene = "GameScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Loud(InitialScene, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(GameScene);

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }

        public void Exit(){}

        private static IInputService RegisterInputService() => 
            new KeyboardInputService();
    }
}