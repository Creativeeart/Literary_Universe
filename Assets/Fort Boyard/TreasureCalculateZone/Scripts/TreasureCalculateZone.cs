﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
namespace cakeslice
{
    public class TreasureCalculateZone : MonoBehaviour
    {
        public Chest chest;
        public GameObject UI;
        public Highscores_FortBoyard _highscores_FortBoyard;
        public HitColliderCoin _hitColliderCoin;
        public TimerGame _timerGame;
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
        public TextMeshProUGUI totalGoldsTextMeshPro, currentCoinstext;

        public GameObject uiCanvas;
        public GameObject alhpabetManager;
        public GameObject watchUI;
        public GameObject spawn1, spawn2;
        public float time = 1f;

        public TextMeshProUGUI recordText;
        public float totalCalculateCoins;
        float currentCoins;
        public string donationName;
        public bool check = false;

        public void CalculateGold()
        {
            //totalCalculateCoins = _hitColliderCoin.currentCoins * 63f;
            if (chest.countKeyOpened == 5)
            {
                totalCalculateCoins = chest.coinsBoyard * 63f;
            }
            else
            {
                totalCalculateCoins = 0;
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
                float result = Mathf.Lerp(0, totalCalculateCoins, fraction);
                totalGoldsTextMeshPro.text = "$ " + result.ToString();
                yield return null;
            }
            StartCoroutine("ShowPojertvovanieUI");
            if (totalCalculateCoins > _highscores_FortBoyard.maxValue)
            {
                recordText.text = "Вы установили новый рекорд!";
            }
            else
            {
                recordText.text = string.Format("Предыдущий рекорд: ($ {0})", _highscores_FortBoyard.maxValue);
            }
        }

        IEnumerator ShowPojertvovanieUI()
        {
            yield return new WaitForSeconds(2f);
            pojertvovanieUItext.text = "Поздравляем ваш выигрыш составил:<size=70 ><b><color=#FF6400FF>\n" + "$ "
                                        + totalCalculateCoins.ToString() + "</color></b></font>";
            pojertvovanieUI.SetActive(true);
        }
        public void SelectDonation(TextMeshProUGUI selecetDonationName)
        {
            donationName = selecetDonationName.text;
        }
        public void SendInfoPojertvovanie()
        {
            if (donationName != null)
            {
                _highscores_FortBoyard.UpdateDonationName();
                _highscores_FortBoyard.UpdatedScoreName();
                finishUI_nameText.text = "Уважаемый<b><color=#FF6400FF> " + _highscores_FortBoyard.realname + "</color></b></font>!";
                finishUI_resultMoneyText.text = "<size=40> Ваш результат<b><color=#FF6400FF>"+ " $ "+ totalCalculateCoins.ToString() + "</color> был добавлен на доску рекордов.</size>";
                finishUI_positionLeaderboardText.text = "Ваша позиция в рейтинге: <size=70><b><color=#FF6400FF>" + " - " + "</color></b></font>";
                finishUI_donateText.text = "Так же вы пожертвовали заработанные средства в:" + "\n" + "<size=50><b><color=#FF6400FF>" + donationName + "</color></b></font>";
                selectDonateModalUI.SetActive(false);
                finishModalUI.SetActive(true);
            }
        }
        public void GameReload()
        {
            Time.timeScale = 1;
            
            UI.SetActive(true);
            if (GameObject.FindGameObjectWithTag("UI"))
            {
                UI = GameObject.FindGameObjectWithTag("UI");
            }
            else
            {
                Debug.Log("UI - не найден либо отключен!");
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //UI.SetActive(false);
            
        }
        private void Start()
        {
            if (GameObject.FindGameObjectWithTag("UI"))
            {
                UI = GameObject.FindGameObjectWithTag("UI");
            }
            else
            {
                Debug.Log("UI - не найден либо отключен!");
            }
        }
        void Update()
        {
            if (check)
            {
                alhpabetManager.SetActive(false);
                spawn1.SetActive(false);
                spawn2.SetActive(false);
                //treasurZoneCamera.SetActive(false);
                //treasureCalculateZoneCamera.SetActive(true);
                //_highscores_FortBoyard._fortBoyardGameController.FB_CamMovingController.CameraMovingToPoint(_highscores_FortBoyard._fortBoyardGameController.FB_CamMovingController.pointToTreasure_Calculate_Zone_A);
                //StartCoroutine(Wait());
                uiCanvas.SetActive(true);
                //currentCoins = _hitColliderCoin.currentCoins;
                if (chest.countKeyOpened == 5)
                {
                    currentCoins = chest.coinsBoyard;
                }
                else
                {
                    currentCoins = 0;
                }
                currentCoinstext.text = currentCoins.ToString();
                _hitColliderCoin.enabled = false;
                watchUI.SetActive(false);
                check = false;
            }
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
            _highscores_FortBoyard._fortBoyardGameController.FB_CamMovingController.CameraMovingToPoint(_highscores_FortBoyard._fortBoyardGameController.FB_CamMovingController.pointToTreasure_Calculate_Zone_B);
        }
    }
}