using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public Transform[] prefabs;
    public Transform diamondPrefab;
    public Transform[] spawnpoints;

    private float timeToSpawn = 3f;
    //public Text wavesText;
    public float startTimeBetweenWaves = 1f;

    public GameObject countdownOverlay;
    public Text countdownText;
    private int countdownTimer = 3;

    [HideInInspector]
    public static int wavesSpawned = 0;

    private bool paused = false;

    void Start()
    {
        timeToSpawn = Time.time + 3f;
        StartCoroutine(Countdown());
        wavesSpawned = 0;
    }

    void Update () {
        if (GameManager.instance.gamePaused)
        {
            paused = true;
            return;
        }

        if (paused)
        {
            timeToSpawn = Time.time + 3f;
            countdownTimer = 3;
            StartCoroutine(Countdown());
            paused = false;
        }

        if (GameManager.instance.gameEnded) return;

        if (Time.time >= timeToSpawn)
        {
            int type = Random.Range(0, 2);
            float extraTime = 0;

            switch (type)
            {
                case 0:
                    SpawnFour();
                    
                    break;

                case 1:
                    StartCoroutine(SpawnFive());
                    extraTime = 2f;
                    break;
            }

            wavesSpawned++;
            //wavesText.text = "Wave: " + wavesSpawned.ToString();

            timeToSpawn = Time.time + (startTimeBetweenWaves + extraTime);
        }
	}

    IEnumerator Countdown()
    {
        while(countdownTimer >= 0)
        {
            countdownText.text = countdownTimer.ToString();
            countdownTimer--;
            if (countdownTimer < 0)
            {
                countdownOverlay.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnFour()
    {
        int randomIndex = Random.Range(0, spawnpoints.Length);

        for (int i = 0; i < spawnpoints.Length; i++)
        {
            if (randomIndex != i)
            {
                SpawnBlock(spawnpoints[i].position, spawnpoints[i], prefabs[0]);
            }else if (randomIndex == i)
            {
                if (Random.Range(0, 2) == 1) SpawnGem(spawnpoints[i].position, spawnpoints[i]);
            }
        }
    }

    IEnumerator SpawnFive()
    {
        bool spawnGem = Random.Range(0, 5) == 1;
        int spawnIndex = Random.Range(0, (spawnpoints.Length - 1));
        Transform prefab = prefabs[Random.Range(0, prefabs.Length)];

        for (int i = 0; i < spawnpoints.Length; i++)
        {
            if (spawnGem && i == spawnIndex)
            {
                SpawnGem(spawnpoints[i].position, spawnpoints[i]);
            }
            else
            {
                SpawnBlock(spawnpoints[i].position, spawnpoints[i], prefab);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    void SpawnBlock(Vector3 position, Transform parent, Transform prefab)
    {
        Transform _block = Instantiate(prefab, position, Quaternion.identity);
        _block.parent = parent;
        //Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        //_block.GetComponent<Renderer>().material.color = newColor;
    }

    void SpawnGem(Vector3 position, Transform parent)
    {
        Transform _diamond = Instantiate(diamondPrefab, position, Quaternion.identity);
        _diamond.parent = parent;
    }
}
