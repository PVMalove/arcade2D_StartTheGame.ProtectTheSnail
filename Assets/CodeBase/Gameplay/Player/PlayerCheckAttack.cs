using System;
using CodeBase.Data;
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

        private Spawner _spawner;
        private WorldData _worldData;
        
        public event Action<Position> DefenseFX;
        public event Action<Position> HitFX;

        public void Construct(Spawner spawner, WorldData worldData)
        {
            _spawner = spawner;
            _worldData = worldData;
        }

        private void Start() => 
            _spawner.OnArrowCollision += ArrowCollision;

        private void OnDisable() =>
            _spawner.OnArrowCollision -= ArrowCollision;

        private void ArrowCollision(Position arrowPosition)
        {
            if (_player.Position == arrowPosition)
            {
                DefenseFX?.Invoke(_player.Position);
                _worldData.DiamondData.Add(CountDiamond);
            }
            else
            {
                HitFX?.Invoke(arrowPosition);
                playerHealth.TakeDamage(CountDamage);
            }
        }
    }
}