using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using cakeslice;
public class SelectChar : MonoBehaviour
{
    public Outline _outLine;
    public bool selectObject = false;
    AlphabetZoneController AlphabetZoneController;
    AlertUI AlertUI;
    FB_CamMovingController FB_CamMovingController;
    void Start()
    {
        AlphabetZoneController = AlphabetZoneController.Instance;
        AlertUI = AlertUI.Instance;
        FB_CamMovingController = FB_CamMovingController.Instance;
        _outLine = gameObject.GetComponent<Outline>();
        _outLine.enabled = false;
        selectObject = false;
    }

    void OnMouseDown()
    {
        if (AlphabetZoneController.IsEnableChars)
        {
            if (!AlertUI.isAlertUIActive)
            {
                if (AlphabetZoneController.CurentChar != AlphabetZoneController.MaxChar)
                {
                    _outLine.enabled = true;

                    if (!selectObject)
                    {
                        selectObject = true;
                        gameObject.transform.DOLocalMoveY(0, 0.6f);
                        AlphabetZoneController.Word.Add(gameObject.name);
                        AlphabetZoneController.LastChar = gameObject.name;
                        AlphabetZoneController.WordUI_TextMeshPro.text = AlphabetZoneController.MergeText();
                        AlphabetZoneController.InputWord = AlphabetZoneController.WordUI_TextMeshPro.text;
                        //AlphabetZoneController.word.text += gameObject.name;
                        AlphabetZoneController.CurentChar += 1;
                        //AlphabetZoneController.inputWord = AlphabetZoneController.word.text;

                        if (AlphabetZoneController.CurentChar == AlphabetZoneController.MaxChar)
                        {
                            if (AlphabetZoneController.InputWord == AlphabetZoneController.CorrectWord)
                            {
                                //Debug.Log("Слово ВЕРНОЕ");
                                AlphabetZoneController.RunTime = true;
                                FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToAlphabetZoneA);
                                AlphabetZoneController.IsWordCorrect = true;
                                AlphabetZoneController.IsEnableChars = false;
                            }
                            else
                            {
                                //Debug.Log("Слово НЕВЕРНОЕ");
                                AlphabetZoneController.RunTime = true;
                                FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToAlphabetZoneA);
                                AlphabetZoneController.IsWordCorrect = false;
                                AlphabetZoneController.ClearInput();
                            }
                        }
                    }
                    else
                    {
                        if (AlphabetZoneController.Word.Contains(AlphabetZoneController.LastChar))
                        {
                            if (AlphabetZoneController.LastChar == gameObject.name)
                            {
                                AlphabetZoneController.Word.RemoveAt(AlphabetZoneController.Word.Count - 1);
                                AlphabetZoneController.LastChar = gameObject.name;
                                selectObject = false;
                                AlphabetZoneController.WordUI_TextMeshPro.text = AlphabetZoneController.MergeText();
                                AlphabetZoneController.InputWord = AlphabetZoneController.WordUI_TextMeshPro.text;
                                AlphabetZoneController.CurentChar -= 1;
                                gameObject.transform.DOLocalMoveY(0.06f, 0.6f);
                            }
                            else
                            {
                                //Debug.LogFormat("Вы можете удалить только последний введенный символ: {0}", AlphabetZoneController.LastWord);
                                AlertUI.ShowAlert_DEFAULT("Вы можете удалить только последний введенный символ: <b>'" + AlphabetZoneController.LastChar + "'.</b>" +
                                    "\nЧтобы стереть все буквы, нажмите кнопку 'Стереть слово'");
                            }
                        }
                        //AlertUI.ShowWarningModalWindow("Буквы нельзя выбрать дважды! Если ошиблись при вводе буквы, нажмите кнопку 'Стереть слово'");
                    }
                }
            }
        }
    }

    void OnMouseEnter()
    {
        if (AlphabetZoneController.IsEnableChars)
        {
            if (selectObject == false) _outLine.enabled = true;
        }
    }

    void OnMouseExit()
    {
        if (AlphabetZoneController.IsEnableChars)
        {
            if (selectObject == false) _outLine.enabled = false;
        }
    }

}
