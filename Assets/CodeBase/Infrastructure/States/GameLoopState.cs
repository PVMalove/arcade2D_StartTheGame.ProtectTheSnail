using CodeBase.Infrastructure.States.StateMachine;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public GameLoopState(GameStateMachine stateMachine)
        {
        }

        public void Enter()
        {
            Debug.Log("GameLoopState");
        }

        public void Exit()
        {
        }
    }
}