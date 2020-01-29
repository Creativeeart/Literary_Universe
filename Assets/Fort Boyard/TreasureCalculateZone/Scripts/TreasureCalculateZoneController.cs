using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
namespace cakeslice
{
    public class TreasureCalculateZoneController : MonoBehaviour
    {
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

        public void Awake()
        {
            Instance = this;
        }

        public void TreasureCalculateZoneEntered()
        {
            Instance.CalculateGold();
        }

        void Update()
        {
            //if (FortBoyardGameController.Instance.IsTreasureCalculateZone)
            //{
            //    //spawn1.SetActive(false);
            //    //spawn2.SetActive(false);
            //    //treasurZoneCamera.SetActive(false);
            //    //treasureCalculateZoneCamera.SetActive(true);
            //    //_highscores_FortBoyard._fortBoyardGameController.FB_CamMovingController.CameraMovingToPoint(_highscores_FortBoyard._fortBoyardGameController.FB_CamMovingController.pointToTreasure_Calculate_Zone_A);
            //    //StartCoroutine(Wait());
            //    //uiCanvas.SetActive(true);
            //    //currentCoins = _hitColliderCoin.currentCoins;
            //    //if (Chest.Instance.countKeyOpened == 5)
            //    //{
            //    //    currentCoins = Chest.Instance.coinsBoyard;
            //    //}
            //    //else
            //    //{
            //    //    currentCoins = 0;
            //    //}
            //    //currentCoinstext.text = currentCoins.ToString();
            //    //_hitColliderCoin.enabled = false;
            //    //watchUI.SetActive(false);
            //    FortBoyardGameController.Instance.IsTreasureCalculateZone = false;
            //}
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToTreasure_Calculate_Zone_B);
        }

        public void CalculateGold()
        {
            //totalCalculateCoins = _hitColliderCoin.currentCoins * 63f;
            if (Chest.Instance.countKeyOpened == 5)
            {
                TotalCalculateCoins = Chest.Instance.coinsBoyard * 63f;
            }
            else
            {
                TotalCalculateCoins = 0;
            }
            //treasureCalculateZoneCamera.GetComponent<Animator>().enabled = true;
            StartCoroutine("NumberAnimate");
        }
        IEnumerator NumberAnimate()
        {
            yield return new WaitForSeconds(2f);
            float startTime = Time.realtimeSinceStartup;
            float fraction = 0f;

            while (fraction < 1f)
            {
                fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
                float result = Mathf.Lerp(0, TotalCalculateCoins, fraction);
                totalGoldsTextMeshPro.text = "$ " + result.ToString();
                yield return null;
            }
            StartCoroutine("ShowPojertvovanieUI");
            if (TotalCalculateCoins > Highscores_FortBoyard.Instance.maxValue)
            {
                recordText.text = "Вы установили новый рекорд!";
            }
            else
            {
                recordText.text = string.Format("Предыдущий рекорд: ($ {0})", Highscores_FortBoyard.Instance.maxValue);
            }
        }

        IEnumerator ShowPojertvovanieUI()
        {
            yield return new WaitForSeconds(2f);
            pojertvovanieUItext.text = "Поздравляем ваш выигрыш составил:<size=70 ><b><color=#FF6400FF>\n" + "$ "
                                        + TotalCalculateCoins.ToString() + "</color></b></font>";
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
                Highscores_FortBoyard.Instance.UpdateDonationName();
                Highscores_FortBoyard.Instance.UpdatedScoreName();
                finishUI_nameText.text = "Уважаемый<b><color=#FF6400FF> " + Highscores_FortBoyard.Instance.realname + "</color></b></font>!";
                finishUI_resultMoneyText.text = "<size=40> Ваш результат<b><color=#FF6400FF>"+ " $ "+ TotalCalculateCoins.ToString() + "</color> был добавлен на доску рекордов.</size>";
                finishUI_positionLeaderboardText.text = "Ваша позиция в рейтинге: <size=70><b><color=#FF6400FF>" + " - " + "</color></b></font>";
                finishUI_donateText.text = "Так же вы пожертвовали заработанные средства в:" + "\n" + "<size=50><b><color=#FF6400FF>" + DonationName + "</color></b></font>";
                selectDonateModalUI.SetActive(false);
                finishModalUI.SetActive(true);
            }
        }
        public void GameReload()
        {
            SupportScripts.Instance.UI.SetActive(true);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SupportScripts.Instance.UI.SetActive(true);
        }
    }
}