using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.UI.Elements.View;
using CodeBase.UI.Services;
using CodeBase.UI.Services.UIFactory;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private StartGameButtonView _startGameButton;
        [SerializeField] private Button _buttonTutorial;
        
        private IGameRunnerService _game;
        private UIFactory _uiFactory;
        
        public void Construct(IPauseService pauseService, IGameRunnerService gameRunnerService,
            UIFactory uiFactory)
        {
            base.Construct(pauseService);
            _game = gameRunnerService;
            _uiFactory = uiFactory;
        }

        private void OnEnable()
        {
            _startGameButton.StartGame += OnStartGame;
            _buttonTutorial.onClick.AddListener(ViewTutorial);
        }

        private void OnDisable()
        {
            _startGameButton.StartGame -= OnStartGame;
            _buttonTutorial.onClick.RemoveListener(ViewTutorial);
        }

        private void ViewTutorial() => 
            _uiFactory.CreateTutorial();

        private void OnStartGame() => 
            _game.GoLoadScene();
    }
}