using System;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Presenter
{
    public class Presenter : MonoBehaviour
    {
        public event Action GameStart;

        private IInputService _input;

        private void Awake() =>
            _input = Game.InputService;


        private void Update()
        {
            if (_input.GameStart) 
                OnPlay();
        }

        private void OnPlay() =>
            GameStart?.Invoke();
    }
}