using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Loader
{
    public class LoadingCurtain : MonoBehaviour
    {
        private CanvasGroup _curtain;

        private void Awake()
        {
            _curtain = GetComponent<CanvasGroup>();
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1f;
        }

        public void Hide() =>
            FadeIn();

        private async void FadeIn()
        {
            float fadeStep = 0.05f;
            
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= fadeStep;
                await UniTask.Delay(30);
            }
            
            Destroy(gameObject);
        }
    }
}