using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.UI.Services.UIFactory;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States.StateMachine
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain,
            AllServices services)
        {
            _states[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services);
            _states[typeof(LoadProgressState)] = new LoadProgressState(this, sceneLoader,
                services.Single<IPersistentProgressService>(),
                services.Single<ISaveLoadService>());
            _states[typeof(MainMenuState)] = new MainMenuState(sceneLoader, curtain,
                services.Single<IUIFactory>(),
                services.Single<IWindowService>());
            _states[typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, curtain,
                services.Single<IGameFactory>(),
                services.Single<IUIFactory>());
            _states[typeof(GameLoopState)] = new GameLoopState(this);
        }

        public void Start() => Enter<BootstrapState>();
    }
}