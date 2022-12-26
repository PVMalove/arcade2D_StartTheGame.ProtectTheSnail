using CodeBase.Gameplay.Player;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Logic;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Loud(sceneName, OnSceneLoaded);
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
            GameObject player = _gameFactory.CreatePlayer();
            _gameFactory.CreateSpawner();
            _gameFactory.CreateSnail();
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