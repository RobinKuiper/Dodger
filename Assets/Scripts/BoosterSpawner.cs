using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour
{
    public Transform[] BoosterPrefabs;

    public float minSpawnTime = 15f;
    public float maxSpawnTime = 40f;

    private float timeToSpawn = 10f;

    void Update()
    {
        if (GameManager.instance.gameEnded || GameManager.instance.gamePaused) return;

        if (Time.time >= timeToSpawn)
        {
            Instantiate(BoosterPrefabs[Random.Range(0, BoosterPrefabs.Length)], transform.position, Quaternion.identity);

            timeToSpawn = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
