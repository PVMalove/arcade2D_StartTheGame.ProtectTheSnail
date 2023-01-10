using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services.UIFactory
{
    public interface IUIFactory : IService
    {
        void CreateTutorial();
        void CreateUIRoot();
    }
}