using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.View
{
    public class OpenWindowButtonView : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private WindowType _windowType;

        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Awake() => 
            _openButton.onClick.AddListener(Open);

        private void Open()
        {
            _windowService.Open(_windowType);
        }
    }
}