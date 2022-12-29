using System;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Plugins.Yandex.CodeBase
{
    public static class YandexGamesSDK
    {
        public static bool CallbackLogging = false;

        private static Action _onInitializeSuccessCallback;

        public static bool IsInitialized => GetYandexGamesSdkIsInitialized();

        [DllImport("__Internal")]
        private static extern bool GetYandexGamesSdkIsInitialized();

        public static IEnumerator Initialize(Action onSuccessCallback = null)
        {
            _onInitializeSuccessCallback = onSuccessCallback;

            YandexGamesSdkInitialize(OnInitializeSuccessCallback);

            while (!IsInitialized)
                yield return null;
        }

        [DllImport("__Internal")]
        private static extern void YandexGamesSdkInitialize(Action successCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnInitializeSuccessCallback()
        {
            if (CallbackLogging)
                Debug.Log($"{nameof(YandexGamesSDK)}.{nameof(OnInitializeSuccessCallback)} invoked");

            _onInitializeSuccessCallback?.Invoke();
        }
    }
}