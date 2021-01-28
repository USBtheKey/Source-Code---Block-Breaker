using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneSFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource ballDeathSFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("MultiBall"))
        {
            ballDeathSFX.Play();
        }
    }
}
