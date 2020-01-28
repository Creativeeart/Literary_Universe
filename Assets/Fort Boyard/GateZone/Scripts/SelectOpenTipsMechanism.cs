using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cakeslice
{
    public class SelectOpenTipsMechanism : MonoBehaviour
    {
        public Button nextZoneBTN;
        public AlertUI alertUI;
        public GateZoneController gateZoneController;
        public Outline _outLine;

        public Image readyImage;
        bool isClicked = true;

        void Start()
        {
            _outLine = gameObject.GetComponent<Outline>();
        }
        
        void OnMouseDown()
        {
            if (gateZoneController.greenLamp.activeSelf)
            {
                if (gateZoneController.countAddTips != 0)
                {
                    if (isClicked)
                    {
                        StartCoroutine("FiilImage");
                        gateZoneController.fortBoyardGameController._timerGame.RunTime = true;
                        gateZoneController.arrow3DTipsMechanism.SetActive(false);
                    }
                }
                else alertUI.ShowWarningModalWindow("Доступ запрещен. У вас нет подсказок. \nДополнительные подсказки вы можете взять на панели, в нижней части экрана");
            }
            else alertUI.ShowWarningModalWindow("Доступ запрещен. Сначала вставьте ключи в панель управления.");
        }

        void OnMouseEnter()
        {
            _outLine.enabled = true;
        }

        void OnMouseExit()
        {
            _outLine.enabled = false;
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
                    gateZoneController.OpenTip();
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
}