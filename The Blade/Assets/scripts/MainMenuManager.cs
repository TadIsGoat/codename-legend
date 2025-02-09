using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;

    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(Helper.SceneList.persistentObjects.ToString())); //loads scene in the main thread
        scenesToLoad.Add(SceneManager.LoadSceneAsync(Helper.SceneList.BaseScene.ToString(), LoadSceneMode.Additive)); //loads scene in the background

        StartCoroutine(UpdateLoadingBar());
    }

    private IEnumerator UpdateLoadingBar()
    {
        float progress = 0f;

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                progress += scenesToLoad[i].progress;
                loadingBar.fillAmount = progress / scenesToLoad.Count;
                yield return null;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}