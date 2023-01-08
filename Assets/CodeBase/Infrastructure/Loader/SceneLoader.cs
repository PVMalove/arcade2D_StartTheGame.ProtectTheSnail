using System;
using CodeBase.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Loader
{
    public class SceneLoader
    {
        public SceneNames PreviousSceneName { get; private set; }
        
        public void Load(SceneNames sceneName, Action onLoaded = null)
        {
            GetPreviousSceneName();
            LoadScene(sceneName, onLoaded).Forget();
        }

        private async UniTaskVoid LoadScene(SceneNames sceneName, Action onLoaded = null)
        {
            string nextScene = sceneName.ToString();
            
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            while (!waitNextScene.isDone)
                await UniTask.Yield();

            onLoaded?.Invoke();
        }

        private void GetPreviousSceneName()
        {
            string loadedSceneName = SceneManager.GetActiveScene().name;
            if (Enum.TryParse(loadedSceneName, out SceneNames previousSceneName))
                PreviousSceneName = previousSceneName;
        }
    }
}