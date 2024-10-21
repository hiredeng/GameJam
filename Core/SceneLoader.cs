using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectName.Core
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineContext;
        private readonly ViewBlocker _viewBlocker;

        public SceneLoader(ICoroutineRunner coroutineContext) =>
            _coroutineContext = coroutineContext;

        public void LoadScene(string scene, Action loadedCallback) =>
            _coroutineContext.StartCoroutine(LoadSceneProcess(scene, loadedCallback, false));
        public void AddScene(string scene, Action loadedCallback) =>
            _coroutineContext.StartCoroutine(LoadSceneProcess(scene, loadedCallback, true));


        private IEnumerator LoadSceneProcess(string targetScene, Action loadedCallback, bool additive)
        {
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(targetScene, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);

            while (!loadSceneOperation.isDone) 
                yield return null;

            loadedCallback?.Invoke();
        }
    }
}