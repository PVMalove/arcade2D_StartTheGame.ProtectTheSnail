using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.UI.Services.UIFactory;

namespace CodeBase.Infrastructure.States.StateMachine
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain,
            AllServices services)
        {
            _states[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services);
            _states[typeof(LoadProgressState)] = new LoadProgressState(this, sceneLoader);
            _states[typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, curtain,
                services.Single<IGameFactory>(),
                services.Single<IUIFactory>());
            _states[typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, curtain,
                services.Single<IGameFactory>(),
                services.Single<IUIFactory>());
            _states[typeof(GameLoopState)] = new GameLoopState(this);
        }

        public void Start() => Enter<BootstrapState>();
    }
}