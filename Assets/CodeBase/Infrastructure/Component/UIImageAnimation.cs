using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Component
{
    [RequireComponent(typeof(Image))]
    public class UIImageAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRare;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;

        private Image _renderer;
        private float _secondPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;
        private bool _isPaused;

        private void OnEnable()
        {
            _secondPerFrame = 1f / _frameRare;
            _nextFrameTime = Time.time + _secondPerFrame;
            _currentSpriteIndex = 0;
        }

        private void Start() => 
            _renderer = GetComponent<Image>();

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
                    return;
                }
            }

            _renderer.sprite = _sprites[_currentSpriteIndex];
            _nextFrameTime += _secondPerFrame;
            _currentSpriteIndex++;
        }
    }
}