using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRare;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private SpriteRenderer _renderer;
        private float _secondPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;


        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _secondPerFrame = 1f / _frameRare;
            _nextFrameTime = Time.time + _secondPerFrame;
            _currentSpriteIndex = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= _sprites.Length)
            {
                if (_loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    enabled = false;
                    _onComplete?.Invoke();
                    return;
                }
            }

            _renderer.sprite = _sprites[_currentSpriteIndex];
            _nextFrameTime += _secondPerFrame;
            _currentSpriteIndex++;
        }
    }
}