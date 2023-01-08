using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private CloseButtonView _closeButton;

        private void OnEnable() => 
            _closeButton.CloseWindow += ClosePanel;

        private void OnDisable() => 
            _closeButton.CloseWindow -= ClosePanel;

        private void ClosePanel() => 
            gameObject.SetActive(false);

        public void Show() => 
            gameObject.SetActive(true);
    }
}