using System;
using CodeBase.Gameplay.Arrow;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class PlayerCheckAttack : MonoBehaviour
    {
        private const int CountDamage = 1;
        private const int CountDiamond = 1;

        [SerializeField] private PlayerMovement _player;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private Diamond _diamond;

        private Spawner _spawner;
        
        public event Action<Position> DefenseFX;
        public event Action<Position> HitFX;


        public void Construct(Spawner spawner) => 
            _spawner = spawner;

        private void Start() => 
            _spawner.OnArrowCollision += ArrowCollision;

        private void OnDisable() =>
            _spawner.OnArrowCollision -= ArrowCollision;

        private void ArrowCollision(Position arrowPosition)
        {
            if (_player.Position == arrowPosition)
            {
                DefenseFX?.Invoke(_player.Position);
                _diamond.Add(CountDiamond);
            }
            else
            {
                HitFX?.Invoke(arrowPosition);
                playerHealth.TakeDamage(CountDamage);
            }
        }
    }
}