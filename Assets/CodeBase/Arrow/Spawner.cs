using System;
using System.Collections;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Presenter;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Arrow
{
    public class Spawner : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();
        private bool _isPlay;

        [SerializeField] private GameObject[] _arrows;
        [SerializeField] private Transform[] _spawnPoint;

        [SerializeField] private DesktopPresenter _presenter;

        [Header("Interval spawn settings:")]
        [SerializeField] private int _minSpawnInterval;
        [SerializeField] private int _maxSpawnInterval;
        [SerializeField] private int _spawnInterval;
        [SerializeField] private int _boostSpawnInterval;
        [SerializeField] private float _nonBoostSpawnIntervalTime;

        public event Action<Position> OnArrowCollision;

        private void Start()
        {
            Spawn();
        }

        private void OnEnable()
        {
            _presenter.GameStart += OnGameStart;
        }

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

        private void OnGameStart()
        {
            _isPlay = true;
            StartCoroutine(SpawnIntervalCounter());
        }

        private IEnumerator SpawnIntervalCounter()
        {
            yield return new WaitForSeconds(_nonBoostSpawnIntervalTime);

            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                _spawnInterval -= _boostSpawnInterval;

                _spawnInterval = Math.Clamp(_spawnInterval, _minSpawnInterval, _maxSpawnInterval);
            }
        }
    }
}