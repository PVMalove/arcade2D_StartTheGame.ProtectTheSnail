using CodeBase.UI.Elements;
using Plugins.Yandex.CodeBase;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private StartButtonView _startButton;
        [SerializeField] private GameObject _bootstrapperPrefab;
        [SerializeField] private LoadingYandexSDK _yandex;

        private void Awake() => 
            _yandex.Load();

        private void OnEnable() => 
            _startButton.StartGame += StartGame;

        private void OnDestroy() => 
            _startButton.StartGame -= StartGame;

        private void StartGame() => 
            Instantiate(_bootstrapperPrefab);
    }
}