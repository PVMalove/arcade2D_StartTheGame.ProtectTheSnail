using System.Collections.Generic;
using CodeBase.StaticData.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    [CreateAssetMenu(menuName = "StaticData/Window static data", fileName = "WindowStaticData")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowDesign> Configs;
    }
}