using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallService : MonoBehaviour
{

    #region Variables
    private Rigidbody2D ball; // reference to the compoenent rigid body 2D
    private bool onlyOnce = false; // only used for service so the force wouldnt be applied many times.

    [SerializeField] private float force = 150f; // Raw force applied on the ball

    private Transform myParent;// link to the original parent (Player)
    private Vector3 initPos; // local coordinate fomr the ball relative to its parent

    [SerializeField] private GameObject objBall;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        ball = GetComponent<Rigidbody2D>(); // recover component rigidbody2D from the ball
        ball.simulated = false; // stops the simulation 


        initPos = transform.localPosition;// memorize the location of the ball relative to the parent
        myParent = transform.transform.parent; // memorize the location of the parent
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonUp("Service") || Input.GetMouseButtonDown(0)) && !onlyOnce) // at key release and did not service yet
        {
            onlyOnce = true; // freeze service
            ball.simulated = true; // activate simulation
            ball.transform.parent = null; // detach the ball from its parent PLayer.
            ball.AddForce(new Vector2(force, force)); // gives force right and up
        }
    }

    public void Init()
    {
        gameObject.SetActive(true);
        transform.parent = myParent; // meet the parents
        transform.localPosition = initPos; // go back where the parent is currently
        ball.simulated = false;
        onlyOnce = false; // 
        ball.velocity = new Vector2(0, 0); // Remove all the force
    }
}
