using System;
using System.Collections;
using Plugins.Yandex.CodeBase.Account;
using UnityEngine;

namespace Plugins.Yandex.CodeBase
{
    public class LoadingYandexSDK : MonoBehaviour
    {
        public bool IsInitialized { get; private set; }
        public bool IsAuthorized => IsInitialized && PlayerAccount.IsAuthorized; 
        public bool HasPersonalDataPermission => PlayerAccount.HasPersonalProfileDataPermission;

        private void Awake()
        {
            Load();
            DontDestroyOnLoad(this);
        }

        public void Load() => 
            StartCoroutine(Initialize());

        public void UserRegistry() => 
            ConnectProfile(OnAuthorizationSuccess, OnAuthorizationError);


        private void ConnectProfile(Action onSuccess, Action<string> onError)
        {
            if (IsInitialized == false)
            {
                onError?.Invoke($"[YandexSDK]: connect to profile failed! SDK not initialized;");
                return;
            }

            PlayerAccount.Authorize(onSuccess, onError);
        }

        private IEnumerator Initialize()
        {
#if UNITY_WEBGL || UNITY_EDITOR
#if YANDEX_GAMES
            Debug.Log("[YandexSDK] start initializing...");
            yield return InitializeSDK(OnSDKInitializedSuccess);
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

        private void OnSDKInitializedSuccess()
        {
            IsInitialized = true;
            Debug.Log("[YandexSDK] successful initialized.");
        }

        private void OnAuthorizationSuccess() => 
            PlayerAccount.RequestPersonalProfileDataPermission();

        private void OnAuthorizationError(string text) => 
            Debug.Log($"Error authorization: {text}");

        public void GetProfileData()
        {
            PlayerAccount.GetProfileData((result) =>
            {
                string name = result.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";

                Debug.Log($"My id = {result.uniqueID}, name = {name}");
            });
        }
    }
}