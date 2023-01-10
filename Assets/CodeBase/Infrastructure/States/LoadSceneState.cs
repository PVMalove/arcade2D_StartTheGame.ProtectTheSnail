using CodeBase.Gameplay.Logic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.UIFactory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<SceneNames>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter(SceneNames sceneName)
        {
            if (_curtain != null)
                _curtain.Show();
            
            _sceneLoader.Load(sceneName, OnSceneLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnSceneLoaded()
        {
            InitializeGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitializeGameWorld()
        {
            _gameFactory.CreatePoolEntry();
            _gameFactory.CreateSpawner();
            GameObject player = _gameFactory.CreatePlayer();
            InitializeUIRoot();
            InitializeHUD(player);
        }

        private void InitializeUIRoot()
        {
            _uiFactory.CreateUIRoot();
        }

        private void InitializeHUD(GameObject player)
        {
            GameObject hud = _gameFactory.CreateHUD();
            hud.GetComponentInChildren<ActorUI>()
                .Construct(player.GetComponent<IHealth>(), player.GetComponent<IDiamond>());
        }
    }
}