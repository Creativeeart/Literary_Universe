using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour {

	const string privateCode = "39lJ7r_3qUWfg_i7BWO2ZgNlWGsED6B0WYjJ6c4tLn5w";
	const string publicCode = "5ab88063012b2e1068cce1e2";
	const string webURL = "http://dreamlo.com/lb/";

	DisplayHighscores highscoreDisplay;
	public Highscore[] highscoresList;
	static Highscores instance;

	void Awake() {
		highscoreDisplay = GetComponent<DisplayHighscores> ();
		instance = this;
	}

	public static void AddNewHighscore(string username, int score) {
		instance.StartCoroutine(instance.UploadNewHighscore(username,score));
	}

	IEnumerator UploadNewHighscore(string username, int score) {
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty(www.error)) {
			Debug.Log ("Успешно загружено в онлайн базу");
			DownloadHighscores();
		}
		else {
			Debug.Log ("Ошибка загрузки в онлайн базу: " + www.error);
		}
	}

	public void DownloadHighscores() {
		StartCoroutine("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase() {
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if (string.IsNullOrEmpty (www.error)) {
			FormatHighscores (www.text);
			highscoreDisplay.OnHighscoresDownloaded(highscoresList);
			Debug.Log ("База загружена");
		}
		else {
			Debug.Log ("Ошибка Загрузки: " + www.error);
		}
	}

	void FormatHighscores(string textStream) {
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i <entries.Length; i ++) {
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new Highscore(username,score);
		}
	}
	public void ClearLeaderBoard(){
		StartCoroutine ("SendServerCommandClearLeaderBoard");
		for (int i = 0; i < highscoreDisplay.highscoreFields.Length; i ++) {
			highscoreDisplay.highscoreFields[i].text = i+1 + ". Загрузка данных...";
			highscoreDisplay.highscoreRecord [i].text = "";
		}
		DownloadHighscores ();
	}
	IEnumerator SendServerCommandClearLeaderBoard(){
		WWW www = new WWW(webURL + privateCode + "/clear/");
		yield return www;
	}


}

public struct Highscore {
	public string username;
	public int score;

	public Highscore(string _username, int _score) {
		username = _username;
		score = _score;
	}

}