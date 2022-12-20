using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Presenter
{
    public class DesktopPresenter : MonoBehaviour
    {
        public event Action GameStart;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
                OnPlay();
        }

        private void OnPlay() => 
            GameStart?.Invoke();
    }
}