using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace cakeslice
{
    public class SelectChar : MonoBehaviour
    {
        public Outline _outLine;
        public bool selectObject = false;
        void Start()
        {
            _outLine = gameObject.GetComponent<Outline>();
            _outLine.enabled = false;
            selectObject = false;
        }

        void OnMouseDown()
        {
            if (!AlertUI.Instance.isAlertUIActive)
            {
                if (AlphabetZoneController.Instance.curentChar != AlphabetZoneController.Instance.maxChar)
                {
                    _outLine.enabled = true;

                    if (!selectObject)
                    {
                        selectObject = true;
                        AlphabetZoneController.Instance.word.text = AlphabetZoneController.Instance.word.text + gameObject.name;
                        AlphabetZoneController.Instance.curentChar += 1;
                        AlphabetZoneController.Instance.inputWord = AlphabetZoneController.Instance.word.text;

                        if (AlphabetZoneController.Instance.curentChar == AlphabetZoneController.Instance.maxChar)
                        {
                            if (AlphabetZoneController.Instance.inputWord == AlphabetZoneController.Instance.correctWord)
                            {
                                //Debug.Log("Слово ВЕРНОЕ");
                                AlphabetZoneController.Instance.runTime = true;
                                //AlphabetZoneController.Instance.alphabet_CamAnimator.SetBool("ViewLionHead", true);
                                FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToAlphabetZoneA);
                                AlphabetZoneController.Instance.isWordCorrect = true;
                            }
                            else
                            {
                                //Debug.Log("Слово НЕВЕРНОЕ");
                                AlphabetZoneController.Instance.runTime = true;
                                //AlphabetZoneController.Instance.alphabet_CamAnimator.SetBool("ViewLionHead", true);
                                FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToAlphabetZoneA);
                                AlphabetZoneController.Instance.isWordCorrect = false;
                                AlphabetZoneController.Instance.ClearInput();
                            }
                        }
                    }
                    else
                    {
                        AlertUI.Instance.ShowWarningModalWindow("Буквы нельзя выбрать дважды!");
                    }
                }
            }
        }
        
        void OnMouseEnter()
        {
            if (selectObject == false) _outLine.enabled = true;
        }

        void OnMouseExit()
        {
            if (selectObject == false) _outLine.enabled = false;
        }

    }
}