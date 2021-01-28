using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{

    [SerializeField] private int points;
    private GameManager gm;



    private void Start()
    {
        gm = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") || collision.CompareTag("MultiBall"))
        {
            gm.AddPoints(points);
            Destroy(gameObject);
        }
    }
}
