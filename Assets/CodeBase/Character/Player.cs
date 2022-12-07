using CodeBase.Arrow;
using UnityEngine;

namespace CodeBase.Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;

        private void OnEnable() => 
            _spawner.OnArrowCollision += ArrowCollision;

        private void OnDisable() => 
            _spawner.OnArrowCollision -= ArrowCollision;

        private void ArrowCollision(Position obj)
        {
            Debug.Log("Player " + obj);
        }
    }
}