using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)){
			int score = Random.Range(0,2000);
			string username = "";
			string alphabet = "абвгшдийклмонпрстуфхщшя";
			for (int i = 0; i< Random.Range(5,10); i++){
				username +=alphabet[Random.Range(0, alphabet.Length)];
			}
				Highscores.AddNewHighscore(username,score);

		}
	}
}

