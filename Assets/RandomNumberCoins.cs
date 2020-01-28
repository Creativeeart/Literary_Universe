using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RandomNumberCoins : MonoBehaviour {
    Chest chest;
    public TextMeshProUGUI currentNumber_TextMeshPro;
    public TextMeshProUGUI currentNumber2_TextMeshPro;
    public int randomNumber = 0;
    
	void Start () {
        chest = GameObject.Find("Chest").GetComponent<Chest>();
        float tempCoins = chest.coinsBoyard / 50;
        //randomNumber = Random.Range(1, 6);
        randomNumber = (int)tempCoins;
        currentNumber_TextMeshPro.text = randomNumber.ToString();
        currentNumber2_TextMeshPro.text = randomNumber.ToString();
    }
}
