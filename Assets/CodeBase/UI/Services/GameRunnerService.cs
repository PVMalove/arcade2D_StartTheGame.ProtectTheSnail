using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Pool;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.States.StateMachine;

namespace CodeBase.UI.Services
{
    public class GameRunnerService : IGameRunnerService
    {
        private readonly IGameStateMachine _stateMachine;

        public GameRunnerService(IGameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void GoLoadScene()
        {
            if (_stateMachine != null) 
                _stateMachine.Enter<LoadSceneState, SceneNames>(SceneNames.GameScene);
        }
        
        public void Restart()
        {
            if (_stateMachine != null)
            {
                ObjectPool.DestroyAllPools();
                _stateMachine.Enter<BootstrapState>();
            }
        }
    }
}