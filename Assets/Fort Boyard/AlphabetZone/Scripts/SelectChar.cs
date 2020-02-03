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
            if (AlphabetZoneController.Instance.IsEnableChars)
            {
                if (!AlertUI.Instance.isAlertUIActive)
                {
                    if (AlphabetZoneController.Instance.CurentChar != AlphabetZoneController.Instance.MaxChar)
                    {
                        _outLine.enabled = true;

                        if (!selectObject)
                        {
                            selectObject = true;
                            AlphabetZoneController.Instance.Word.Add(gameObject.name);
                            AlphabetZoneController.Instance.LastChar = gameObject.name;
                            AlphabetZoneController.Instance.WordUI_TextMeshPro.text = AlphabetZoneController.Instance.MergeText();
                            AlphabetZoneController.Instance.InputWord = AlphabetZoneController.Instance.WordUI_TextMeshPro.text;
                            //AlphabetZoneController.Instance.word.text += gameObject.name;
                            AlphabetZoneController.Instance.CurentChar += 1;
                            //AlphabetZoneController.Instance.inputWord = AlphabetZoneController.Instance.word.text;

                            if (AlphabetZoneController.Instance.CurentChar == AlphabetZoneController.Instance.MaxChar)
                            {
                                if (AlphabetZoneController.Instance.InputWord == AlphabetZoneController.Instance.CorrectWord)
                                {
                                    //Debug.Log("Слово ВЕРНОЕ");
                                    AlphabetZoneController.Instance.RunTime = true;
                                    FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToAlphabetZoneA);
                                    AlphabetZoneController.Instance.IsWordCorrect = true;
                                    AlphabetZoneController.Instance.IsEnableChars = false;
                                }
                                else
                                {
                                    //Debug.Log("Слово НЕВЕРНОЕ");
                                    AlphabetZoneController.Instance.RunTime = true;
                                    FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToAlphabetZoneA);
                                    AlphabetZoneController.Instance.IsWordCorrect = false;
                                    AlphabetZoneController.Instance.ClearInput();
                                }
                            }
                        }
                        else
                        {
                            if (AlphabetZoneController.Instance.Word.Contains(AlphabetZoneController.Instance.LastChar))
                            {
                                if (AlphabetZoneController.Instance.LastChar == gameObject.name)
                                {
                                    AlphabetZoneController.Instance.Word.RemoveAt(AlphabetZoneController.Instance.Word.Count - 1);
                                    AlphabetZoneController.Instance.LastChar = gameObject.name;
                                    selectObject = false;
                                    AlphabetZoneController.Instance.WordUI_TextMeshPro.text = AlphabetZoneController.Instance.MergeText();
                                    AlphabetZoneController.Instance.InputWord = AlphabetZoneController.Instance.WordUI_TextMeshPro.text;
                                    AlphabetZoneController.Instance.CurentChar -= 1;
                                }
                                else
                                {
                                    //Debug.LogFormat("Вы можете удалить только последний введенный символ: {0}", AlphabetZoneController.Instance.LastWord);
                                    AlertUI.Instance.ShowWarningModalWindow("Вы можете удалить только последний введенный символ: <b>'" + AlphabetZoneController.Instance.LastChar + "'.</b>" +
                                        "\nЧтобы стереть все буквы, нажмите кнопку 'Стереть слово'");
                                }
                            }
                            //AlertUI.Instance.ShowWarningModalWindow("Буквы нельзя выбрать дважды! Если ошиблись при вводе буквы, нажмите кнопку 'Стереть слово'");
                        }
                    }
                }
            }
        }
        
        void OnMouseEnter()
        {
            if (AlphabetZoneController.Instance.IsEnableChars)
            {
                if (selectObject == false) _outLine.enabled = true;
            }
        }

        void OnMouseExit()
        {
            if (AlphabetZoneController.Instance.IsEnableChars)
            {
                if (selectObject == false) _outLine.enabled = false;
            }
        }

    }
}