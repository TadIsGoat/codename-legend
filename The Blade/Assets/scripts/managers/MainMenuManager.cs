using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;
    [SerializeField] private SceneLoader sceneLoader;


    public void StartGame()
    {
        sceneLoader.LoadScene(GameData.SceneList.persistentObjects.ToString(), false);
        sceneLoader.LoadScene(GameData.SceneList.BaseScene.ToString());

        StartCoroutine(UpdateLoadingBar());
    }

    private IEnumerator UpdateLoadingBar()
    {
        while (sceneLoader.ReturnProgress() > 0 && sceneLoader.ReturnProgress() < 1)
        {
            loadingBar.fillAmount = sceneLoader.ReturnProgress();
            yield return null; //wait for the next frame
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}