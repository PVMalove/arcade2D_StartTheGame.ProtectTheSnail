using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Pool;
using CodeBase.Infrastructure.Services.Randomizer;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Arrow
{
    public class Spawner : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        private IRandomService _randomService;
        private ITimerService _timerService;
        private DateTimeOffset _timeOffset;

        private bool _isPlay;

        [SerializeField] private GameObject[] _arrows;
        [SerializeField] private Transform[] _spawnPoint;


        [Header("Interval spawn settings:")] 
        [SerializeField] private float _minInterval;

        [SerializeField] private float _maxInterval;
        [SerializeField] private float _interval;
        [SerializeField] private float _timeBetweenIntervals;
        [SerializeField] private float _nonLowerInterval;
        [SerializeField] private float _lowerInterval;

        public event Action<Position> OnArrowCollision;

        public void Construct(IRandomService randomService, ITimerService timerService)
        {
            _randomService = randomService;
            _timerService = timerService;
        }


        private void Start()
        {
            OnGameStart();
            UpdateSpawn();
        }

        private void OnDisable()
        {
            if (_disposable != null)
                _disposable.Clear();
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

        private void SpawnArrow()
        {
            int index = _randomService.Next(0, _arrows.Length);
            ObjectPool.Spawn(_arrows[index], _spawnPoint[index].position, _arrows[index].transform.rotation);
        }

        private bool CheckTimer(Timestamped<long> x) =>
            x.Timestamp >= _timeOffset.AddSeconds(_interval) && _isPlay;

        private void OnGameStart()
        {
            _isPlay = true;
            //StartCoroutine(_timerService.IntervalCounter());
            StartCoroutine(IntervalCounter());
        }

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