using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyShell"))
        {
            gm.RemoveLife();
        }
    }
}
