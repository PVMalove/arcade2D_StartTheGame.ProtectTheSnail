using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.States.Interface;

namespace CodeBase.Infrastructure.States.StateMachine
{
    public abstract class StateMachine : IGameStateMachine

    {
    private protected readonly Dictionary<Type, IExitState> _states;
    private IExitState _activeState;

    protected StateMachine()
    {
        DefaultState defaultState = new DefaultState();
        _activeState = defaultState;
        _states = new Dictionary<Type, IExitState> { [typeof(DefaultState)] = defaultState };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
        TState state = ChangeState<TState>();
        state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitState
    {
        _activeState?.Exit();

        TState state = GetState<TState>();
        _activeState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitState =>
        _states[typeof(TState)] as TState;
    }
}