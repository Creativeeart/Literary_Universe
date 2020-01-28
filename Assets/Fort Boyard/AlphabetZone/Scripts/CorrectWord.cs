using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace cakeslice
{
    public class CorrectWord : MonoBehaviour
    {
        public GameObject alphabetZoneManager;
        public GateZoneController gateZoneController;
        public TextMeshProUGUI tips;
        public GameObject treasureCoinFall, treasureCoinSpawn;
        public GameObject alphabetUI;
        public float waitForMoneyFalling = 0f;
        bool runFakeSekonds = false;
        public TextMeshProUGUI word;
        public Animator alphabet_CamAnimator;
        public Animator headLion;
        public bool isWordCorrect = false;
        public bool runTime;
        public float seconds;
        public int maxChar = 0;
        public int curentChar = 0;
        public string correctWord;
        public string inputWord;
        public cakeslice.Outline[] outlines;
        public cakeslice.SelectChar[] selectChars;
        public Color red;
        public Color green;
        public Color defaultColor;
        //public CameraShake cameraShake;
        public AlertUI alertUI;
        void Start()
        {
            word.text = string.Empty;
            tips.text = gateZoneController.tipsText.text;
            maxChar = correctWord.Length;
            for (int i = 0; i < outlines.Length; i++) outlines[i].enabled = false;
            for (int i = 0; i < selectChars.Length; i++) selectChars[i].selectObject = false;
        }
        public void ClearOutline()
        {
            for (int i = 0; i < outlines.Length; i++) outlines[i].enabled = false;
            for (int i = 0; i < selectChars.Length; i++) selectChars[i].selectObject = false;
        }
        private void Update()
        {
            if (runTime) seconds += Time.deltaTime;
            if (seconds >= 3)
            {
                if (isWordCorrect)
                {
                    word.color = green;
                    RotationHead();
                    runTime = false;
                    seconds = 0;
                }
                else
                {
                    //cameraShake.shakeDuration = 0.5f;
                    word.color = red;
                    gateZoneController.TimeReducing(5);
                    ReturnToAlphabet();
                    runTime = false;
                    seconds = 0;
                }
            }
            if (runFakeSekonds) waitForMoneyFalling += Time.deltaTime;
            if ((waitForMoneyFalling >= 3) && (runFakeSekonds == true))
            {
                //treasureCoinFall.SetActive(true);
                if ((waitForMoneyFalling >= 4) && (runFakeSekonds == true))
                {
                    if ((waitForMoneyFalling >= 6) && (runFakeSekonds == true))
                    {
                        //treasureCam.SetActive(true);
                        runFakeSekonds = false;
                        alphabetZoneManager.SetActive(false);
                        FortBoyardGameController.Instance.alphabetZone = false;
                        FortBoyardGameController.Instance.gameRooms = false;
                        FortBoyardGameController.Instance.treasureZone = true;
                    }
                    FortBoyardGameController.Instance.FB_CamMovingController.CameraMovingToPoint(FortBoyardGameController.Instance.FB_CamMovingController.pointToTreasure_Zone);
                    //alphabet_CamAnimator.SetBool("GoToTreasureCam", true);
                    //treasureCoinSpawn.SetActive(true);
                    alphabetUI.SetActive(false);
                }
            }
        }
        public void RotationHead()
        {
            headLion.enabled = true;
            runFakeSekonds = true;
        }

        public void ReturnToAlphabet()
        {
            //alphabet_CamAnimator.SetBool("ViewLionHead", false);
            FortBoyardGameController.Instance.FB_CamMovingController.CameraMovingToPoint(FortBoyardGameController.Instance.FB_CamMovingController.pointToAlphabetZoneB);
            StartCoroutine("ClearInterfaceWaiting");
        }

        public void ClearInput()
        {
            inputWord = string.Empty;
            curentChar = 0;
            ClearOutline();
        }
        public void ClearInputByInterface()
        {
            ClearInput();
            word.text = string.Empty;
            word.color = defaultColor;
        }
        IEnumerator ClearInterfaceWaiting()
        {
            yield return new WaitForSeconds(1.0f);
            ClearInputByInterface();
        }

    }
}
