using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public string orientation;

    void Update()
    {
        Debug.Log(Screen.orientation);
        orientation = (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) ? "Portrait" : "Landscape";
    }

    public static ResolutionManager instance;

    void Awake()
    {
        instance = this;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("ResolutionManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
