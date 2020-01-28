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
        public CorrectWord _correctWord;
        void Start()
        {
            _outLine = gameObject.GetComponent<Outline>();
            _outLine.enabled = false;
            selectObject = false;
        }

        void OnMouseDown()
        {
            if (!_correctWord.alertUI.isAlertUIActive)
            {
                if (_correctWord.curentChar != _correctWord.maxChar)
                {
                    _outLine.enabled = true;

                    if (!selectObject)
                    {
                        selectObject = true;
                        _correctWord.word.text = _correctWord.word.text + gameObject.name;
                        _correctWord.curentChar += 1;
                        _correctWord.inputWord = _correctWord.word.text;

                        if (_correctWord.curentChar == _correctWord.maxChar)
                        {
                            if (_correctWord.inputWord == _correctWord.correctWord)
                            {
                                //Debug.Log("Слово ВЕРНОЕ");
                                _correctWord.runTime = true;
                                //_correctWord.alphabet_CamAnimator.SetBool("ViewLionHead", true);
                                FortBoyardGameController.Instance.FB_CamMovingController.CameraMovingToPoint(FortBoyardGameController.Instance.FB_CamMovingController.pointToAlphabetZoneA);
                                _correctWord.isWordCorrect = true;
                            }
                            else
                            {
                                //Debug.Log("Слово НЕВЕРНОЕ");
                                _correctWord.runTime = true;
                                //_correctWord.alphabet_CamAnimator.SetBool("ViewLionHead", true);
                                FortBoyardGameController.Instance.FB_CamMovingController.CameraMovingToPoint(FortBoyardGameController.Instance.FB_CamMovingController.pointToAlphabetZoneA);
                                _correctWord.isWordCorrect = false;
                                _correctWord.ClearInput();
                            }
                        }
                    }
                    else
                    {
                        _correctWord.alertUI.ShowWarningModalWindow("Буквы нельзя выбрать дважды!");
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