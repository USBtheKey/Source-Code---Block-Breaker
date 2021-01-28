using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddleMovement : MonoBehaviour
{

    private GameManager gm;
    private int choiceControl; //0 = keyboard; 1 = mouse
    private GameObject board;


    private float boardWidth;
    private float constant = 0.5f; // board is 5 width; / 2 = 2.5 - .5 = 2 == the play area
    private float restrictedArea; // area where the player is allowed to move

    //Keyboard Control
    [SerializeField] private float keyboardSpeed;// = 5f;

    private void Awake()
    {
        board = GameObject.Find("Board");
        boardWidth = board.GetComponent<Renderer>().bounds.size.x;
        
    }


    private void Start()
    {
        restrictedArea = (boardWidth / 2) - constant;

        choiceControl = PlayerPrefs.GetInt("Control", 0);
    }


    // Update is called once per frame
    void Update()
    {

        switch (choiceControl)
        {
            case 0:
                //Keyboard Control
                KeyboardControl();
                break;

            case 1:
                //Mouse control;
                MouseControl();
                break;
        }

    }

    private void KeyboardControl()
    {

        float transHorizontal = Input.GetAxis("KdbMouseHorizontal") * keyboardSpeed * (Time.deltaTime/Time.timeScale); //(Time.deltaTime/Time.timeScale)

        //Check if paddle is out of bound && if paddle is moving; 
        //if doesn't check paddle moving, it will get stuck aka it will loop the if statement to inifity in Update()
        if ((transform.position.x >= restrictedArea && transHorizontal > 0) 
            || (transform.position.x <= -restrictedArea && transHorizontal < 0))
        {
            transHorizontal = 0;
        }

        transform.Translate(transHorizontal, 0, 0);
    }


    private void MouseControl()
    {
       
        float currentMouseX  = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

       
        if (currentMouseX >= restrictedArea)
        {
            currentMouseX = restrictedArea;
        }
        else if ((currentMouseX <= -restrictedArea))
        {
            currentMouseX = -restrictedArea;
        }

        transform.position = new Vector3(currentMouseX, transform.position.y) ; 
    }
}
