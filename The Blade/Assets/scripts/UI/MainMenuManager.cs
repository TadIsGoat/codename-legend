using NUnit.Framework;
using System;
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
    [SerializeField] private GameManager gameManager;

    public async void StartGame()
    {
        await sceneLoader.Transition();

        sceneLoader.LoadScene(GameData.SceneList.persistentObjects.ToString(), false);
        sceneLoader.LoadScene(GameData.SceneList.BaseScene.ToString());

        //StartCoroutine(UpdateLoadingBar());
    }

    public void QuitGame()
    {
        gameManager.Quit();
    }

    /*  //nobody can currently see the bar, cuz of the transition, we need to make the bar react somehow before the transition and dum this
    private IEnumerator UpdateLoadingBar()
    {
        while (sceneLoader.ReturnProgress() > 0 && sceneLoader.ReturnProgress() < 1)
        {
            loadingBar.fillAmount = sceneLoader.ReturnProgress();
            yield return null; //wait for the next frame
        }
    }*/
}