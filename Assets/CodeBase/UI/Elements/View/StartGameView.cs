using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.View
{
    public class StartGameView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        public event Action StartGame;
 
        private void OnEnable() =>
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        
        private void OnDisable() => 
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);

        private void OnPlayButtonClicked() => 
            StartGame?.Invoke();
    }
}