using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOpenTipsMechanism : MonoBehaviour
{
    public Button nextZoneBTN;
    public cakeslice.Outline _outLine;

    public Image readyImage;
    bool isClicked = true;

    public static SelectOpenTipsMechanism Instance { get; private set; }
    GateZoneController GateZoneController;
    AlertUI AlertUI;
    TimerGame TimerGame;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GateZoneController = GateZoneController.Instance;
        AlertUI = AlertUI.Instance;
        TimerGame = TimerGame.Instance;
        _outLine = gameObject.GetComponent<cakeslice.Outline>();
    }

    void OnMouseDown()
    {
        if (GateZoneController.greenLamp.activeSelf)
        {
            if (GateZoneController.countAddTips != 0)
            {
                if (isClicked)
                {
                    if (GateZoneController.isOpenTipsMechanismEnabled)
                    {
                        StartCoroutine(FiilImage());
                        TimerGame.RunTime = true;
                        GateZoneController.arrow3DTipsMechanism.SetActive(false);
                    }
                }
            }
            else AlertUI.ShowAlert_DEFAULT("Доступ запрещен. У вас нет подсказок. \nДополнительные подсказки вы можете взять на панели, в нижней части экрана");
        }
        else AlertUI.ShowAlert_DEFAULT("Доступ запрещен. \nСначала вставьте ключи в панель управления.");
    }

    void OnMouseEnter()
    {
        if (GateZoneController.isOpenTipsMechanismEnabled)
        {
            _outLine.enabled = true;
        }
    }

    void OnMouseExit()
    {
        if (GateZoneController.isOpenTipsMechanismEnabled)
        {
            _outLine.enabled = false;
        }
    }

    IEnumerator FiilImage()
    {
        while (readyImage.fillAmount <= 1)
        {
            readyImage.fillAmount += Time.deltaTime * 0.5f;
            _outLine.enabled = false;
            isClicked = false;
            if (readyImage.fillAmount >= 1)
            {
                GateZoneController.OpenTip();
                readyImage.fillAmount = 0;
                _outLine.enabled = true;
                isClicked = true;
                nextZoneBTN.interactable = true;
                break;
            }
            yield return null;
        }
    }
}
