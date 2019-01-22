using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    public static int money;

    void Start()
    {
        // Load pref
        money = PlayerPrefs.GetInt("Gold");
    }

    public void add(int value)
    {
        money += value;
        PlayerPrefs.SetInt("Gold", money);
    }

    public int get()
    {
        return money;
    }

    public static Bank instance;

    void Awake()
    {
        instance = this;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Bank");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
