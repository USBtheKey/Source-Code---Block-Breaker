using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSavePlayerScores : MonoBehaviour
{

    public static LoadSavePlayerScores instance = null;

    public static List<PlayerScore> playerScoresList = new List<PlayerScore>();

    private int elementsNum = 10;

    #region Variables
    [SerializeField] private Text highscoreTxtField;

    private GameManager gm;


    #endregion


    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        
        gm = GameManager.instance;
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void LoadData()
    {
        playerScoresList.Clear();

        for (int i = 0; i < elementsNum; i++)
        {
            string name;
            int score;

            name = PlayerPrefs.GetString("name" + i, "AAAA");
            score = PlayerPrefs.GetInt("Score" + i, 0);


            PlayerScore playerScore = new PlayerScore(name, score);

            playerScoresList.Add(playerScore);
            
        }
    }

    public void SaveData()
    {

        SortData();

        for (int i = 0; i < playerScoresList.Count; i++)
        {
            PlayerPrefs.SetInt("Score" + i, playerScoresList[i].Score);
            PlayerPrefs.SetString("name" + i, playerScoresList[i].Name);

            PlayerPrefs.Save();
            
        }
        playerScoresList.Clear();

    }

    public void DisplayData()
    {
        SortData();

        string temp = "";

        for (int i = 0; i < elementsNum; i++)
        {
            temp += i + 1 + ". ";

            if(playerScoresList[i].Name == "")
            {
                temp += "[  ]";
            }
            else
            {
                temp += playerScoresList[i].Name;
            } 
            temp += "\t\t\t\t" + playerScoresList[i].Score.ToString("D8") + "\n";
        }

        highscoreTxtField.text = temp; 
    }

    private void SortData() //Bubble Sort
    {

        for (int i = 0; i < playerScoresList.Count - 1; i++)
        {
            for (int j = 0; j < playerScoresList.Count - 1; j++)
            {
                if (playerScoresList[j + 1].Score > playerScoresList[j].Score)
                {
                    PlayerScore temp = new PlayerScore();

                    temp = playerScoresList[j];
                    playerScoresList[j] = playerScoresList[j + 1];
                    playerScoresList[j + 1] = temp;
                }
            }        
        }
    }
}
