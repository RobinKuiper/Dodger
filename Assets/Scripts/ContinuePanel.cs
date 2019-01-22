using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePanel : MonoBehaviour
{
    public Button freeContinueButton;
    public Text continuesAmountText;
    public Image timerBar;
    public float timer = 5f;
    public Text timerText;

    private float t;
    private float transformSteps;

    void OnEnable()
    {
        t = timer;
        //transformSteps = timerImage.GetComponent<RectTransform>().rect.width / timer;
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        timerText.text = Mathf.Round(t).ToString();
        timerBar.fillAmount = 1 / (timer / t);

        freeContinueButton.interactable = AdManager.instance.usedFreeContinue ? false : true;
        continuesAmountText.text = ApplicationManager.instance.continues.ToString() + "x";

        t -= Time.deltaTime;

        if(t <= 0)
        {
            GameManager.instance.EndGame();
            gameObject.SetActive(false);
        }
    }

    public void ShowVideoAd()
    {
        if (!AdManager.instance.Show("continue"))
        {
            t += 5;
        }
    }
}
