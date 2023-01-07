using System;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class CloseButtonView : MonoBehaviour
    {
        [SerializeField] private BasicButton _closeButton;

        public event Action CloseWindow;

        private void OnEnable() => 
            _closeButton.AddListener(Close);

        private void OnDisable() => 
            _closeButton.RemoveListener(Close);

        private void Close() => 
            CloseWindow?.Invoke();
    }
}