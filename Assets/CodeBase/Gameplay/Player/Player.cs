using System;
using CodeBase.Gameplay.Arrow;
using CodeBase.Gameplay.Snail;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        private const int CountDamage = 1;
        private const int CountDiamond = 1;

        [SerializeField] private Spawner _spawner;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private Health _health;
        [SerializeField] private Diamond _diamond;

        public event Action<Position> DefenseFX;
        public event Action<Position> HitFX;


        private void OnEnable() =>
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
                _health.TakeDamage(CountDamage);
            }
        }
    }
}