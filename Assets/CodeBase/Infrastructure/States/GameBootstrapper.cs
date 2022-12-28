using CodeBase.Infrastructure.Loader;
using Plugins.Yandex.CodeBase;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtain;
        [SerializeField] private LoadingYandexSDK _yandex;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _curtain, _yandex);
            _game.stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}

