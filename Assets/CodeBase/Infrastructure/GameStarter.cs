using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using Plugins.Yandex.CodeBase;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;
        [SerializeField] private LoadingYandexSDK _yandex;

        private void Awake()
        {
            LoadingYandexSDK loadingYandexSDK = FindObjectOfType<LoadingYandexSDK>();
            if (loadingYandexSDK == null)
                Instantiate(_yandex);

            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();
            if (bootstrapper == null)
                Instantiate(_bootstrapperPrefab);

            Destroy(gameObject);
        }
    }
}