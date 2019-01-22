using UnityEngine;
using UnityEngine.UI;

public class BoosterSelector : MonoBehaviour
{
    public Transform parentCanvas;

    public Text LaserBoosterText;
    public Text ShieldBoosterText;

    public GameObject BuyCanvas;

    private int laserBoosters = 0;
    private int shieldBoosters = 0;

    public Transform laserBoosterBtn;
    public Transform shieldBoosterBtn;
    public Transform laserBoosterAnimItem;
    public Transform shieldBoosterAnimItem;

    void Update()
    {
        laserBoosters = PlayerPrefs.HasKey("boosters_laser") ? PlayerPrefs.GetInt("boosters_laser") : laserBoosters;
        shieldBoosters = PlayerPrefs.HasKey("boosters_shield") ? PlayerPrefs.GetInt("boosters_shield") : shieldBoosters;

        LaserBoosterText.text = laserBoosters.ToString();
        ShieldBoosterText.text = shieldBoosters.ToString();
    }

    public void Use(string type)
    {
        bool used = false;

        switch (type)
        {
            case "Laser":
                if (ApplicationManager.instance.boosters.Exists(item => item == 0)) return;

                if (laserBoosters > 0)
                {
                    PlayerPrefs.SetInt("boosters_laser", PlayerPrefs.GetInt("boosters_laser") - 1);
                    ApplicationManager.instance.boosters.Add(0);
                    used = true;
                    Transform t = Instantiate(laserBoosterAnimItem, laserBoosterBtn.position, Quaternion.identity);
                    t.SetParent(parentCanvas, false);
                }
                break;

            case "Shield":
                if (ApplicationManager.instance.boosters.Exists(item => item == 1)) return;

                if (shieldBoosters > 0)
                {
                    PlayerPrefs.SetInt("boosters_shield", PlayerPrefs.GetInt("boosters_shield") - 1);
                    ApplicationManager.instance.boosters.Add(1);
                    used = true;
                    Transform t = Instantiate(shieldBoosterAnimItem, shieldBoosterBtn.position, Quaternion.identity);
                    t.SetParent(parentCanvas, false);
                }
                break;
        }

        if (!used)
        {
            BuyCanvas.SetActive(true);
        }
    }

    public void Close()
    {
        BuyCanvas.SetActive(false);
    }
}
