using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour {

	public Text[] highscoreFields;
	public Text[] highscoreRecord;
	Highscores highscoresManager;

	void Start() {
		for (int i = 0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i+1 + ". Загрузка данных......";
			highscoreRecord [i].text = "";
		}


		highscoresManager = GetComponent<Highscores>();
		StartCoroutine("RefreshHighscores");
	}

	public void OnHighscoresDownloaded(Highscore[] highscoreList) {
		for (int i =0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i+1 + ".          ";
			if (i < highscoreList.Length) {
				highscoreFields[i].text += highscoreList[i].username;
				highscoreRecord [i].text = highscoreList [i].score.ToString ();
			}
		}
	}


	IEnumerator RefreshHighscores() {
		while (true) {
			highscoresManager.DownloadHighscores();
			yield return new WaitForSeconds(15);
		}
	}
}