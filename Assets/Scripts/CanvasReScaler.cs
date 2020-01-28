using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasReScaler : MonoBehaviour {

    public CanvasScaler[] canvas;

    public void IncreaseLevelDefault()
    {
        for (int i = 0; i < canvas.Length; i++)
            canvas[i].matchWidthOrHeight = 1;
    }
    public void IncreaseLevelOne()
    {
        for (int i = 0; i < canvas.Length; i++)
            canvas[i].matchWidthOrHeight = 0.7f;
    }
    public void IncreaseLevelTwo()
    {
        for (int i = 0; i < canvas.Length; i++)
            canvas[i].matchWidthOrHeight = 0.5f;
    }
}
