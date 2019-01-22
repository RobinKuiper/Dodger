using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text continuesText;

    public GameObject BonusPanel;
    public Text BonusAmountText;
    public Text BonusAmountText2;
    public GameObject BonusTimeTextBlock;
    public Text BonusTimeText;
    private int bonusAmount;

    DateTime test = DateTime.Now.AddSeconds(10);

    void Start()
    {
        BonusTimeTextBlock.SetActive(false);

        if (PlayerPrefs.HasKey("BonusAmount"))
        {
            bonusAmount = PlayerPrefs.GetInt("BonusAmount");
        }
        else
        {
            bonusAmount = 3;
            PlayerPrefs.SetInt("BonusAmount", 3);
        }
         
    }

    void Update()
    {
        bonusAmount = PlayerPrefs.GetInt("BonusAmount");

        continuesText.text = ApplicationManager.instance.continues.ToString() + "x";

        BonusAmountText.text = bonusAmount.ToString();
        BonusAmountText2.text = bonusAmount.ToString();

        if (PlayerPrefs.HasKey("NewBonus") && PlayerPrefs.GetString("NewBonus") != "none")
        {
            BonusTimeTextBlock.SetActive(true);
            DateTime datetime;
            DateTime.TryParse(PlayerPrefs.GetString("NewBonus"), out datetime);
            TimeSpan timeOver = DateTime.Now.Subtract(datetime);
            BonusTimeText.text = timeOver.ToString(@"hh\:mm\:ss");

            if(TimeSpan.Compare(timeOver, new TimeSpan()) == 1) {
                bonusAmount++;
                PlayerPrefs.SetInt("BonusAmount", bonusAmount);

                if(bonusAmount < 3)
                {
                    PlayerPrefs.SetString("NewBonus", DateTime.Now.AddHours(3).ToString());
                }
                else
                {
                    PlayerPrefs.SetString("NewBonus", "none");
                    BonusTimeTextBlock.SetActive(false);
                }
            }
        }

        //Debug.Log(TimeSpan.Compare(DateTime.Now.Subtract(test), new TimeSpan()));
        //Debug.Log(new TimeSpan());
    }

    public void ShowBonusPanel()
    {
        BonusPanel.SetActive(true);
    }

    public void CloseBonusPanel()
    {
        BonusPanel.SetActive(false);
    }

    public void ShowRewardVideo()
    {
        if(bonusAmount > 0)
        {
            if (AdManager.instance.Show("reward"))
            {
                bonusAmount--;
                PlayerPrefs.SetInt("BonusAmount", bonusAmount);

                CloseBonusPanel();

                if (bonusAmount == 2)
                {
                    PlayerPrefs.SetString("NewBonus", DateTime.Now.AddHours(3).ToString());
                    //PlayerPrefs.SetString("NewBonus", DateTime.Now.AddSeconds(10).ToString());
                }
            }
            else
            {
                // TODO Video not loaded shit
            }
        }
    }
}
