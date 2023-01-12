using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;


        public LoadProgressState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            if (_sceneLoader.PreviousSceneName == SceneNames.GameScene)
                _stateMachine.Enter<LoadSceneState, SceneNames>(SceneNames.GameScene);
            else
                _stateMachine.Enter<MainMenuState, SceneNames>(SceneNames.MainScene);
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new();
            return progress;
        }

        public void Exit()
        {
        }
    }
}