using CodeBase.Infrastructure.Services.Pool;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class FX : MonoBehaviour
    {
        [SerializeField] private PlayerCheckAttack playerCheckAttack;
        
        [Header("Effects settings:")] 
        [SerializeField] private GameObject _diamondFX;
        [SerializeField] private GameObject _HitFX;

        [Space]
        [SerializeField] private Transform _fxPointRightTop;
        [SerializeField] private Transform _fxPointRightDown;
        [SerializeField] private Transform _fxPointLeftTop;
        [SerializeField] private Transform _fxPointLeftDown;

        private void OnEnable()
        {
            playerCheckAttack.DefenseFX += PlayFXDefense;
            playerCheckAttack.HitFX += PlayFXHit;
        }

        private void OnDisable()
        {
            playerCheckAttack.DefenseFX -= PlayFXDefense;
            playerCheckAttack.HitFX -= PlayFXHit;
        }

        private void PlayFXDefense(Position playerPosition)
        {
            switch (playerPosition)
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
        
        private void PlayFXHit(Position arrowPosition)
        {
            switch (arrowPosition)
            {
                case Position.RightTop:
                    SpawnFX(_HitFX, _fxPointRightTop);
                    break;
                case Position.RightDown:
                    SpawnFX(_HitFX, _fxPointRightDown);
                    break;
                case Position.LeftTop:
                    SpawnFX(_HitFX, _fxPointLeftTop);
                    break;
                case Position.LeftDown:
                    SpawnFX(_HitFX, _fxPointLeftDown);
                    break;
            }
        }

        private void SpawnFX(GameObject fx, Transform point) =>
            ObjectPool.Spawn(fx, point.position);
    }
}