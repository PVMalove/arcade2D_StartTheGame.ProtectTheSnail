using System;
using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.UI.Elements;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class BaseWindow : MonoBehaviour
    {
        [SerializeField] private CloseButtonView _closeButton;
        
        private IPauseService _pauseService;

        protected void Construct(IPauseService pauseService) => 
            _pauseService = pauseService;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
            _pauseService.SetPause(true);
        }

        private void OnDestroy()
        {
            _closeButton.CloseWindow -= Hide;
            UnsubscribeUpdates();
        }

        private void OnAwake() =>
            _closeButton.CloseWindow += Hide;

        private void Hide()
        {
            _pauseService.SetPause(false);
            Destroy(gameObject);
        }

        protected virtual void Initialize() {}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnsubscribeUpdates(){}
    }
}