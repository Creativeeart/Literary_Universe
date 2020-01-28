using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace cakeslice
{
    public class GateZoneController : MonoBehaviour
    {
        public bool isGateZone = false;
        public GameObject arrow3DKeysHolder, arrow3DTipsMechanism;
        public FortBoyardGameController fortBoyardGameController;
        public GameObject AlphabetZoneManager;
        public CameraDoorsController cameraDoorsController;
        public GameObject gateZoneCam;
        public AlertUI alertUI;

        [Space]
        public Animator greenLampAnimator;
        public Animator redLampAnimator;
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
        public GameObject[] keysSiluet;
        public GameObject[] tipsSiluet;
        public GameObject goToNextZoneBtn;

        bool runTime = false;
        readonly int needKeys = 3;
        int activeKeyCount = 0;


        public int insertKeysInHolder = 0;
        int countOpenedTips = 0;
        int countOpenedKeys = 0;


        public int countAddTips = 0;
        public int countAddKeys = 0;

        int counter = 0;
        int counter2 = 0;

        void Start()
        {
            isGateZone = true;
            CheckKeysAndTips();
            OnEnableRedLamp();
            for (int i = 0; i < fortBoyardGameController.tips.Length; i++) if (fortBoyardGameController.tips[i].activeSelf) countAddTips++;
            for (int i = 0; i < fortBoyardGameController.currentTips; i++) tips3DIcons[i].SetActive(true);

            for (int i = 0; i < fortBoyardGameController.keys.Length; i++)
            {
                if (i >= 3) break;
                if (fortBoyardGameController.keys[i].activeSelf) countAddKeys++;
            }
            for (int i = 0; i < fortBoyardGameController.currentKeys; i++)
            {
                if (i >= 3) break;
                keys3DIcons[i].SetActive(true);
                activeKeyCount++;
            }
        }

        void Update()
        {
            if (timeRotationGears <= 0)
            {
                runTime = false;
                timeRotationGears = 5.0f;
            }
            if (runTime)
            {
                timeRotationGears = timeRotationGears - Time.deltaTime;
                if (isOpenGate)
                {
                    for (int i = 0; i < gearsNormal.Length; i++) gearsNormal[i].transform.Rotate(new Vector3(0, 0, Time.deltaTime * speedGear));
                    for (int i = 0; i < gearsReverse.Length; i++) gearsReverse[i].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedGear));
                }
                if (isOpenGate == false)
                {
                    for (int i = 0; i < gearsNormal.Length; i++) gearsNormal[i].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedGear));
                    for (int i = 0; i < gearsReverse.Length; i++) gearsReverse[i].transform.Rotate(new Vector3(0, 0, Time.deltaTime * speedGear));
                }
            }
        }

        public void AddKeys()
        {
            fortBoyardGameController.currentKeys = fortBoyardGameController.currentKeys + 1;
            TimeReducing(10);
            countAddKeys++;
            for (int i = 0; i < countAddKeys; i++) keys3DIcons[i].SetActive(true);
            activeKeyCount++;
        }

        public void AddTips()
        {
            if (fortBoyardGameController.currentTips < fortBoyardGameController.totalTips)
            {
                fortBoyardGameController.currentTips = fortBoyardGameController.currentTips + 1;
                TimeReducing(10);
                countAddTips++;
                for (int i = 0; i < countAddTips; i++) tips3DIcons[i].SetActive(true);
            }
            if (fortBoyardGameController.currentTips == 5) addExtraTipButton.GetComponent<Button>().interactable = false;
        }

        public void TimeReducing(float countTime)
        {
            fortBoyardGameController.timeReducing.GetComponent<TextMeshProUGUI>().text = "- " + countTime;
            var go = Instantiate(fortBoyardGameController.timeReducing, fortBoyardGameController.timeReducingParent);
            Destroy(go, 1);
            fortBoyardGameController.totalTime = fortBoyardGameController.totalTime - countTime;
            fortBoyardGameController._timerGame.seconds = fortBoyardGameController._timerGame.seconds - countTime;
            fortBoyardGameController.ReloadTimer();
        }

        public void CheckKeysAndTips()
        {
            for (int i = 0; i <= 2; i++)
            {
                if (fortBoyardGameController.keys[i].activeSelf) buttonAddKeys[i].SetActive(false);
                else buttonAddKeys[i].SetActive(true);
            }
            for (int i = 0; i <= 2; i++)
            {
                if (fortBoyardGameController.tips[i].activeSelf) buttonAddTips[i].SetActive(false);
                else buttonAddTips[i].SetActive(true);
            }
            for (int i = 0; i < buttonAddKeys.Length; i++)
            {
                if (buttonAddKeys[i].activeSelf)
                {
                    keysSiluet[i].SetActive(false);
                }
            }
            for (int i = 0; i < buttonAddTips.Length; i++)
            {
                if (buttonAddTips[i].activeSelf)
                {
                    tipsSiluet[i].SetActive(false);
                }
            }
        }

        public void CountingKeys()
        {
            if (fortBoyardGameController.currentKeys < needKeys)
            {
                int how = needKeys - fortBoyardGameController.currentKeys;
                alertUI.ShowWarningModalWindow("Доступ запрещен. Вам не хватает ключей: (" + how + ")");
            }
            else
            {
                StartCoroutine("MaterialSwitching");
            }
        }

        public void CloseGate()
        {
            gateAnimation.SetBool("Closed", true);
        }

        public void EnableDisable3DIconTips()
        {
            countAddTips--;
            int result = fortBoyardGameController.currentTips - countOpenedTips;
            for (int i = 0; i < tips3DIcons.Length; i++) tips3DIcons[i].SetActive(false);
            for (int i = 0; i < result; i++) tips3DIcons[i].SetActive(true);
        }

        public void EnableDisable3DIconKeys()
        {
            countAddKeys--;
            int result = activeKeyCount - countOpenedKeys;
            for (int i = 0; i < keys3DIcons.Length; i++) keys3DIcons[i].SetActive(false);
            for (int i = 0; i < result; i++) keys3DIcons[i].SetActive(true);
        }

        public void GoToNextZone()
        {
            gateZoneCam.SetActive(false);
            AlphabetZoneManager.SetActive(false);
            cameraDoorsController.GoToAlphabet();
            fortBoyardGameController.mainUconsUI.SetActive(false);
            FortBoyardGameController.Instance.alphabetZone = true;
            FortBoyardGameController.Instance.treasureZone = false;
            FortBoyardGameController.Instance.gameRooms = false;
    }

        public void OpenTip()
        {
            if (tipsText.text == "Подсказки скрыты") tipsText.text = string.Empty;
            if (fortBoyardGameController.currentTips <= fortBoyardGameController.totalTips)
            {
                if (counter < fortBoyardGameController.currentTips)
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
            if (fortBoyardGameController.currentKeys <= fortBoyardGameController.totalKeys)
            {
                if (counter2 < fortBoyardGameController.currentKeys)
                {
                    counter2++;
                    countOpenedKeys++;
                    EnableDisable3DIconKeys();
                }
            }
        }

        public void OpenGateAndEnableOpenTipsMechanism()
        {
            runTime = true;
            isOpenGate = true;
            gateAnimation.enabled = true;
            OnEnableGreenLamp();
        }

        public void OnEnableGreenLamp()
        {
            greenLampAnimator.enabled = true;
            greenLamp.SetActive(true);
            redLampAnimator.enabled = false;
            redLamp.SetActive(false);
        }

        public void OnEnableRedLamp()
        {
            greenLampAnimator.enabled = false;
            greenLamp.SetActive(false);
            redLampAnimator.enabled = true;
            redLamp.SetActive(true);
        }


        IEnumerator MaterialSwitching()
        {
            int i = 0;
            while (i < fortBoyardGameController.currentKeys)
            {
                keyHolders[i].GetComponent<MeshRenderer>().material = emissionMaterialForKeyHolders;
                keyHolders[i].transform.GetChild(0).gameObject.SetActive(true);
                i++;
                if (i >= 3)
                {
                    runTime = true;
                    isOpenGate = true;
                    gateAnimation.enabled = true;
                    OnEnableGreenLamp();
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}

