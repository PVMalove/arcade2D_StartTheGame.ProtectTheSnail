using CodeBase.Gameplay.Logic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<SceneNames>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
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
            InitGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.CreatePoolEntry();
            _gameFactory.CreateSpawner();
            GameObject player = _gameFactory.CreatePlayer();
            InitHUD(player);
        }

        private void InitHUD(GameObject player)
        {
            GameObject hud = _gameFactory.CreateHUD();
            hud.GetComponentInChildren<ActorUI>()
                .Construct(player.GetComponent<IHealth>(), player.GetComponent<IDiamond>());
        }
    }
}