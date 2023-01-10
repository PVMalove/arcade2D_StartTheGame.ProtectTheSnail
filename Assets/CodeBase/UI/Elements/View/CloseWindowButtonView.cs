using System;
using UnityEngine;

namespace CodeBase.UI.Elements.View
{
    public class CloseWindowButtonView : MonoBehaviour
    {
        [SerializeField] private BasicButton _closeButton;

        public event Action CloseWindow;

        private void OnEnable() => 
            _closeButton.AddListener(OnCloseButtonClicked);

        private void OnDestroy() => 
            _closeButton.RemoveListener(OnCloseButtonClicked);

        private void OnCloseButtonClicked() => 
            CloseWindow?.Invoke();
    }
}