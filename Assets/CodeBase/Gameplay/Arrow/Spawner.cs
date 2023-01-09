﻿using System;
using CodeBase.Infrastructure.Services.PauseService;
using CodeBase.Infrastructure.Services.Pool;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.UI.Elements;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Arrow
{
    public class Spawner : MonoBehaviour, IPauseHandler
    {
        private readonly CompositeDisposable _disposable = new();

        [SerializeField] private GameObject[] _arrows;
        [SerializeField] private Transform[] _spawnPoint;

        [Header("Interval spawn settings:")]
        [SerializeField] private float _minInterval;
        [SerializeField] private float _maxInterval;
        [SerializeField] private float _interval;
        [SerializeField] private float _timeBetweenIntervals;
        [SerializeField] private float _nonLowerInterval;
        [SerializeField] private float _lowerInterval;

        private IRandomService _randomService;
        private DateTimeOffset _timeOffset;
        private CloseButtonView _TutorialView;
        public bool _isPlay;
        private IScheduler _mainThreadIgnoreTimeScale;
        private bool _isPause;

        public event Action<Position> OnArrowCollision;

        public void Construct(IRandomService randomService) =>
            _randomService = randomService;

        private void OnEnable() =>
            OnGameStart();

        private void Start() =>
            UpdateSpawn();

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
                    Debug.Log("Interval spawn- " + _interval);
                }).AddTo(_disposable);
        }

        public void NotifyArrowCollision(Position arrow) =>
            OnArrowCollision?.Invoke(arrow);

        private void SpawnArrow()
        {
            int index = _randomService.Next(0, _arrows.Length);
            ObjectPool.Spawn(_arrows[index], _spawnPoint[index].position, _arrows[index].transform.rotation);
        }

        private bool CheckTimer(Timestamped<long> x)
        {
            if (_isPause)
            {
                if (_disposable != null)
                    _disposable.Clear();
                Debug.Log($"Spawner - _isPause {_isPause}");
                return false;
            }

            return x.Timestamp >= _timeOffset.AddSeconds(_interval) && _isPlay;
        }

        private void OnGameStart()
        {
            _isPlay = true;
            IntervalCounter().Forget();
        }

        private async UniTaskVoid IntervalCounter()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_nonLowerInterval));

            while (_isPlay)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_timeBetweenIntervals));

                _interval -= _lowerInterval;
                _interval = Math.Clamp(_interval, _minInterval, _maxInterval);
            }
        }

        public void OnPauseChanged(bool isPaused) =>
            _isPause = isPaused;
    }
}