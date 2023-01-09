using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.View
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _tutorialButton;

        public event Action StartGame;
        public event Action OnTutorialPanel; 

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _tutorialButton.onClick.AddListener(OnTutorialButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _tutorialButton.onClick.RemoveListener(OnTutorialButtonClicked);
        }

        private void OnPlayButtonClicked() => 
            StartGame?.Invoke();

        private void OnTutorialButtonClicked() => 
            OnTutorialPanel?.Invoke();
    }
}