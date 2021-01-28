using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BrickBehavior : MonoBehaviour
{
    #region Variables
    private GameManager gm;

    [SerializeField] private int brickLife; 
    [SerializeField] private GameObject powerLargePaddle;
    [SerializeField] private GameObject powerMegaBall;
    [SerializeField] private GameObject powerShortPaddle;
    [SerializeField] private GameObject powerSmallBall;
    [SerializeField] private GameObject powerFlipCamera;
    [SerializeField] private GameObject powerMultiBalls;
    [SerializeField] private GameObject powerAmmo; //aka fireball 
    [SerializeField] private GameObject powerSpeedUp;
    [SerializeField] private GameObject collectibleHex;


    [SerializeField] private GameObject deathVFX;
    #endregion

    

    public int BrickLife { get => brickLife; set => brickLife = value; }
    

    private void Start()
    {
        gm = GameManager.instance; 
        gm.Blocks += 1;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        BrickLife -= gm.BallDamage;

        //When brick is destroyed
        if (BrickLife <= 0)
        {
            Destroy(this.gameObject);
        }

        //When brick is not destroyed
        else
        {
            ChangeColor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            BrickLife -= 1;
            Destroy(collision.gameObject);

            if (BrickLife <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                ChangeColor();
            }
        }
    }



    private void SpawnPower()
    {
        int rng = Random.Range(0, 9);
        //rng = 5; // debug line
                 //TODO: Make Sure drop rate are good

        if (Random.Range(0, 4) != 0) // 25% Change of getting a powerup or get a collectible
        {
            switch (rng)
            {
                //Powers Drop
                case 0:
                    Instantiate(powerLargePaddle, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 1:
                    Instantiate(powerMegaBall, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 2:
                    Instantiate(powerShortPaddle, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 3:
                    Instantiate(powerSmallBall, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 4:
                    Instantiate(powerFlipCamera, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 5:
                    Instantiate(powerMultiBalls, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 6:
                    Instantiate(powerAmmo, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 7:
                    Instantiate(powerSpeedUp, gameObject.transform.position, gameObject.transform.rotation);
                    break;

                //Collecible Drop
                case 8:
                    Instantiate(collectibleHex, gameObject.transform.position, gameObject.transform.rotation);
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        if (!GameManager.isQuitting)
        {
            gm.RemoveBlock();
            gm.AddPoints(1000);
            if (!gm.CheckWinLevel())
            {
                SpawnPower();
            }
        }
    }

    private void ChangeColor()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        switch (BrickLife)
        {
            case 1:
                renderer.color = new Color(0, 255, 0);
                break;
            case 2:
                renderer.color = new Color(255, 0, 0);
                break;
        }
    }
}
