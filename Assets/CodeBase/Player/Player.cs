using CodeBase.Arrow;
using CodeBase.Infrastructure.Services.Pool;
using UnityEngine;

namespace CodeBase.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private PlayerMovement _player;

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
                switch (_player.Position)
                {
                    case Position.RightTop:
                        ObjectPool.Spawn(_diamondFX, _fxPointRightTop.position);
                        break;
                    case Position.RightDown:
                        ObjectPool.Spawn(_diamondFX, _fxPointRightDown.position);
                        break;
                    case Position.LeftTop:
                        ObjectPool.Spawn(_diamondFX, _fxPointLeftTop.position);
                        break;
                    case Position.LeftDown:
                        ObjectPool.Spawn(_diamondFX, _fxPointLeftDown.position);
                        break;
                }
            }

            else
            {
                switch (arrowPosition)
                {
                    case Position.RightTop:
                        ObjectPool.Spawn(_deathFX, _fxPointRightTop.position);
                        break;
                    case Position.RightDown:
                        ObjectPool.Spawn(_deathFX, _fxPointRightDown.position);
                        break;
                    case Position.LeftTop:
                        ObjectPool.Spawn(_deathFX, _fxPointLeftTop.position);
                        break;
                    case Position.LeftDown:
                        ObjectPool.Spawn(_deathFX, _fxPointLeftDown.position);
                        break;
                }
            }
        }
    }
}