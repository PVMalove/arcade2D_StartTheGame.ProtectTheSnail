namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayloaded> : IExitState
    {
        void Enter(TPayloaded payloaded);
    }

    public interface IExitState
    {
        void Exit();
    }
}