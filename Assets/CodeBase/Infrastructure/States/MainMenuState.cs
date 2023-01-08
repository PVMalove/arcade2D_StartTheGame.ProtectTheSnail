using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Elements.View;
using CodeBase.UI.Observer;
using CodeBase.UI.Windows;


namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IPayloadedState<SceneNames>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public MainMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(SceneNames sceneNames)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneNames, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            _curtain.Hide();
            InitializeUI();
        }

        private void InitializeUI()
        {
            MainMenuView mainMenu = _gameFactory.CreateMainMenuUI();
            TutorialPanel tutorialPanel = _gameFactory.CreateTutorialPanelUI();
            new MainMenuObserver(_stateMachine, mainMenu, tutorialPanel);
        }
    }
}