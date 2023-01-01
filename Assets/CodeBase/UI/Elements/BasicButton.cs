using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{ 
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class BasicButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private bool _needAnimateOnClick = true;
        [SerializeField] private TextMeshProUGUI _text;

        private Button _button;

        private RectTransform _rectTransform;
        private CanvasGroup _group;

        private Vector3 _baseScale;

        private Sequence _pressTween;
        private Tween _appearTween;
        private Sequence _selectTween;


        private Button.ButtonClickedEvent _onClick = new();
        private ReactiveProperty<bool> _isDown = new(false);

        private event Action _downCallback;
        private event Action _upCallback;
        private event Action _soundClickCallback;

        public Button.ButtonClickedEvent OnClick => _button ? _button.onClick : _onClick;


        public void Awake()
        {
            if (!_button)
                _button = GetComponent<Button>();

            _rectTransform = GetComponent<RectTransform>();

            _baseScale = _rectTransform.localScale;

            _group = GetComponent<CanvasGroup>();
        }

        private void OnDestroy()
        {
            _selectTween?.Kill();
            _selectTween = null;
        }


        public void SetOnClick(Action onClick)
        {
            OnClick.RemoveAllListeners();
            OnClick.AddListener(() => onClick?.Invoke());
        }

        public void AddListener(Action onClick)
        {
            OnClick.AddListener(() => onClick?.Invoke());
        }

        public void RemoveListener(Action onClick)
        {
            OnClick.RemoveListener(() => onClick?.Invoke());
        }

        public void RemoveAllListeners()
        {
            OnClick.RemoveAllListeners();
        }
       
        public void OnSelectAnimation()
        {
            if (!gameObject) return;
            _selectTween?.Kill();

            _selectTween = DOTween.Sequence();
            _selectTween.Append(_rectTransform.DOScale(new Vector3(_baseScale.x * 0.95f, _baseScale.y * 1.01f, _baseScale.z), 0.05f));
            _selectTween.Append(_rectTransform.DOScale(new Vector3(_baseScale.x * 1.05f, _baseScale.y * 0.9f, _baseScale.z), 0.05f));
            _selectTween.Append(_rectTransform.DOScale(new Vector3(_baseScale.x * 0.95f, _baseScale.y * 1.01f, _baseScale.z), 0.05f));
            _selectTween.Append(_rectTransform.DOScale(_baseScale, 0.05f));
            _selectTween.OnComplete(() => _selectTween = null);
            _selectTween.SetLink(gameObject).SetUpdate(true);

            _selectTween.Play();
        }

        public void OnPressAnimationStart()
        {
            if (!gameObject) return;

            if (_pressTween != null)
            {
                _pressTween.Rewind();
                _pressTween.Play();
                return;
            }

            _pressTween = DOTween.Sequence()
                .Append(_rectTransform.DOScale(new Vector3(_baseScale.x * 0.95f, _baseScale.y * .95f, _baseScale.z), 0.05f));

            _pressTween.SetLink(gameObject).SetUpdate(true);
            _pressTween.SetAutoKill(false);

            _pressTween.Play();
        }

        public void OnPressAnimationFinished()
        {
            if (!gameObject) return;

            if (_pressTween != null)
            {
                _pressTween.Rewind();
            }
        }

        public void OnPointerClick(PointerEventData eventData) { }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDown.Value = true;

            _downCallback?.Invoke();

            if (_needAnimateOnClick)
                OnPressAnimationStart();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDown.Value = false;

            _upCallback?.Invoke();

            float dragDistance = Vector2.Distance(eventData.pressPosition, eventData.position);
            var dragThreshold = Screen.dpi / 12;
            var isDrag = dragDistance > dragThreshold;

            var parentCanvas = GetComponentInParent<CanvasGroup>();
            if (parentCanvas != null && !parentCanvas.interactable) return;

            if (isDrag)
            {
                if (_needAnimateOnClick)
                    OnPressAnimationFinished();
                return;
            }

            if (_needAnimateOnClick)
                OnSelectAnimation();

            if (!_button)
                _onClick?.Invoke();
        }
    }
}
