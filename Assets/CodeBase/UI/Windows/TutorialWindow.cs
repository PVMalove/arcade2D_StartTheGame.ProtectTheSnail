using CodeBase.Infrastructure.Services.PauseService;

namespace CodeBase.UI.Windows
{
    public class TutorialWindow : BaseWindow
    {
        public new void Construct(IPauseService pauseService) => 
            base.Construct(pauseService);
    }
}