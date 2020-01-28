using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FeelingTime : MonoBehaviour {
    public Text computerTimeText, userTimeText, howWinnerText;
    public float mainTime = 15f;
    public float min, max;
    float computerTime = 0;
    float userTime = 0;
    bool isStartGame = false;
    bool isStopUserTime = false;
    float rand = 0;

	void Update () {
        if (isStartGame)
        {
            if (computerTime < rand)
            {
                computerTime += Time.deltaTime;
                computerTimeText.text = "Computer Time: " + computerTime.ToString();
            }
            if (!isStopUserTime)
            {
                userTime += Time.deltaTime;
                userTimeText.text = "User Time: " + userTime.ToString();
            }
            if ((computerTime > rand) && isStopUserTime)
            {
                isStartGame = false;
            }
        }
    }
    public void StartGame()
    {
        isStartGame = true;
        rand = Random.Range(min, max);
    }
    public void StopUserTime()
    {
        isStopUserTime = true;
    }
    public void CalculateNumbers()
    {
        if ((computerTime > rand) && isStopUserTime)
        {
            float computer = Mathf.Abs(mainTime - computerTime);
            float user = Mathf.Abs(userTime - mainTime);

            if (computer < user)
            {
                howWinnerText.text = "Computer Winner!";
            }
            else if (computer > user)
            {
                howWinnerText.text = "User Winner!";
            }
            else
            {
                howWinnerText.text = "Draw!";
            }
        }
    }
}
