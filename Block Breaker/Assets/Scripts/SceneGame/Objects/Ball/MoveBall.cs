using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    #region Variables
    [SerializeField] private float force = 155f; // Raw force applied on the ball
    private Rigidbody2D ball; 

    private GameManager gm; 

    [SerializeField] private float blindSpot = 0.2f;
    [SerializeField] private float vForceMin = 0.6f; // min force on the axis b4 we prevent the horizontal bouncing
    [SerializeField] private float multiplier = 2f; //  we multiply this to thie vForceMin when we detect the ball bouncinf horizontally 
    
    //Audio
    [SerializeField] private AudioSource brickDestructionSFX;
    [SerializeField] private AudioSource collisionSFX;



    [SerializeField] private bool isMultiBall = false;
    private float forceX;
    private float forceY;
    #endregion

    public bool IsMultiBall { get => isMultiBall; set => isMultiBall = value; }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance; 
        ball = gameObject.GetComponent<Rigidbody2D>();

        if (IsMultiBall)
        {

            float direction = 0;

            while (direction < 0.9f && direction > -0.9f) direction = Random.Range(-1f, 1f);

            forceX = direction * force;
            forceY = direction * force;

            ball.AddForce(new Vector2(forceX, vForceMin + forceY * multiplier));

            gm.NumsBallOnField += 1;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "Death")
        {
            collisionSFX.Play();
        }
        else if (collision.gameObject.tag == "Death")
        {
            gm.NumsBallOnField -= 1;
           
            //Debug.Log("nums ball on field" + gm.NumsBallOnField);
            if (!IsMultiBall)
            {
                gameObject.SetActive(false);
            }
            else if (isMultiBall)
            {
                
                Destroy(gameObject);
            }

            if (gm.NumsBallOnField <= 0 )  
            { 
               gm.Death();
            }
        }

        if (collision.gameObject.tag == "Paddle") 
        {
            float diffX = transform.position.x - collision.transform.position.x;
            
            Time.timeScale *= 1.005f;

            if (diffX < -blindSpot) //check left side blind spot paddle
            {
                ball.velocity = new Vector2(0, 0);
                ball.AddForce(new Vector2(-force, force));
            }
            else if (diffX > blindSpot)
            {
                ball.velocity = new Vector2(0, 0);
                ball.AddForce(new Vector2(force, force));
            }
        }

        else if (collision.gameObject.CompareTag("Brick"))
        {
            int life = collision.gameObject.GetComponent<BrickBehavior>().BrickLife;

            if (life == 1)
            {
                brickDestructionSFX.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) // prevent the ball to move horizontally
    {
        if (Mathf.Abs(ball.velocity.y) < vForceMin) // checks if we are below vForceMin
        {
            float vecX = ball.velocity.x; // save velocity.x
            if (ball.velocity.y < 0)
            {
                ball.velocity = new Vector2(vecX, -vForceMin * multiplier);
            }
            else // going up
            {
                ball.velocity = new Vector2(vecX, vForceMin * multiplier);
            }
        }
    }
}
