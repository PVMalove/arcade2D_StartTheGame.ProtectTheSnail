using System;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class StartButtonView : MonoBehaviour
    {
        [SerializeField] private BasicButton _startButton;

        public event Action StartGame;

        private void OnEnable() =>
            _startButton.AddListener(Launch);

        private void OnDisable() =>
            _startButton.RemoveListener(Launch);

        private void Launch() => 
            StartGame?.Invoke();
    }
}