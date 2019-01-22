using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class endGamePoints : MonoBehaviour
{
    public Text pointsText;

    void OnEnable()
    {
        StartCoroutine(animateText());
    }

    IEnumerator animateText()
    {
        pointsText.text = "0";
        int points = 0;

        yield return new WaitForSeconds(.00007f);

        while (points < GameManager.instance.gold)
        {
            points++;
            pointsText.text = points.ToString();

            yield return new WaitForSeconds(.000005f);
        }
    }
}
