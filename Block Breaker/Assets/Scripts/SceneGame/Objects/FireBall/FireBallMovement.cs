using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int upDown = 1; // 1 for up -1 for down

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,1 * upDown));
    }
}
