using UnityEngine;
using UnityEngine.UI;

public class ShipChooser : MonoBehaviour
{
    public int index = 0;

    public GameObject price;
    public Text priceText;

    public GameObject colorChooser;

    public Image colorImg1;
    public Image colorImg2;
    public Image colorImg3;
    public Image colorImg4;

    public Text colorPrice2;
    public Text colorPrice3;
    public Text colorPrice4;

    private Image img;

    private Ships ships;

    private Ships.Ship selected;

    void Start()
    {
        index = (PlayerPrefs.HasKey("ShipIndex") && PlayerPrefs.GetInt("ShipIndex") >= 0) ? PlayerPrefs.GetInt("ShipIndex") : 0;
        ships = ApplicationManager.instance.GetComponent<Ships>();
        ApplicationManager.instance.ship = ships.list[index];
        PlayerPrefs.SetInt(ships.list[0].name, 1);

        img = GetComponent<Image>();

        select(index);
    }

    public void Next()
    {
        index++;
        if (index > ships.list.Count - 1) index = 0;
        select(index);
    }

    public void Prev()
    {
        index--;
        if (index < 0) index = ships.list.Count - 1;
        select(index);
    }

    public void Buy()
    {
        if (selected.unlocked) return;

        if(Bank.money >= selected.price)
        {
            Bank.instance.add(-selected.price);
            selected.unlocked = true;
            PlayerPrefs.SetInt(selected.name, 1);
            select();
        }
    }

    private void select(int index = -1)
    {
        if(index != -1)
        {
            selected = ships.list[index];
            img.sprite = selected.sprite;
            selected.unlocked = PlayerPrefs.HasKey(selected.name) && PlayerPrefs.GetInt(selected.name) == 1;
        }

        price.SetActive(true);
        priceText.text = selected.price.ToString();

        if (selected.unlocked)
        {
            img.color = new Color(255, 255, 255);
            ApplicationManager.instance.ship = selected;
            PlayerPrefs.SetInt("ShipIndex", index);
            price.SetActive(false);
        }
        else img.color = new Color(0, 0, 0);
    }
}
