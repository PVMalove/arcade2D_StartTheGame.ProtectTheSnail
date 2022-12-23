using System;
using System.Collections;
using CodeBase.Infrastructure.Presenter;
using CodeBase.Infrastructure.Services.Pool;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Arrow
{
    public class Spawner : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();
        
        private bool _isPlay;
        private DateTimeOffset _timeOffset;

        [SerializeField] private GameObject[] _arrows;
        [SerializeField] private Transform[] _spawnPoint;

        [Header("Interval spawn settings:")] 
        [SerializeField] private float _minInterval;
        [SerializeField] private float _maxInterval;
        [SerializeField] private float _interval;
        [SerializeField] private float _timeBetweenIntervals;
        [SerializeField] private float _nonLowerInterval;
        [SerializeField] private float _lowerInterval;

        [SerializeField] private Presenter _presenter;

        public event Action<Position> OnArrowCollision;

        private void OnEnable() => 
            _presenter.GameStart += OnGameStart;

        private void Start() => 
            UpdateSpawn();

        private void OnDisable()
        {
            if (_disposable != null)
                _disposable.Clear();

            _presenter.GameStart -= OnGameStart;
        }

        private void UpdateSpawn()
        {
            _timeOffset = DateTimeOffset.Now;

            Observable.EveryUpdate()
                .Timestamp()
                .Where(CheckTimer)
                .Subscribe(x =>
                {
                    SpawnArrow();
                    _timeOffset = x.Timestamp;
                }).AddTo(_disposable);
        }

        public void NotifyArrowCollision(Position arrow) =>
            OnArrowCollision?.Invoke(arrow);

        private void OnGameStart()
        {
            Debug.Log("OnGameStart is play - " + _isPlay);
            _isPlay = true;
            StartCoroutine(IntervalCounter());
        }

        private void SpawnArrow()
        {
            int index = Random.Range(0, _arrows.Length);
            ObjectPool.Spawn(_arrows[index], _spawnPoint[index].position, _arrows[index].transform.rotation);
        }

        private bool CheckTimer(Timestamped<long> x) =>
            x.Timestamp >= _timeOffset.AddSeconds(_interval) && _isPlay;

        private IEnumerator IntervalCounter()
        {
            yield return new WaitForSeconds(_nonLowerInterval);

            while (_isPlay)
            {
                yield return new WaitForSeconds(_timeBetweenIntervals);
                _interval -= _lowerInterval;

                _interval = Math.Clamp(_interval, _minInterval, _maxInterval);
            }
        }
    }
}