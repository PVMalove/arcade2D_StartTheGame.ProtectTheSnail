using CodeBase.UI.Elements;
using Plugins.Yandex.CodeBase;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private BasicButton _startButton;
        [SerializeField] private GameObject _bootstrapperPrefab;
        [SerializeField] private LoadingYandexSDK _yandex;

        private void Awake()
        {
            _yandex.Load();
        }

        private void Start()
        {
            _startButton.AddListener(StartGame);
        }

        private void OnEnable()
        {
            _startButton.RemoveListener(StartGame);
        }

        private void StartGame() =>
            Instantiate(_bootstrapperPrefab);
    }
}