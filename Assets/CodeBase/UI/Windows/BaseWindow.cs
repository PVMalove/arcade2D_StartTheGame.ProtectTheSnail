using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.UI.Elements.View;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class BaseWindow : MonoBehaviour
    {
        [SerializeField] private bool _needSetPause = true;
        [SerializeField] private CloseWindowButtonView closeButton;

        private IPauseService _pauseService;

        protected void Construct(IPauseService pauseService) => 
            _pauseService = pauseService;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
            _pauseService.SetPause(_needSetPause);
        }

        private void OnDestroy()
        {
            closeButton.CloseWindow -= Hide;
            UnsubscribeUpdates();
        }

        private void OnAwake() =>
            closeButton.CloseWindow += Hide;

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