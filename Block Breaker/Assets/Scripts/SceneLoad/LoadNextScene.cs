using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private AsyncOperation async;


    [SerializeField] private GameObject panel;
    [SerializeField] private int sceneToLoad = -1;

    public AsyncOperation Async { get => async; set => async = value; }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (sceneToLoad == -1) // load next scene
        {
            Async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }
        else
        {
            Async = SceneManager.LoadSceneAsync(sceneToLoad);
        }
        Async.allowSceneActivation = false;
    }

    private void Update()
    {
        if(Async.progress/0.9f == 1)
        {
            panel.SetActive(false);
        }
    }

    public void GoToNextScene()
    {
        float percentageCompletion = Mathf.Clamp01(async.progress / 0.9f);

        if (percentageCompletion == 1)
        {
            async.allowSceneActivation = true;
        }
    }
}
