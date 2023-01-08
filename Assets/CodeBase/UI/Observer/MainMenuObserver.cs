using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Elements.View;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Observer
{

    public class MainMenuObserver
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly TutorialPanel _tutorialPanel;

        public MainMenuObserver(IGameStateMachine gameStateMachine, MainMenuView mainMenu, TutorialPanel tutorialPanel)
        {
            _gameStateMachine = gameStateMachine;
            _tutorialPanel = tutorialPanel;

            mainMenu.StartGame += OnStartGame;
            mainMenu.OnTutorialPanel += ShowTutorialPanel;
        }

        private void ShowTutorialPanel() => 
            _tutorialPanel.Show();


        private void OnStartGame() => 
            _gameStateMachine.Enter<LoadSceneState, SceneNames>(SceneNames.GameScene);
    }
}