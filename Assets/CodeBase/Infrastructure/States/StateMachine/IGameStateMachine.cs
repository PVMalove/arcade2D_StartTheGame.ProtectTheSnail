using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States.Interface;

namespace CodeBase.Infrastructure.States.StateMachine
{
    public interface IGameStateMachine : IService
    {
        void Enter<T>() where T : class, IState;
        void Enter<T, TP>(TP payload) where T : class, IPayloadedState<TP>;
    }
}