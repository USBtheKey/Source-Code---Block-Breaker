using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAddScore : MonoBehaviour
{
    [SerializeField] private Text name;
    public void AddScore()
    {
        GameManager gm = GameManager.instance;
       
        string initials = name.text.ToUpper();
        LoadSavePlayerScores.playerScoresList.Add(new PlayerScore(initials, gm.Score));
        LoadSavePlayerScores loadSavePlayer = LoadSavePlayerScores.instance;
        loadSavePlayer.SaveData();
    }
}
