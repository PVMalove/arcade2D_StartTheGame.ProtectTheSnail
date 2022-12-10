using CodeBase.Arrow;
using UnityEngine;

namespace CodeBase.Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private PlayerMove _player;

        private void OnEnable() => 
            _spawner.OnArrowCollision += ArrowCollision;

        private void OnDisable() => 
            _spawner.OnArrowCollision -= ArrowCollision;

        private void ArrowCollision(Position obj)
        {
            if(_player.Position == obj)
                Debug.Log("Scores " + _player.Position + " - " + obj);
            else
                Debug.Log("Dead "+ _player.Position + " - " + obj);
        }
    }
}