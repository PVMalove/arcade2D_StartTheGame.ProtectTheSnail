using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Plugins.Yandex.CodeBase.Account
{
    public static class PlayerAccount
    {
        private static Action s_onAuthorizeSuccessCallback;
        private static Action<string> s_onAuthorizeErrorCallback;
        private static Action s_onRequestPersonalProfileDataPermissionSuccessCallback;
        private static Action<string> s_onRequestPersonalProfileDataPermissionErrorCallback;
        private static Action<PlayerAccountProfileDataResponse> s_onGetProfileDataSuccessCallback;
        private static Action<string> s_onGetProfileDataErrorCallback;
        private static Action s_onSetPlayerDataSuccessCallback;
        private static Action<string> s_onSetPlayerDataErrorCallback;
        private static Action<string> s_onGetPlayerDataSuccessCallback;
        private static Action<string> s_onGetPlayerDataErrorCallback;

        public static bool IsAuthorized => GetPlayerAccountIsAuthorized();

        [DllImport("__Internal")]
        private static extern bool GetPlayerAccountIsAuthorized();

        public static bool HasPersonalProfileDataPermission => GetPlayerAccountHasPersonalProfileDataPermission();

        [DllImport("__Internal")]
        private static extern bool GetPlayerAccountHasPersonalProfileDataPermission();


        public static void Authorize(Action onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onAuthorizeSuccessCallback = onSuccessCallback;
            s_onAuthorizeErrorCallback = onErrorCallback;

            PlayerAccountAuthorize(OnAuthorizeSuccessCallback, OnAuthorizeErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void PlayerAccountAuthorize(Action successCallback, Action<string> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnAuthorizeSuccessCallback()
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnAuthorizeSuccessCallback)} invoked");

            s_onAuthorizeSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnAuthorizeErrorCallback(string errorMessage)
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log(
                    $"{nameof(PlayerAccount)}.{nameof(OnAuthorizeErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onAuthorizeErrorCallback?.Invoke(errorMessage);
        }

        public static void RequestPersonalProfileDataPermission(Action onSuccessCallback = null,
            Action<string> onErrorCallback = null)
        {
            s_onRequestPersonalProfileDataPermissionSuccessCallback = onSuccessCallback;
            s_onRequestPersonalProfileDataPermissionErrorCallback = onErrorCallback;

            PlayerAccountRequestPersonalProfileDataPermission(OnRequestPersonalProfileDataPermissionSuccessCallback,
                OnRequestPersonalProfileDataPermissionErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void PlayerAccountRequestPersonalProfileDataPermission(Action successCallback,
            Action<string> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnRequestPersonalProfileDataPermissionSuccessCallback()
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log(
                    $"{nameof(PlayerAccount)}.{nameof(OnRequestPersonalProfileDataPermissionSuccessCallback)} invoked");

            s_onRequestPersonalProfileDataPermissionSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnRequestPersonalProfileDataPermissionErrorCallback(string errorMessage)
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log(
                    $"{nameof(PlayerAccount)}.{nameof(OnRequestPersonalProfileDataPermissionErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onRequestPersonalProfileDataPermissionErrorCallback?.Invoke(errorMessage);
        }

        public static void GetProfileData(Action<PlayerAccountProfileDataResponse> onSuccessCallback,
            Action<string> onErrorCallback = null)
        {
            s_onGetProfileDataSuccessCallback = onSuccessCallback;
            s_onGetProfileDataErrorCallback = onErrorCallback;

            PlayerAccountGetProfileData(OnGetProfileDataSuccessCallback, OnGetProfileDataErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void PlayerAccountGetProfileData(Action<string> successCallback,
            Action<string> errorCallback);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetProfileDataSuccessCallback(string profileDataResponseJson)
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log(
                    $"{nameof(PlayerAccount)}.{nameof(OnGetProfileDataSuccessCallback)} invoked, {nameof(profileDataResponseJson)} = {profileDataResponseJson}");

            PlayerAccountProfileDataResponse profileDataResponse =
                JsonUtility.FromJson<PlayerAccountProfileDataResponse>(profileDataResponseJson);

            s_onGetProfileDataSuccessCallback?.Invoke(profileDataResponse);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetProfileDataErrorCallback(string errorMessage)
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log(
                    $"{nameof(PlayerAccount)}.{nameof(OnGetProfileDataErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetProfileDataErrorCallback?.Invoke(errorMessage);
        }

        public static void SetPlayerData(string playerDataJson, Action onSuccessCallback = null,
            Action<string> onErrorCallback = null)
        {
            if (playerDataJson == null)
                throw new ArgumentNullException(nameof(playerDataJson));

            if (string.IsNullOrEmpty(playerDataJson))
                playerDataJson = "{}";

            s_onSetPlayerDataSuccessCallback = onSuccessCallback;
            s_onSetPlayerDataErrorCallback = onErrorCallback;

            PlayerAccountSetPlayerData(playerDataJson, OnSetPlayerDataSuccessCallback, OnSetPlayerDataErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void PlayerAccountSetPlayerData(string playerDataJson, Action successCallback,
            Action<string> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSetPlayerDataSuccessCallback()
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnSetPlayerDataSuccessCallback)} invoked");

            s_onSetPlayerDataSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnSetPlayerDataErrorCallback(string errorMessage)
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log(
                    $"{nameof(PlayerAccount)}.{nameof(OnSetPlayerDataErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onSetPlayerDataErrorCallback?.Invoke(errorMessage);
        }

        public static void GetPlayerData(Action<string> onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onGetPlayerDataSuccessCallback = onSuccessCallback;
            s_onGetPlayerDataErrorCallback = onErrorCallback;

            PlayerAccountGetPlayerData(OnGetPlayerDataSuccessCallback, OnGetPlayerDataErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void PlayerAccountGetPlayerData(Action<string> successCallback,
            Action<string> errorCallback);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPlayerDataSuccessCallback(string playerDataJson)
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log(
                    $"{nameof(PlayerAccount)}.{nameof(OnGetPlayerDataSuccessCallback)} invoked, {nameof(playerDataJson)} = {playerDataJson}");

            s_onGetPlayerDataSuccessCallback?.Invoke(playerDataJson);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPlayerDataErrorCallback(string errorMessage)
        {
            if (YandexGamesSDK.CallbackLogging)
                Debug.Log(
                    $"{nameof(PlayerAccount)}.{nameof(OnGetPlayerDataErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetPlayerDataErrorCallback?.Invoke(errorMessage);
        }
    }
}