using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadProgressState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            if (_sceneLoader.PreviousSceneName == SceneNames.GameScene)
                _stateMachine.Enter<LoadSceneState, SceneNames>(SceneNames.GameScene);
            else
                _stateMachine.Enter<MainMenuState, SceneNames>(SceneNames.MainScene);
        }

        public void Exit() { }
    }
}