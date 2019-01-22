using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public Transform Laser;
    public float timeToShoot = 1f;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.instance.gameEnded || GameManager.instance.gamePaused) return;

        if(Time.time >= timeToShoot)
        {
            Instantiate(Laser, transform.position, Quaternion.identity);
            audio.Play();

            timeToShoot = Time.time + 1f;
        }
    }
}
