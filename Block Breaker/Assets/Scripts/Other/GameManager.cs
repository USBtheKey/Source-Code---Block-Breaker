using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance = null; // Declaration of singleton

    //Gameplay
    private PaddleShoot ps;
    private PowersBehaviorManager pbm;

    private int life = 5;
    private int ammo = 0;
    private int ballDamage = 1;
    private int numsBallOnField = 1;
  
    private BallService ball; 

    
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text lifeTxt;
    [SerializeField] private Text ammoTxt;

    [SerializeField] private string preTextScore = "SCORE: ";
    private int score = 0;

    private int blocks = 0; 
    [SerializeField] private GameObject[] levels; 
    private int currentLevel = 0; 
    private GameObject currentBoard; 
    private int bestScore = 0;
    private PanelManager panelsManager;

    private float levelSpeed;

    //Audio
    [SerializeField] private AudioSource audioS;

    //Canvas
    private bool isGamePaused = false;
    public static bool isQuitting = false;
    #endregion

    #region Get & Set
    public int NumsBallOnField { get => numsBallOnField; set => numsBallOnField = value; }
    public int BallDamage { get => ballDamage; set => ballDamage = value; }
    public int Score { get => score; set => score = value; }
    public int Blocks { get => blocks; set => blocks = value; }
    #endregion

    private void LoadLevel()
    {
        Blocks = 0; //Reset the number of blocks

        if (currentBoard) // if we have a board, delete the board
        {
            Destroy(currentBoard);
        }

        currentBoard = Instantiate(levels[currentLevel]); // clone the level


        levelSpeed = Time.timeScale *= 1.05f;
    }


    private void Awake()
    {

        if (instance == null) 
        {
            instance = this; 
        }
        else if (instance != this) 
        {
            Destroy(gameObject); 
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        isQuitting = false;
        Time.timeScale = 0.97f; // starting speed; to be added with the 0.03f in Loadlevel()

        pbm = PowersBehaviorManager.GetInstance;
        

        GameObject.FindGameObjectWithTag("Ball").SetActive(true);
        ball = GameObject.Find("Ball").GetComponent<BallService>();
        panelsManager = GameObject.Find("Canvas").GetComponent<PanelManager>();

        UpdateAmmoText();
        UpdateScoreText();
        UpdateLifeText();
        LoadLevel();

        bestScore = PlayerPrefs.GetInt("Score" + 0, 0);

    }

    void Init() // initialize the game
    {
        
        ResetGameObjects();
        //Balls reset

        ball.Init(); // initialize the ball
        numsBallOnField = 1;


        // Reset Ammo
        ammo = 0;
        UpdateAmmoText();
    }



    void Update()
    {

        if(!(life <= 0)) // if game is not over
        {
            if (Input.GetButtonDown("Cancel") && !isGamePaused) // press ESC 
            {
                Time.timeScale = 0f;
                panelsManager.PanelToggler(0); // Shows ingame Menu
                isGamePaused = true;
                audioS.Pause();
            }
            else if (Input.GetButtonDown("Cancel") && isGamePaused) // press ESC 
            {
                Time.timeScale = 0f;
                panelsManager.Panels[0].SetActive(false); // Shows ingame Menu
                isGamePaused = false;
                Time.timeScale = levelSpeed;
                if(!audioS.isPlaying) audioS.Play();

            }
        }
    }

    private void OnDisable()
    {
        if (this.enabled)
        {
            Destroy(this);
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnApplicationFocus(bool focus)
    {

    }

    public void Death()
    {
        life--; //remove a life
        UpdateLifeText();


        if (life > 0)
        {
            Init();
        }
        else //Game Over
        {
            Time.timeScale = 0f;
            panelsManager.PanelToggler(1); // Open Up the Score Entry Menu
        }
    }

    public bool CheckWinLevel()
    {
     
        if(Blocks <= 0 && !isQuitting) // Finished level
        {
            if(currentLevel < levels.Length - 1) // move to the next level, if we are not at the last level;
            {
                currentLevel++; 
            }

            //replay level if last level
            LoadLevel(); 
            Init();
            return true;
        }
        return false;
    }

    private void ResetGameObjects()
    {
        DestroyObjects("MultiBall");
        DestroyObjects("PowerMegaBall");
        DestroyObjects("PowerSmallBall");
        DestroyObjects("PowerMultiBalls");
        DestroyObjects("PowerFireball");
        DestroyObjects("PowerFlipCamera");
        DestroyObjects("PowerLargePaddle");
        DestroyObjects("PowerShortPaddle");
        DestroyObjects("Fireball");
    }

    private void DestroyObjects(string name)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(name);

        for (int i = 0; i < gameObjects.Length; i ++)
        {
            Destroy(gameObjects[i]);
        }

    }

    public bool HasAmmo()
    {
        return !(ammo <= 0);
    }

    private void UpdateScoreText()
    {
        if (!scoreTxt.Equals(default))
        {
            scoreTxt.text = preTextScore + "\n" + Score.ToString("D8"); // defaut 8 digits of zeros
        }
    }

    private void UpdateScoreText(string str)
    {
        if (!scoreTxt.Equals(default))
        {
            scoreTxt.text = str + preTextScore + "\n" + Score.ToString("D8"); // defaut 8 digits of zeros
        }
    }

    public void QuitGame()
    {
        isQuitting = true;
        instance = null;
    }

    private void UpdateLifeText()
    {
        if (!lifeTxt.Equals(default))
        {
            lifeTxt.text = "Life: " + life;
        }
    }
    private void UpdateAmmoText()
    {
        if (!ammoTxt.Equals(null))
        {
            ammoTxt.text = "Ammo: " + ammo;
        }
    }
    public void AddBlock()
    {
        blocks++;
    }
    public void RemoveBlock()
    {
        blocks--;
    }
    public void AddAmmo()
    {
        ammo += (ammo >= 5) ? 0 : 1;
        UpdateAmmoText();
    }
    public void RemoveAmmo()
    {
        ammo -= (ammo <= 0) ? 0 : 1;
        UpdateAmmoText();
    }

    public void AddPoints(int points)
    {
        string prepreText = "";

        if (Score > bestScore)
        {
            prepreText = "BEST ";
        }
        
        Score += points;

        UpdateScoreText(prepreText);
    }

    public void RemoveLife()
    {
        life--;
        UpdateLifeText();

        if (life <= 0)
        {
            Time.timeScale = 0f;
            panelsManager.PanelToggler(1); // Open Up the Score Entry Menu
        }
    }
}
