namespace CodeBase.Infrastructure.States.Interface
{
    public interface IState : IExitState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayloaded> : IExitState
    {
        void Enter(TPayloaded payload);
    }

    public interface IExitState
    {
        void Exit();
    }
}