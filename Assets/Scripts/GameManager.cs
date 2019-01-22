using GoogleMobileAds.Api;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public float slowness = 10f;
    public Animator animator;
    public GameObject gameOverUI;
    public GameObject continuePanel;
    public GameObject countdownPanel;
    public Text goldText;

    DateTime started;

    [HideInInspector] public bool gameEnded = false;
    [HideInInspector] public bool gamePaused = false;
    [HideInInspector] public bool moveOn = false;

    public int gold = 0;

    float timer = -3;

    int secondsSurvived = 1;
    public int planesDestroyed = 0;
    public int meteorsDestroyed = 0;

    void Start()
    {

    }

    void Update()
    {
        if(!gameEnded && !gamePaused)
        {
            timer += Time.deltaTime;

            if (timer >= secondsSurvived)
            {
                secondsSurvived++;
            }
        }

        goldText.text = "Gold: " + gold;

        if (moveOn)
        {
            continuePanel.SetActive(false);
            countdownPanel.SetActive(true);
            gamePaused = false;
            moveOn = false;
            timer = -3;
        }
    }

    public void ShowContinuePanel()
    {
        if (gameEnded || gamePaused) return;

        gamePaused = true;
        continuePanel.SetActive(true);
    }

    public void EndGame()
    {
        if (gameEnded) return;

        GameObject.Find("Background").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameOver").GetComponent<AudioSource>().Play();

        gameEnded = true;
        Bank.instance.add(gold);

        int points = secondsSurvived + (planesDestroyed * 3) + (meteorsDestroyed * 3) + (gold * 2);

        PlayerPrefs.SetInt("stat_total_seconds_survived", PlayerPrefs.GetInt("stat_total_seconds_survived") + secondsSurvived);
        PlayerPrefs.SetInt("stat_total_planes_destroyed", PlayerPrefs.GetInt("stat_total_planes_destroyed") + planesDestroyed);
        PlayerPrefs.SetInt("stat_total_meteors_destroyed", PlayerPrefs.GetInt("stat_total_meteors_destroyed") + planesDestroyed);
        PlayerPrefs.SetInt("stat_total_gold", PlayerPrefs.GetInt("stat_total_gold") + gold);

        if (PlayerPrefs.GetInt("stat_max_seconds_survived") < secondsSurvived)
        {
            PlayerPrefs.SetInt("stat_max_seconds_survived", secondsSurvived);
        }
        if (PlayerPrefs.GetInt("stat_max_planes_destroyed") < planesDestroyed)
        {
            PlayerPrefs.SetInt("stat_max_planes_destroyed", planesDestroyed);
        }
        if (PlayerPrefs.GetInt("stat_max_meteors_destroyed") < planesDestroyed)
        {
            PlayerPrefs.SetInt("stat_max_meteors_destroyed", planesDestroyed);
        }
        if (PlayerPrefs.GetInt("stat_max_gold_per_game") < gold)
        {
            PlayerPrefs.SetInt("stat_max_gold_per_game", gold);
        }
        if (PlayerPrefs.GetInt("stat_max_points_per_game") < points)
        {
            PlayerPrefs.SetInt("stat_max_points_per_game", points);
        }

        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        animator.SetTrigger("EndGame");

        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;
        //animator.speed = 1f * slowness;

        yield return new WaitForSeconds(1f / slowness);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;
        //animator.speed = 1f / slowness;

        ApplicationManager.instance.deathsBeforeAd--;
        Debug.Log(ApplicationManager.instance.deathsBeforeAd);
        if (ApplicationManager.instance.deathsBeforeAd == 0)
        {
            AdManager.instance.Show();
            ApplicationManager.instance.deathsBeforeAd = UnityEngine.Random.Range(3, 6);
        }

        gameOverUI.SetActive(true);
    }

    // Buttons

    public void Continue()
    {
        if(ApplicationManager.instance.continues > 0)
        {
            PlayerPrefs.SetInt("continues", PlayerPrefs.GetInt("continues") - 1);
            moveOn = true;
        }
    }

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
}
