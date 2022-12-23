using System;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Presenter
{
    public class StartGame : MonoBehaviour
    {
        private IInputService _input;

        public event Action GameStart;

        private void Awake()
        {
            _input = Game.InputService;
        }

        private void Update()
        {
            if (_input.GameStart)
            {
                GameStart?.Invoke();
            }
        }
    }
}