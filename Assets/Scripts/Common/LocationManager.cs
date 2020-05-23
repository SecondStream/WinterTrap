namespace WinterTrap.Common
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using View;

    public class LocationManager : BaseBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private string _startLocation;
#pragma warning restore 0649

        public void LoadLocation(string locationName, Action onComplete = null)
        {
            StartCoroutine(Load(locationName, onComplete));
        }

        protected override void DoAwake()
        {
            DontDestroyOnLoad(this);

            if (!string.IsNullOrEmpty(_startLocation))
                LoadLocation(_startLocation);
        }

        private IEnumerator Load(string locationName, Action onComplete)
        {
            var currentScene = SceneManager.GetActiveScene();
            var operation = SceneManager.LoadSceneAsync(locationName, LoadSceneMode.Additive);

            operation.allowSceneActivation = true;
            while (!operation.isDone)
                yield return null;

            operation = SceneManager.UnloadSceneAsync(currentScene);
            while (!operation.isDone)
                yield return null;

            onComplete?.Invoke();
        }
    }
}