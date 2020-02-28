using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using cakeslice;
public class GateZoneController : MonoBehaviour
{
    public SelectOpenTipsMechanism SelectOpenTipsMechanism;
    public GameObject UI_GateZone;
    public GameObject arrow3DKeysHolder, arrow3DTipsMechanism;
    public int needKeys = 3;
    [Space]
    public GameObject greenLamp;
    public GameObject redLamp;

    [Space]
    public GameObject[] keyHolders;
    public Material emissionMaterialForKeyHolders;
    public Material defaultMaterialForKeyHolder;

    [Space]
    public Animator gateAnimation;
    public bool isOpenGate = false;

    [Space]
    public float timeRotationGears = 5.0f;
    public GameObject[] gearsNormal;
    public GameObject[] gearsReverse;
    public float speedGear = 10f;

    [Space]
    public TextMeshProUGUI tipsText;
    public List<string> allTipsList;
    public GameObject addExtraTipButton;
    public GameObject[] tips3DIcons;

    [Space]
    public GameObject[] keys3DIcons;

    [Space]
    public GameObject[] buttonAddKeys;
    public GameObject[] buttonAddTips;
    [Space]
    public GameObject goToNextZoneBtn;

    bool runTime = false;
    int activeKeyCount = 0;

    public int insertKeysInHolder = 0;
    int countOpenedTips = 0;
    int countOpenedKeys = 0;

    public int countAddTips = 0;
    public int countAddKeys = 0;

    int counter = 0;

    public bool isOpenTipsMechanismEnabled = true;
    public static GateZoneController Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void GateZoneEntered()
    {
        if (FortBoyardGameController.Instance.IsGateZone)
        {
            UI_GateZone.SetActive(true);
            CheckKeysAndTips();
            LampControl(greenLamp, redLamp, false, true);
            for (int i = 0; i < FortBoyardGameController.Instance.TipsImage.Length; i++)
            {
                if (FortBoyardGameController.Instance.TipsImage[i].sprite == FortBoyardGameController.Instance.ActiveTip)
                {
                    countAddTips++;
                }
            }

            for (int i = 0; i < FortBoyardGameController.Instance.CurrentTips; i++)
            {
                tips3DIcons[i].SetActive(true);
            }

            for (int i = 0; i < FortBoyardGameController.Instance.KeysImage.Length; i++)
            {
                if (i >= needKeys) break;
                if (FortBoyardGameController.Instance.KeysImage[i].sprite == FortBoyardGameController.Instance.ActiveKey)
                {
                    countAddKeys++;
                }
            }
            for (int i = 0; i < FortBoyardGameController.Instance.CurrentKeys; i++)
            {
                if (i >= needKeys) break;
                keys3DIcons[i].SetActive(true);
                activeKeyCount++;
            }
            isOpenTipsMechanismEnabled = true;
        }
    }

    void Update()
    {
        if (FortBoyardGameController.Instance.IsGateZone)
        {
            if (runTime)
            {
                timeRotationGears = timeRotationGears - Time.deltaTime;
                if (timeRotationGears <= 0)
                {
                    runTime = false;
                    timeRotationGears = 5.0f;
                }
                if (isOpenGate)
                {
                    for (int i = 0; i < gearsNormal.Length; i++) gearsNormal[i].transform.Rotate(new Vector3(0, 0, Time.deltaTime * speedGear));
                    for (int i = 0; i < gearsReverse.Length; i++) gearsReverse[i].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedGear));
                }
            }
        }
    }

    public void AddKeys(int numberImage)
    {
        FortBoyardGameController.Instance.CurrentKeys += 1;
        TimeReducing(10);
        countAddKeys++;
        for (int i = 0; i < countAddKeys; i++) keys3DIcons[i].SetActive(true);
        activeKeyCount++;
        FortBoyardGameController.Instance.KeysImage[numberImage].gameObject.SetActive(true);
        FortBoyardGameController.Instance.KeysImage[numberImage].sprite = FortBoyardGameController.Instance.ActiveKey;
        FortBoyardGameController.Instance.KeysImage[numberImage].color = "#FFFFFFFF".ToColor();
    }

    public void AddTips(int numberImage)
    {
        if (FortBoyardGameController.Instance.CurrentTips < FortBoyardGameController.Instance.totalTips)
        {
            FortBoyardGameController.Instance.CurrentTips += 1;
            TimeReducing(10);
            countAddTips++;
            for (int i = 0; i < countAddTips; i++) tips3DIcons[i].SetActive(true);
            FortBoyardGameController.Instance.TipsImage[numberImage].gameObject.SetActive(true);
            FortBoyardGameController.Instance.TipsImage[numberImage].sprite = FortBoyardGameController.Instance.ActiveTip;
            FortBoyardGameController.Instance.TipsImage[numberImage].color = "#FFFFFFFF".ToColor();
        }
        if (FortBoyardGameController.Instance.CurrentTips == 5) addExtraTipButton.GetComponent<Button>().interactable = false;
    }

    public void TimeReducing(float countTime)
    {
        FortBoyardGameController.Instance.timeReducing.GetComponent<TextMeshProUGUI>().text = "- " + countTime;
        GameObject Ins = Instantiate(FortBoyardGameController.Instance.timeReducing, FortBoyardGameController.Instance.timeReducingParent);
        Destroy(Ins, 1);
        FortBoyardGameController.Instance.totalTime -= countTime;
        //FortBoyardGameController.Instance.totalTime = FortBoyardGameController.Instance.totalTime - countTime;
        TimerGame.Instance.seconds -= countTime;
        FortBoyardGameController.Instance.ReloadTimer();
    }

    public void CheckKeysAndTips()
    {
        for (int i = 0; i <= 2; i++)
        {
            if (FortBoyardGameController.Instance.KeysImage[i].sprite != FortBoyardGameController.Instance.ActiveKey)
            {
                buttonAddKeys[i].SetActive(true);
            }

            if (FortBoyardGameController.Instance.TipsImage[i].sprite != FortBoyardGameController.Instance.ActiveTip)
            {
                buttonAddTips[i].SetActive(true);
            }
        }

        for (int i = 0; i < buttonAddKeys.Length; i++)
        {
            if (buttonAddKeys[i].activeSelf)
            {
                FortBoyardGameController.Instance.KeysImage[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < buttonAddTips.Length; i++)
        {
            if (buttonAddTips[i].activeSelf)
            {
                FortBoyardGameController.Instance.TipsImage[i].gameObject.SetActive(false);
            }
        }
    }

    public void CloseGate()
    {
        gateAnimation.SetBool("Closed", true);
    }

    public void EnableDisable3DIconTips()
    {
        countAddTips--;
        int result = FortBoyardGameController.Instance.CurrentTips - countOpenedTips;
        for (int i = 0; i < tips3DIcons.Length; i++)
        {
            tips3DIcons[i].SetActive(false);
        }
        for (int i = 0; i < result; i++)
        {
            tips3DIcons[i].SetActive(true);
        }
    }

    public void EnableDisable3DIconKeys()
    {
        countAddKeys--;
        int result = activeKeyCount - countOpenedKeys;
        for (int i = 0; i < keys3DIcons.Length; i++) keys3DIcons[i].SetActive(false);
        for (int i = 0; i < result; i++) keys3DIcons[i].SetActive(true);
    }


    public void GoToAlphabetZone()
    {
        FortBoyardGameController.Instance.mainUconsUI.SetActive(false);
        FortBoyardGameController.Instance.GameRooms = false;
        UI_GateZone.SetActive(false);
        isOpenTipsMechanismEnabled = false;
        StartCoroutine(FB_CamMovingController.Instance.GoToAlphabetZone()); // Переход к зоне с алфавитом
        SelectOpenTipsMechanism._outLine.enabled = false;
    }

    public void OpenTip()
    {
        if (tipsText.text == "Подсказки скрыты") tipsText.text = string.Empty;
        if (FortBoyardGameController.Instance.CurrentTips <= FortBoyardGameController.Instance.totalTips)
        {
            if (counter < FortBoyardGameController.Instance.CurrentTips)
            {
                tipsText.text = tipsText.text + allTipsList[counter] + "\n";
                counter++;
                countOpenedTips++;
                EnableDisable3DIconTips();
                goToNextZoneBtn.SetActive(true);
                if (counter > 2) addExtraTipButton.SetActive(true);
            }
        }
    }

    public void OpenKey()
    {
        if (FortBoyardGameController.Instance.CurrentKeys <= FortBoyardGameController.Instance.totalKeys)
        {
            if (countOpenedKeys < FortBoyardGameController.Instance.CurrentKeys)
            {
                countOpenedKeys++;
                EnableDisable3DIconKeys();
                insertKeysInHolder++;
                arrow3DKeysHolder.transform.localPosition = new Vector3(
                    arrow3DKeysHolder.transform.localPosition.x,
                    arrow3DKeysHolder.transform.localPosition.y - 0.17f,
                    arrow3DKeysHolder.transform.localPosition.z);
            }
        }
    }

    public void OpenGateAndEnableOpenTipsMechanism()
    {
        runTime = true;
        isOpenGate = true;
        gateAnimation.enabled = true;
        LampControl(greenLamp, redLamp, true, false);
        isOpenTipsMechanismEnabled = true;
        arrow3DKeysHolder.SetActive(false);
        arrow3DTipsMechanism.SetActive(true);
    }

    public void LampControl(
        GameObject GreenLamp, 
        GameObject RedLamp, 
        bool IsLampGreenEnable, bool IsLampRedEnable)
    {
        if ((IsLampGreenEnable && !IsLampRedEnable) || (IsLampGreenEnable && IsLampRedEnable))
        {
            GreenLamp.SetActive(true);
            GreenLamp.transform.parent.GetComponent<Animator>().enabled = true;
            RedLamp.SetActive(false);
            RedLamp.transform.parent.GetComponent<Animator>().enabled = false;
        }
        else if ((!IsLampGreenEnable && IsLampRedEnable) || (!IsLampGreenEnable && !IsLampRedEnable))
        {
            GreenLamp.SetActive(false);
            GreenLamp.transform.parent.GetComponent<Animator>().enabled = false;
            RedLamp.SetActive(true);
            RedLamp.transform.parent.GetComponent<Animator>().enabled = true;
        }
    }

    //public void CountingKeys()
    //{
    //    if (FortBoyardGameController.Instance.CurrentKeys < needKeys)
    //    {
    //        int how = needKeys - FortBoyardGameController.Instance.CurrentKeys;
    //        AlertUI.Instance.ShowWarningModalWindow("Доступ запрещен. Вам не хватает ключей: (" + how + ")");
    //    }
    //    else
    //    {
    //        StartCoroutine(MaterialSwitching());
    //    }
    //}

    //IEnumerator MaterialSwitching()
    //{
    //    int i = 0;
    //    while (i < FortBoyardGameController.Instance.CurrentKeys)
    //    {
    //        keyHolders[i].GetComponent<MeshRenderer>().material = emissionMaterialForKeyHolders;
    //        keyHolders[i].transform.GetChild(0).gameObject.SetActive(true);
    //        i++;
    //        if (i >= needKeys)
    //        {
    //            runTime = true;
    //            isOpenGate = true;
    //            gateAnimation.enabled = true;
    //            OnEnableGreenLamp();
    //            break;
    //        }
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}


