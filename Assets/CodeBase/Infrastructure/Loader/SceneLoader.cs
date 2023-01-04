using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Loader
{
    public class SceneLoader
    {
        public void Load(string name, Action onLoaded = null) =>
            LoadScene(name, onLoaded);

        private async void LoadScene(string nextScene, Action onLoaded = null)
        {
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
    }
}