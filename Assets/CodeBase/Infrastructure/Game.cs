using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using Plugins.Yandex.CodeBase;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, LoadingYandexSDK yandex)
        {
            stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, yandex, AllServices.Container);
        }
    }
}