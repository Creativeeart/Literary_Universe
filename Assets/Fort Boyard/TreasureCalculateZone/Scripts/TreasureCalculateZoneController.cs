using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using cakeslice;
public class TreasureCalculateZoneController : MonoBehaviour
{
    public GameObject goldCap;
    public TextMeshProUGUI CapacityGoldText;
    public TextMeshProUGUI CapacityGoldTextShadow;
    public TextMeshProUGUI finishUI_nameText;
    public TextMeshProUGUI finishUI_resultMoneyText;
    public TextMeshProUGUI finishUI_positionLeaderboardText;
    public TextMeshProUGUI finishUI_donateText;

    [Space]
    public GameObject pojertvovanieUI, selectDonateModalUI, finishModalUI;
    public TextMeshProUGUI pojertvovanieUItext;
    public GameObject sendButton, anotherText;

    [Space]
    //public GameObject treasurZoneCamera, treasureCalculateZoneCamera;
    public float time = 3f;  //Скорость подсчета монет
    public TextMeshProUGUI totalGoldsTextMeshPro;// currentCoinstext;
    public TextMeshProUGUI recordText;
    public float TotalCalculateCoins { get; set; }
    public string DonationName { get; set; }
    //public GameObject uiCanvas;
    //public GameObject watchUI;
    //public GameObject spawn1, spawn2;

    //float currentCoins;

    public static TreasureCalculateZoneController Instance { get; private set; }
    Chest Chest;
    FB_CamMovingController FB_CamMovingController;
    FB_GameMenuController FB_GameMenuController;
    SupportScripts SupportScripts;
    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Chest = Chest.Instance;
        FB_CamMovingController = FB_CamMovingController.Instance;
        FB_GameMenuController = FB_GameMenuController.Instance;
        SupportScripts = SupportScripts.Instance;
    }

    public void TreasureCalculateZoneEntered()
    {
        CalculateGold();
    }

    public void CalculateGold()
    {
        if (Chest.countKeyOpened == 5)
        {
            TotalCalculateCoins = Chest.coinsBoyard * 6f;
        }
        else
        {
            TotalCalculateCoins = 0;
        }
        StartCoroutine("NumberAnimate");
    }
    public IEnumerator CapacityAnimateNumber()
    {
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        Ease EaseAnim = Ease.Unset;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            float result = Mathf.Lerp(0, Chest.coinsBoyard, fraction);
            result = (int)result;
            CapacityGoldText.text = result.ToString();
            CapacityGoldTextShadow.text = result.ToString();

            if (result > 20000)
            {
                goldCap.transform.DOLocalMoveY(65f, 1).Play().SetEase(EaseAnim);
                goldCap.transform.DOScale(new Vector3(80, 80, 80), 1).Play().SetEase(EaseAnim);
            }
            if (result > 40000)
            {
                goldCap.transform.DOLocalMoveY(75f, 1).Play().SetEase(EaseAnim);
                goldCap.transform.DOScale(new Vector3(81, 90, 81), 1).Play().SetEase(EaseAnim);
            }
            if (result > 60000)
            {
                goldCap.transform.DOLocalMoveY(85f, 1).Play().SetEase(EaseAnim);
                goldCap.transform.DOScale(new Vector3(86, 100, 86), 1).Play().SetEase(EaseAnim);
            }
            if (result > 80000)
            {
                goldCap.transform.DOLocalMoveY(95f, 1).Play().SetEase(EaseAnim);
                goldCap.transform.DOScale(new Vector3(90, 110, 90), 1).Play().SetEase(EaseAnim);
            }
            if (result > 100000)
            {
                goldCap.transform.DOLocalMoveY(105f, 1).Play().SetEase(EaseAnim);
                goldCap.transform.DOScale(new Vector3(92, 120, 92), 1).Play().SetEase(EaseAnim);
            }
            yield return null;
        }
        CapacityGoldText.text = Chest.coinsBoyard.ToString();
        CapacityGoldTextShadow.text = Chest.coinsBoyard.ToString();
        yield return new WaitForSeconds(2);
        FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToTreasure_Calculate_Zone_B);
        TreasureCalculateZoneEntered();
    }
    IEnumerator NumberAnimate()
    {
        yield return new WaitForSeconds(3);
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;

        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            float result = Mathf.Lerp(0, TotalCalculateCoins, fraction);
            totalGoldsTextMeshPro.text = result.ToString("C0");
            yield return null;
        }
        StartCoroutine("ShowPojertvovanieUI");
        if (TotalCalculateCoins > FB_GameMenuController.GetMaxValue())
        {
            recordText.text = "Вы установили новый рекорд!";
        }
        else
        {
            recordText.text = string.Format("Предыдущий рекорд: ({0:C0})", FB_GameMenuController.GetMaxValue());
        }
    }

    IEnumerator ShowPojertvovanieUI()
    {
        yield return new WaitForSeconds(2f);
        pojertvovanieUItext.text = "Поздравляем ваш выигрыш составил:<size=70 ><b><color=#FF6400FF>\n" + TotalCalculateCoins.ToString("C0") + "</color></b></font>";
        pojertvovanieUI.SetActive(true);
    }
    public void SelectDonation(TextMeshProUGUI selecetDonationName)
    {
        DonationName = selecetDonationName.text;
    }
    public void SendInfoPojertvovanie()
    {
        if (DonationName != null)
        {
            finishUI_nameText.text = "Уважаемый<b><color=#FF6400FF> " + FB_GameMenuController.currentRealName + "</color></b></font>!";
            finishUI_resultMoneyText.text = "<size=40> Ваш результат<b><color=#FF6400FF>" + TotalCalculateCoins.ToString("C0") + "</color> был добавлен на доску рекордов.</size>";
            finishUI_positionLeaderboardText.text = "Ваша позиция в рейтинге: <size=70><b><color=#FF6400FF>" + " - " + "</color></b></font>";
            finishUI_donateText.text = "Так же вы пожертвовали заработанные средства в:" + "\n" + "<size=50><b><color=#FF6400FF>" + DonationName + "</color></b></font>";
            selectDonateModalUI.SetActive(false);
            finishModalUI.SetActive(true);
            FB_GameMenuController.AddNewHighscore();
        }
    }
    public void GameReload()
    {
        SupportScripts.UI.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SupportScripts.UI.SetActive(true);
    }
}
