using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public List<AsyncOperation> scenesToLoad {get; private set;} = new List<AsyncOperation>();

    public void LoadScene(string sceneName, bool loadAsync = true) {
        if (loadAsync) {
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive)); //loads scene in the background
        } else if (loadAsync == false) {
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName)); //loads scene in the main thread
        }
    }

    public float ReturnProgress()
    {
        float progress = 0;

        foreach (var scene in scenesToLoad)
        {
            if (!scene.isDone) {
                progress += scene.progress;
            }
        }
        
        progress /= scenesToLoad.Count;

        return progress;
    }
}
