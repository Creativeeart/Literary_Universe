using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisuallyImpaired : MonoBehaviour {
    public bool activateDeactivateObject = false; //fix ScrollView drawing
    public bool positionUse = true;
    public bool scaleUse = true;
    public bool textMeshProUse = false;

    [Header("Значения по умолчанию")]
    public Vector2 PositionLevelDefault;
    public Vector2 ScaleLevelDefault;
    public float defaultFontSize;

    [Header("Увеличение 1.5x")]
    public Vector2 newPositionLevelOne;
    public Vector2 newScaleLevelOne;
    public float newFontSizeLevelOne;

    [Header("Увеличение 2.0x")]
    public Vector2 newPositionLevelTwo;
    public Vector2 newScaleLevelTwo;
    public float newFontSizeLevelTwo;

    TriggerVisuallyImpared _triggerVisuallyImpared;

    private void Start()
    {
        //_triggerVisuallyImpared = GameObject.Find("Main UI Controller").GetComponent<TriggerVisuallyImpared>();
    }
    public void IncreaseLevelDefault() //1.0x 
    {
        if (positionUse)
        {
            GetComponent<RectTransform>().anchoredPosition = PositionLevelDefault;
        }
        if (scaleUse)
        {
            GetComponent<RectTransform>().localScale = ScaleLevelDefault;
        }
        if (textMeshProUse)
        {
            GetComponent<TextMeshProUGUI>().fontSize = defaultFontSize;
        }

    }
    public void IncreaseLevelOne() //1.5x
    {
        if (positionUse)
        {
            GetComponent<RectTransform>().anchoredPosition = newPositionLevelOne;
        }
        if (scaleUse)
        {
            GetComponent<RectTransform>().localScale = newScaleLevelOne;
        }
        if (textMeshProUse)
        {
            GetComponent<TextMeshProUGUI>().fontSize = newFontSizeLevelOne;
        }
    }
    public void IncreaseLevelTwo() //2.0x
    {
        if (positionUse)
        {
            GetComponent<RectTransform>().anchoredPosition = newPositionLevelTwo;
        }
        if (scaleUse)
        {
            GetComponent<RectTransform>().localScale = newScaleLevelTwo;
        }
        if (textMeshProUse)
        {
            GetComponent<TextMeshProUGUI>().fontSize = newFontSizeLevelTwo;
        }
    }
    private void Update()
    {
        //if (activateDeactivateObject)
        //{
        //    gameObject.SetActive(false);
        //    gameObject.SetActive(true);
        //}
        //if (_triggerVisuallyImpared.increaseLevelOne)
        //{
        //    IncreaseLevelOne();
        //}
        //if (_triggerVisuallyImpared.increaseLevelTwo)
        //{
        //    IncreaseLevelTwo();
        //}
        //if (_triggerVisuallyImpared.increaseLevelOne == false && _triggerVisuallyImpared.increaseLevelTwo == false)
        //{
        //    IncreaseLevelDefault();
        //}
    }
}
