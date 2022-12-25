using CodeBase.Arrow;
using CodeBase.Infrastructure.Services.Pool;
using CodeBase.Snail;
using UnityEngine;

namespace CodeBase.Player
{
    public class Player : MonoBehaviour
    {
        private const int Damage = 1;
        
        [SerializeField] private Spawner _spawner;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private SnailHealth _snailHealth;

        [Header("Effects settings:")] 
        [SerializeField] private GameObject _diamondFX;
        [SerializeField] private GameObject _deathFX;

        [Space]
        [SerializeField] private Transform _fxPointRightTop;
        [SerializeField] private Transform _fxPointRightDown;
        [SerializeField] private Transform _fxPointLeftTop;
        [SerializeField] private Transform _fxPointLeftDown;


        private void OnEnable() =>
            _spawner.OnArrowCollision += ArrowCollision;

        private void OnDisable() =>
            _spawner.OnArrowCollision -= ArrowCollision;

        private void ArrowCollision(Position arrowPosition)
        {
            if (_player.Position == arrowPosition)
            {
                PlayFXReward();
            }
            else
            {
                _snailHealth.TakeDamage(Damage);
                PlayFXDead(arrowPosition);
            }
        }

        private void PlayFXReward()
        {
            switch (_player.Position)
            {
                case Position.RightTop:
                    SpawnFX(_diamondFX, _fxPointRightTop);
                    break;
                case Position.RightDown:
                    SpawnFX(_diamondFX, _fxPointRightDown);
                    break;
                case Position.LeftTop:
                    SpawnFX(_diamondFX, _fxPointLeftTop);
                    break;
                case Position.LeftDown:
                    SpawnFX(_diamondFX, _fxPointLeftDown);
                    break;
            }
        }

        private void PlayFXDead(Position arrowPosition)
        {
            switch (arrowPosition)
            {
                case Position.RightTop:
                    SpawnFX(_deathFX, _fxPointRightTop);
                    break;
                case Position.RightDown:
                    SpawnFX(_deathFX, _fxPointRightDown);
                    break;
                case Position.LeftTop:
                    SpawnFX(_deathFX, _fxPointLeftTop);
                    break;
                case Position.LeftDown:
                    SpawnFX(_deathFX, _fxPointLeftDown);
                    break;
            }
        }

        private void SpawnFX(GameObject fx, Transform point) => 
            ObjectPool.Spawn(fx, point.position);
    }
}