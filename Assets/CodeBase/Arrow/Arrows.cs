using System;
using CodeBase.Infrastructure.Services.Pool;
using UniRx;
using UnityEngine;

namespace CodeBase.Arrow
{
    public class Arrows : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        [SerializeField] private GameObject[] _arrowSprite;
        [SerializeField] private Position _position;
        [SerializeField] private float _displayPeriod = 500;
        [SerializeField] private Spawner _mediator;

        private void OnEnable() =>
            ArrowShot();

        private void OnDisable()
        {
            if (_disposable != null)
                _disposable.Clear();
        }

        private void ArrowShot()
        {
            int arrow = 0;

            Observable.Interval(period: TimeSpan.FromMilliseconds(value: _displayPeriod)).Subscribe(onNext: _ =>
            {
                CheckCollision(arrow);
                ShowArrow(index: arrow);
                HideArrow(index: arrow);
                DespawnArrows(index: arrow);
                arrow++;
            }).AddTo(container: _disposable);
        }

        private void CheckCollision(int arrow)
        {
            if (IsLastArrow(index: arrow))
                _mediator.NotifyArrowCollision(arrow: _position);
        }

        private void ShowArrow(int index)
        {
            if (index >= _arrowSprite.Length)
                return;

            _arrowSprite[index].SetActive(true);
        }

        private void HideArrow(int index)
        {
            Observable.Timer(dueTime: TimeSpan.FromMilliseconds(value: _displayPeriod))
                .TakeUntilDisable(target: gameObject)
                .Subscribe(onNext: _ => { _arrowSprite[index].SetActive(false); });
        }

        private void DespawnArrows(int index)
        {
            if (index == _arrowSprite.Length)
            {
                ObjectPool.Despawn(toDespawn: gameObject);
                _disposable.Clear();
            }
        }

        private bool IsLastArrow(int index) => 
            index == _arrowSprite.Length - 1;
    }
}