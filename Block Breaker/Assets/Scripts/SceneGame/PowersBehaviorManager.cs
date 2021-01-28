using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersBehaviorManager : MonoBehaviour
{
    public static PowersBehaviorManager GetInstance = null;

    #region Variables
    private GameManager gm;


    private bool isBallNormal = true;
    private bool isPaddleNormal = true;
    private bool isGameSpedUp = false;
   

    [SerializeField] private int numBallsSpawn;
    [SerializeField] private GameObject objMultiBalls;
    #endregion

    private void Awake()
    {
        if(GetInstance == null)
        {
            GetInstance = this;
        }
        else if(GetInstance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;

    }

    //Check if power up or down then unrender the object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (collision.tag)
        {
            case "PowerMegaBall":
                if(isBallNormal) PowerBigBall();
                break;

            case "PowerLargePaddle":
                if(isPaddleNormal) PowerLargePaddle();
                break;

            case "PowerShortPaddle":
                if(isPaddleNormal) PowerShortPaddle();
                break;

            case "PowerSmallBall":
                if(isBallNormal) PowerSmallBall();
                break;

            case "PowerFlipCamera":
                PowerFlipCamera();
                break;

            case "PowerMultiBalls":
                PowerMultiBall();
                break;

            case "PowerFireball":
                PowerFireBall();
                break;

            case "PowerSpeedUp":
                if(!isGameSpedUp) PowerSpeedUp();
                break;  
        } 
        if(!collision.gameObject.CompareTag("CollectibleHexagone")) Destroy(collision.gameObject); // hexagones are only collected with the ball;
    }


    //--------------------- Power Functions -----------------//


    private void PowerBigBall()
    {
        GameObject.FindGameObjectWithTag("Ball").transform.localScale = new Vector3(2f, 2f, 0f);

        GameObject[] gameObjectsMultiBall = GameObject.FindGameObjectsWithTag("MultiBall");

        for (int i = 0; i < gameObjectsMultiBall.Length; i++)
        {
            gameObjectsMultiBall[i].transform.localScale = new Vector3(2f, 2f, 0f);
        }

        gm.BallDamage = 3;
        isBallNormal = false;
        StartCoroutine(IE_Ball_BackToNormal(15));
    }

    private void PowerSmallBall()
    {
        GameObject.FindGameObjectWithTag("Ball").transform.localScale = new Vector3(0.65f, 0.65f, 0f);
        
        GameObject[] gameObjectsMultiBall = GameObject.FindGameObjectsWithTag("MultiBall");

        for (int i = 0; i < gameObjectsMultiBall.Length; i++)
        {
            gameObjectsMultiBall[i].transform.localScale = new Vector3(0.65f, 0.65f, 0f);
        }

        gm.BallDamage = 0;
        isBallNormal = false;
        StartCoroutine(IE_Ball_BackToNormal(25));
    }

    private void PowerShortPaddle()
    {
        Transform paddle = GameObject.FindGameObjectWithTag("Paddle").transform;
        paddle.localScale = new Vector3(0.5f, 1f, 0f);
        isPaddleNormal = false;
        StartCoroutine(IE_Paddle_BackToNormal(20));
    }

    private void PowerLargePaddle()
    {
        Transform paddle = GameObject.FindGameObjectWithTag("Paddle").transform;
        paddle.localScale = new Vector3(2f, 1f, 0f);
        isPaddleNormal = false;
        StartCoroutine(IE_Paddle_BackToNormal(20));
    }

    private void PowerFlipCamera()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.rotation = new Quaternion(0f, 0f, 180f, 0f);
        StartCoroutine(IE_Camera_BackToNormal(10));
    }
    private void PowerFireBall()
    {
        gm.AddAmmo();
    }
    private void PowerMultiBall()
    {
        if (gm.NumsBallOnField == 1)
        {
            Transform currentBallPosition = GameObject.FindGameObjectWithTag("Ball").transform;

            for (int i = 0; i < numBallsSpawn; i++)
            {
                Instantiate(objMultiBalls, currentBallPosition.position, currentBallPosition.rotation); 
            }
        }
    }

    private void PowerSpeedUp()
    {
        Time.timeScale += 0.5f;
        StartCoroutine(IE_Normal_Time_Scale(30)); 
    }

    //------------------------ Power Effect Lasting Timer --------------------//


    private IEnumerator IE_Ball_BackToNormal(float time)
    {
        
        yield return new WaitForSeconds(time);

        GameObject.FindGameObjectWithTag("Ball").transform.localScale = new Vector3(1f, 1f, 0f);

        GameObject[] gameObjectsMultiBall = GameObject.FindGameObjectsWithTag("MultiBall");

        for (int i = 0; i < gameObjectsMultiBall.Length; i++)
        {
            gameObjectsMultiBall[i].transform.localScale = new Vector3(1f, 1f, 0f);
        }
       
        isBallNormal = true;
        gm.BallDamage = 1;
    }

    private IEnumerator IE_Paddle_BackToNormal(float time)
    {
        
        yield return new WaitForSeconds(time);
        Transform paddle = GameObject.FindGameObjectWithTag("Paddle").transform;
        paddle.localScale = new Vector3(1f, 1f, 0f);
        isPaddleNormal = true;
    }

    private IEnumerator IE_Camera_BackToNormal(float time)
    {
        
        yield return new WaitForSeconds(time);
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    private IEnumerator IE_Normal_Time_Scale(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale -= 0.5f;
        isGameSpedUp = false;
    }

}
