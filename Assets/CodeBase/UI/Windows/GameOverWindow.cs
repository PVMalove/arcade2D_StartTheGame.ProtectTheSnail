using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.UI.Elements.View;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class GameOverWindow : BaseWindow
    {
        [SerializeField] private RestartButtonView _restartButton;
        
        private IGameRunnerService _game;
        
        public void Construct(IPauseService pauseService, IGameRunnerService gameRunnerService)
        {
            base.Construct(pauseService);
            _game = gameRunnerService;
        }

        private void OnEnable() => 
            _restartButton.RestartGame += OnRestart;

        private void OnDisable() => 
            _restartButton.RestartGame -= OnRestart;

        private void OnRestart() => 
            _game.Restart();
    }
}