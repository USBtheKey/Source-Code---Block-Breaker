using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation asyncOps;

    [SerializeField] private Image progressBar;
    [SerializeField] private Text textProgressPercent;

    [SerializeField] private int sceneToLoad = -1;
    [SerializeField] private bool allowSceneActivation = true;



    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (sceneToLoad == -1) // load next scene
        {
            asyncOps = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1 );
        }
        else
        {
            asyncOps = SceneManager.LoadSceneAsync(sceneToLoad);
        }
        asyncOps.allowSceneActivation = allowSceneActivation;
    }

    private void Update()
    {
        float percentageCompletion = Mathf.Clamp01(asyncOps.progress / 0.9f);
        progressBar.fillAmount = percentageCompletion;
        textProgressPercent.text = (percentageCompletion * 100).ToString("F2") + "%";

        if (Input.GetKey("return") && percentageCompletion == 1)
        {
            asyncOps.allowSceneActivation = true;
        }
    }

    public void GoToNextScene()
    {
        float percentageCompletion = Mathf.Clamp01(asyncOps.progress / 0.9f);

        if (percentageCompletion == 1)
        {
            asyncOps.allowSceneActivation = true;
        }
    }

}
