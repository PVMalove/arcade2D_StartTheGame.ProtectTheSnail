using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States.StateMachine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public Game(LoadingCurtain curtain) => 
            _stateMachine = new GameStateMachine(new SceneLoader(), curtain, AllServices.Container);

        public void Start() => _stateMachine.Start();
    }
}