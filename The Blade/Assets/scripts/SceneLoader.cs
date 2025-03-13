using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //////////////////////////////////////////////////////////
    // The entry scrossfadeOut is set in the unity animator //
    //////////////////////////////////////////////////////////

    [SerializeField] private Animator animator;
    public List<AsyncOperation> scenesToLoad {get; private set;} = new List<AsyncOperation>();

    public void LoadScene(string sceneName, bool loadAsync = true) {
        if (loadAsync) {
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        } else {
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName));
        }
    }

    public async Task Transition()
    {
        animator.Play("crossfadeIn");
        await Task.Delay((int)animator.GetCurrentAnimatorStateInfo(0).length * 1000);
    }

    /* //might be useless later
    public float ReturnProgress()
    {
        if (scenesToLoad.Count == 0) 
            return 1;

        float progress = 0;

        foreach (var scene in scenesToLoad)
        {
            if (!scene.isDone) {
                progress += scene.progress;
            }
        }
        
        progress /= scenesToLoad.Count;

        return progress;
    }*/
}
