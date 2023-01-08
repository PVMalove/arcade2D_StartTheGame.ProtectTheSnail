using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services.Pool;
using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Elements;
using UnityEngine;
using UnityEngine.Assertions;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private RestartView _view;
        private readonly GameStateMachine _stateMachine;


        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _view = Object.FindObjectOfType<RestartView>();
            Assert.IsNotNull(_view, "Main menu view not found");
            _view.Restart += OnRestart;
        }

        private void OnRestart()
        {
            ObjectPool.DestroyAllPools();
            _stateMachine.Start();
        }

        public void Exit() => 
            _view.Restart -= OnRestart;
    }
}