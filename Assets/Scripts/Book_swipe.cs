using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class Book_swipe : MonoBehaviour {
    public GameObject[] books;
    [Header("Прогресс бар")]
    public Image progressBarImageFilling;
    public TextMeshProUGUI countText;
    int i = 0;
    int j = 1;
    [Range(0, 100)] public float speed = 60;
    float currentPercent = 0.0f;
    float stepPercent = 0.0f;


    public void RightMove()
    {
        if (i != 0)
        {
            MoveDoTween(books[i], 1090);
            MoveDoTween(books[i - 1], 0);
            i--;
            j--;
        }
    }

    public void LeftMove()
    {
        if (i < books.Length-1)
        {
            MoveDoTween(books[i], -1090);
            MoveDoTween(books[i + 1], 0);
            i++;
            j++;
            
        }
    }
    private void Update()
    {
        countText.text = j + " / " + books.Length;

        if (currentPercent <= stepPercent)
        {
            currentPercent += speed * Time.deltaTime;
        }
        if (currentPercent >= stepPercent)
        {
            currentPercent -= speed * Time.deltaTime;
        }
        stepPercent = (float)j / (float)books.Length;
        progressBarImageFilling.fillAmount = currentPercent;
    }

    void MoveDoTween(GameObject book, float endPosition)
    {
        DOTween.defaultEaseType = Ease.InOutBack;
        book.GetComponent<RectTransform>().DOLocalMoveX(endPosition, 1);
    }
}
