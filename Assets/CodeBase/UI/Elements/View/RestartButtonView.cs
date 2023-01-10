using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.View
{
        public class RestartButtonView : MonoBehaviour
        {
            [SerializeField] private Button _restartButton;

            public event Action RestartGame;
 
            private void OnEnable() =>
                _restartButton.onClick.AddListener(OnPlayButtonClicked);
        
            private void OnDisable() => 
                _restartButton.onClick.RemoveListener(OnPlayButtonClicked);

            private void OnPlayButtonClicked() => 
                RestartGame?.Invoke();
        }
}