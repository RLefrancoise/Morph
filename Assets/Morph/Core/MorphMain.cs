using System.Collections.Generic;
using Morph.Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Morph.Core
{
    /// <inheritdoc cref="IMorphMainEntryPoint"/>
    /// <summary>
    /// Morph entry point.
    /// </summary>
    public sealed class MorphMain : UnitySingleton<MorphMain>, IMorphMainEntryPoint
    {
        public IMorphApplication Application { get; private set; }

        [SerializeField]
        private SceneReference _applicationScene;

        [SerializeField]
        private List<SceneReference> _scenesToLoad;

        private void Start()
        {
            //Load application scene if not already loaded
            if (!SceneManager.GetSceneByPath(_applicationScene.ScenePath).isLoaded)
            {
                AsyncOperation asyncOp = SceneManager.LoadSceneAsync(_applicationScene.ScenePath, LoadSceneMode.Additive);
                asyncOp.completed += WhenApplicationSceneLoaded;
            }
        }

        private void WhenApplicationSceneLoaded(AsyncOperation op)
        {
            //Find application in scene
            GameObject[] sceneRoots = SceneManager.GetSceneByPath(_applicationScene.ScenePath).GetRootGameObjects();

            foreach (var root in sceneRoots)
            {
                Application = root.GetComponent<IMorphApplication>();
                if (Application != null) break;
            }

            if (Application == null)
            {
                Debug.LogError("No Morph application found!");
                return;
            }

            //Initialize the application
            if (!Application.Initialize())
            {
                Debug.LogError("Failed to initialize application");
                return;
            }

            //Load all other scenes
            foreach (var scene in _scenesToLoad)
            {
                SceneManager.LoadScene(scene.ScenePath, LoadSceneMode.Additive);
            }
        }
    }
}
