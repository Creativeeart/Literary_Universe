using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlertUI : MonoBehaviour
{
    public GameObject warningModalWindow;
    public TextMeshProUGUI warningText;
    public bool isAlertUIActive = false;

    public void ShowWarningModalWindow(string warningTextCustom)
    {
        warningModalWindow.SetActive(true);
        warningText.text = warningTextCustom;
        isAlertUIActive = true;
    }

    public void CloseWarningModalWindow()
    {
        warningModalWindow.SetActive(false);
        warningText.text = string.Empty;
        isAlertUIActive = false;
    }
}

