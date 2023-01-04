using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class CloseButtonView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _closeWindow;

        public event Action CloseWindow;

        private void OnEnable() => 
            _closeButton.onClick.AddListener(Close);

        private void OnDisable() => 
            _closeButton.onClick.RemoveListener(Close);

        private void Close()
        {
            CloseWindow?.Invoke();
            _closeWindow.SetActive(false);
        }
    }
}