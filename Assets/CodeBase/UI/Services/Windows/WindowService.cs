using CodeBase.UI.Services.UIFactory;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowType windowType)
        {
            switch (windowType)
            {
                case  WindowType.None:
                    break;
                case WindowType.MainMenuWindow:
                    _uiFactory.CreateMainMenu();
                    break;
                case WindowType.TutorialWindow:
                    _uiFactory.CreateTutorial();
                    break;
                case WindowType.GameOverWindow:
                    _uiFactory.CreateGameOver();
                    break;
            }
        }
    }
}