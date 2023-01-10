using CodeBase.Infrastructure.Services;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        WindowDesign GetWindowConfig(WindowType windowType);
    }
}