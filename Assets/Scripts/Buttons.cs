using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void AdPlay()
    {
        /*if (AdManager.instance.interstitial.IsLoaded())
        {
            AdManager.Instance.interstitial.Show();
        }*/

        SceneManager.LoadScene("Main");
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
