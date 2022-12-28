using System;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Plugins.Yandex.CodeBase
{
    public static class YandexGamesSDK
    {
        private static Action _onInitializeSuccessCallback;

        /// <summary>
        /// Enable it to log SDK callbacks in the console.
        /// </summary>
        public static bool CallbackLogging = false;

        /// <summary>
        /// SDK is initialized automatically on load.
        /// If either something fails or called way too early, this will return false.
        /// </summary>
        public static bool IsInitialized => GetYandexGamesSdkIsInitialized();

        [DllImport("__Internal")]
        private static extern bool GetYandexGamesSdkIsInitialized();
      
        /// <summary>
        /// Invoke this and wait for coroutine to finish before using any SDK methods.<br/>
        /// Downloads Yandex SDK script and inserts it into the HTML page.
        /// </summary>
        /// <returns>Coroutine waiting for <see cref="IsInitialized"/> to return true.</returns>
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
