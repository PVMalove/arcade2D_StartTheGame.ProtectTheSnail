namespace CodeBase.Infrastructure.States
{
    public class MainSceneState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public MainSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Exit()
        {
            Initialize();
        }

        public void Enter()
        {
            
        }

        private  void Initialize()
        {
            _sceneLoader.Loud("MainScene");
        }
    }
}