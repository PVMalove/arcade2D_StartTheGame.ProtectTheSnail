using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services.UIFactory
{
    public interface IUIFactory : IService
    {
        void CreateMainMenu();
        void CreateTutorial();
        void CreateUIRoot();
        void CreateGameOver();
    }
}