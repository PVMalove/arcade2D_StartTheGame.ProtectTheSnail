using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _continueButton;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(Pause);
            _continueButton.onClick.AddListener(Continue);
        }

        private void Continue() => 
            Time.timeScale = 1;

        private void Pause() => 
            Time.timeScale = 0;
    }
}