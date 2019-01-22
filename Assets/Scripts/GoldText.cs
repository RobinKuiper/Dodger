using UnityEngine.UI;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = Bank.instance.get().ToString();
    }
}
