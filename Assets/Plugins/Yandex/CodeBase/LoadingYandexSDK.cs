using System;
using System.Collections;
using UnityEngine;

namespace Plugins.Yandex.CodeBase
{
    public class LoadingYandexSDK : MonoBehaviour
    {
        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Loud() => 
            StartCoroutine(Initialize());

        private IEnumerator Initialize()
        {
#if UNITY_WEBGL || UNITY_EDITOR
#if YANDEX_GAMES
            Debug.Log("[YandexSDK] start initializing...");
            yield return InitializeSDK(OnSDKInitialized);
#endif
#endif
        }

        private IEnumerator InitializeSDK(Action onSuccessCallback)
        {
#if YANDEX_GAMES && UNITY_EDITOR
            yield break;
#elif !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#else
            YandexGamesSDK.CallbackLogging = true;
            yield return YandexGamesSDK.Initialize(onSuccessCallback);
#endif
        }

        private void OnSDKInitialized()
        {
            Debug.Log("[YandexSDK] successful initialized.");
        }
    }
}