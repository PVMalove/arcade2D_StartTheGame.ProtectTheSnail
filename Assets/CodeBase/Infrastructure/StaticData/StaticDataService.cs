using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowType, WindowDesign> _windowConfigs;

        public void Load()
        {
            _windowConfigs = Resources
                .Load<WindowStaticData>("StaticData/UI/WindowStaticData")
                .Configs
                .ToDictionary(x => x.WindowType, x => x);
        }

        public WindowDesign GetWindowConfig(WindowType windowType) =>
            _windowConfigs.TryGetValue(windowType, out WindowDesign windowDesign)
                ? windowDesign
                : null;
    }
}