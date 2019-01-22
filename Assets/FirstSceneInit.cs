using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneInit : MonoBehaviour
{
    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("ApplicationManager");

        if (!obj)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
