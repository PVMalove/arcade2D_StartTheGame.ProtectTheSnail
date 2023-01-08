using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;

namespace CodeBase.Infrastructure.States.StateMachine
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain,
            AllServices services)
        {
            _states[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services);
            _states[typeof(LoadProgressState)] = new LoadProgressState(this);
            _states[typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, curtain,
                services.Single<IGameFactory>());
            _states[typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, curtain,
                services.Single<IGameFactory>());
            _states[typeof(GameLoopState)] = new GameLoopState(this);
        }

        public void Start() => Enter<BootstrapState>();
        public void LoadScene(SceneNames sceneName) => Enter<LoadSceneState, SceneNames>(sceneName);
    }
}