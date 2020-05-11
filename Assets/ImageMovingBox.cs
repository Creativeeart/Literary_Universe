using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageMovingBox : MonoBehaviour {
    public Button NextBTN, PrevBTN;
    public GameObject[] ImageHolders;
    public TextMeshProUGUI countText;
    int CurrentPos = 0;
    int j = 1;

    private void Start()
    {
        countText.text = "Страница: " + j + " / " + ImageHolders.Length;
    }

    public void Next()
    {
        if (CurrentPos < ImageHolders.Length - 1)
        {
            PrevBTN.interactable = true;
            CurrentPos++;
        }
        if (CurrentPos == ImageHolders.Length - 1)
        {
            NextBTN.interactable = false;
        }
        for (int i = 0; i < ImageHolders.Length; i++)
        {
            ImageHolders[i].SetActive(false);
        }
        ImageHolders[CurrentPos].SetActive(true);
        j++;
        countText.text = "Страница: " + j + " / " + ImageHolders.Length;
    }
    public void Prev()
    {
        if (CurrentPos > 0)
        {
            CurrentPos--;
            NextBTN.interactable = true;
        }
        if (CurrentPos == 0)
        { 
            PrevBTN.interactable = false;
        }
        for (int i = 0; i < ImageHolders.Length; i++)
        {
            ImageHolders[i].SetActive(false);
        }
        ImageHolders[CurrentPos].SetActive(true);
        j--;
        countText.text = "Страница: " + j + " / " + ImageHolders.Length;
    }
}
