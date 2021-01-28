using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{


    public void ButtonExit()
    {
        
        Application.Quit();
    }

    public void ButtonResume()
    {
        Time.timeScale = 1f; // needs to resume at the paused speed
    }

    public void ButtonLoadScenePreloader()
    {
        SceneManager.LoadSceneAsync("Preloader");
       
        Time.timeScale = 1f; // reset time scale to 1 in the new scene
    }

    public void ButtonLoadSceneLoading()
    {
        SceneManager.LoadSceneAsync("SceneLoad");
       
        Time.timeScale = 1f; // reset time scale to 1 in the new scene
    }




}
