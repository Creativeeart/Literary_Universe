using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cakeslice
{
    public class SelectOpenTipsMechanism : MonoBehaviour
    {
        public Button nextZoneBTN;
        public Outline _outLine;

        public Image readyImage;
        bool isClicked = true;

        void Start()
        {
            _outLine = gameObject.GetComponent<Outline>();
        }
        
        void OnMouseDown()
        {
            if (GateZoneController.Instance.greenLamp.activeSelf)
            {
                if (GateZoneController.Instance.countAddTips != 0)
                {
                    if (isClicked)
                    {
                        if (GateZoneController.Instance.isOpenTipsMechanismEnabled)
                        {
                            StartCoroutine(FiilImage());
                            TimerGame.Instance.RunTime = true;
                            GateZoneController.Instance.arrow3DTipsMechanism.SetActive(false);
                        }
                    }
                }
                else AlertUI.Instance.ShowAlert_DEFAULT("Доступ запрещен. У вас нет подсказок. \nДополнительные подсказки вы можете взять на панели, в нижней части экрана");
            }
            else AlertUI.Instance.ShowAlert_DEFAULT("Доступ запрещен. \nСначала вставьте ключи в панель управления.");
        }

        void OnMouseEnter()
        {
            if (GateZoneController.Instance.isOpenTipsMechanismEnabled)
            {
                _outLine.enabled = true;
            }
        }

        void OnMouseExit()
        {
            if (GateZoneController.Instance.isOpenTipsMechanismEnabled)
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
                    GateZoneController.Instance.OpenTip();
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