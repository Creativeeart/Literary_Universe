using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_States : MonoBehaviour {

    public bool isActivateOverlayUI = false;

    public static UI_States instance;
    public bool isDontDestroyOnLoad = true;

    void Awake()
    {
        if (isDontDestroyOnLoad)
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IsActivateOverlayUI2()
    {
        isActivateOverlayUI = !isActivateOverlayUI;
    }

}
