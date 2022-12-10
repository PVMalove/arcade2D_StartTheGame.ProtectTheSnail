using System;
using CodeBase.Infrastructure.Pool;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Arrow
{
    public class Spawner : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        [SerializeField] private GameObject[] _arrows;
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private int _spawnInterval = 1200;

        public event Action<Position> OnArrowCollision;

        private void Start() =>
            Spawn();

        private void OnDisable()
        {
            if (_disposable != null)
                _disposable.Clear();
        }

        private void Spawn()
        {
            Observable.Interval(TimeSpan.FromMilliseconds(_spawnInterval)).Subscribe(_ =>
            {
                int index = Random.Range(0, _arrows.Length);
                ObjectPool.Spawn(_arrows[index], _spawnPoint[index].position, _arrows[index].transform.rotation);
            }).AddTo(_disposable);
        }

        public void NotifyArrowCollision(Position arrow) => 
            OnArrowCollision?.Invoke(arrow);
    }
}