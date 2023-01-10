using System;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;

namespace CodeBase.StaticData.Windows
{
    [Serializable]
    public class WindowDesign
    {
        public WindowType WindowType;
        public BaseWindow Prefab;
    }
}