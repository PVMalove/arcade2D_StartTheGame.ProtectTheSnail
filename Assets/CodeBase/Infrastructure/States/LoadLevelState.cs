using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Loud(sceneName, OnLoaded);

        public void Exit() { }

        private void OnLoaded()
        {
            InitGameWorld();
        }

        private void InitGameWorld()
        {
            InitPresenter();

            InitPlayer();
            InitSpawner();
        }

        private void InitPresenter()
        {
            GameObject prefab = Resources.Load<GameObject>("Infrastructure/Presenter");
            Object.Instantiate(prefab);
        } 

        private void InitSpawner()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetAddress.SpawnerPath);
            Object.Instantiate(prefab);
        }

        private void InitPlayer()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetAddress.PlayerPath);
            Object.Instantiate(prefab);
        }
    }
}