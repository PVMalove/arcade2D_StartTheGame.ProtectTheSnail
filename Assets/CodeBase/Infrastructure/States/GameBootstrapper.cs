using CodeBase.Infrastructure.Loader;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(Instantiate(_curtainPrefab));
            _game.Start();
            DontDestroyOnLoad(this);
        }
    }
}

