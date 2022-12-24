using CodeBase.Arrow;
using UnityEngine;

namespace CodeBase.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private PlayerMovement _player;
      
        private void OnEnable() => 
            _spawner.OnArrowCollision += ArrowCollision;

        private void OnDisable() =>
            _spawner.OnArrowCollision -= ArrowCollision;

        private void ArrowCollision(Position obj)
        {
            if (_player.Position == obj)
                Debug.Log("Scores");
            else
                Debug.Log("Dead");
        }
    }
}