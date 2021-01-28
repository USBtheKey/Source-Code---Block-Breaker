using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{

    [SerializeField] private Text controlText;


    public void ChangeControlText()
    {
        int control = 0;
        if (controlText.text == "Keyboard")
        {
            controlText.text = "Mouse";
            control = 1;
        }
        else
        {
            controlText.text = "Keyboard";
            control = 0;
        }
        PlayerPrefs.SetInt("Control", control);
        PlayerPrefs.Save();
    }
}
