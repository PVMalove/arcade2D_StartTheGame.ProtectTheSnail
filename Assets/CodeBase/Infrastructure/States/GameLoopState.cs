using CodeBase.Infrastructure.States.Interface;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.UI.Elements;
using CodeBase.UI.Elements.View;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private RestartButtonView _buttonView;
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        { }

        public void Exit()
        {
            Debug.Log("Exit loop");
        }
    }
}