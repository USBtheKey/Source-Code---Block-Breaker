using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] private GameObject EnemyProjectile;
    [SerializeField] private AudioSource FireSFX;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 2, 2);
    }

    void Fire()
    {
        Transform gun = gameObject.transform;
        Instantiate(EnemyProjectile, gun.position, EnemyProjectile.transform.rotation);
        FireSFX.Play();
    }

}
