using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleShoot : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private AudioSource fireProjectileSFX;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

        if (gm.HasAmmo() && (Input.GetButtonDown("Service") || Input.GetButtonDown("KdbMouseShootFireball") || Input.GetMouseButtonDown(0)))
        {
            Transform paddle = GameObject.FindGameObjectWithTag("Paddle").transform;
            Instantiate(fireBall, new Vector3(paddle.position.x, paddle.position.y + 0.25f), paddle.rotation);
            fireProjectileSFX.Play();
            gm.RemoveAmmo();
        }
    }

    
}
