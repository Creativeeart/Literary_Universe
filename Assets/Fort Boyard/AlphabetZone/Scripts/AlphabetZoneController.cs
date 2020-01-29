using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace cakeslice
{
    public class AlphabetZoneController : MonoBehaviour
    {
        public GameObject UI_AlphabetZone;
        public TextMeshProUGUI tips;
        public GameObject treasureCoinFall, treasureCoinSpawn;
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
        public Outline[] outlines;
        public SelectChar[] selectChars;
        public Color red;
        public Color green;
        public Color defaultColor;
        //public CameraShake cameraShake;
        public static AlphabetZoneController Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void AlphabetZoneEntered()
        {
            FortBoyardGameController.Instance.IsAlphabetZone = true;
            UI_AlphabetZone.SetActive(true);
            word.text = string.Empty;
            tips.text = GateZoneController.Instance.tipsText.text;
            maxChar = correctWord.Length;
            for (int i = 0; i < outlines.Length; i++) outlines[i].enabled = false;
            for (int i = 0; i < selectChars.Length; i++) selectChars[i].selectObject = false;
        }

        public void ClearOutline()
        {
            for (int i = 0; i < outlines.Length; i++) outlines[i].enabled = false;
            for (int i = 0; i < selectChars.Length; i++) selectChars[i].selectObject = false;
        }
        void Update()
        {
            if (FortBoyardGameController.Instance.IsAlphabetZone)
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
                        GateZoneController.Instance.TimeReducing(5);
                        ReturnToAlphabet();
                        runTime = false;
                        seconds = 0;
                    }
                }
                if (runFakeSekonds) waitForMoneyFalling += Time.deltaTime;
                if ((waitForMoneyFalling >= 3) && (runFakeSekonds == true))
                {
                    if ((waitForMoneyFalling >= 4) && (runFakeSekonds == true))
                    {
                        if ((waitForMoneyFalling >= 6) && (runFakeSekonds == true))
                        {
                            runFakeSekonds = false;
                            FortBoyardGameController.Instance.IsGateZone = false;
                            FortBoyardGameController.Instance.IsAlphabetZone = false;
                            FortBoyardGameController.Instance.IsTreasureZone = true;
                            FortBoyardGameController.Instance.IsTreasureCalculateZone = false;
                        }
                        UI_AlphabetZone.SetActive(false);
                        StartCoroutine(FortBoyardGameController.Instance.GoToTreasureZone());
                    }
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
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToAlphabetZoneB);
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
