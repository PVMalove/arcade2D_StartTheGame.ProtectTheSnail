using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Elements.View;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Observer
{
    public class MainMenuObserver
    {
        private readonly IGameStateMachine _gameStateMachine;

        public MainMenuObserver(IGameStateMachine gameStateMachine, StartGameView startGame)
        {
            _gameStateMachine = gameStateMachine;
            startGame.StartGame += OnStartGame;
        }

        private void OnStartGame() => 
            _gameStateMachine.Enter<LoadSceneState, SceneNames>(SceneNames.GameScene);
    }
}