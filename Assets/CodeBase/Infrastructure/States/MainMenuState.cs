using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Elements.View;
using CodeBase.UI.Observer;
using CodeBase.UI.Services.UIFactory;
using CodeBase.UI.Windows;


namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IPayloadedState<SceneNames>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public MainMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
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
            InitializeUIRoot();
            InitializeUI();
        }

        private void InitializeUIRoot() => 
            _uiFactory.CreateUIRoot();

        private void InitializeUI()
        {
            StartGameView startGame = _gameFactory.CreateMainMenu();
            new MainMenuObserver(_stateMachine, startGame);
        }
    }
}