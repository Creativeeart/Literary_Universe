using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
public class EscapeFromShip : MonoBehaviour {
	[Header("Имя Игрока")]
	public InputField enterName;
	public Text NamePlayer;

	[Header("Текст-подсказка")]
	public Text InfoText;

	[Header("Объекты для включения/выключения")]
	public GameObject [] GamePanels;
	public GameObject [] OtherObjects;

	public int SuccessfulCount = 0;
	public int ErrorsCount = 0;

	private TimerGame _timerGame;
	private DisplayScores _displayScores;
	private RandomSpawn _randomSpawn;
	public bool nameEnetered;
	public bool runEscapeFromShip = false;
	void Start(){
		_timerGame = gameObject.GetComponent<TimerGame>();
		_displayScores = gameObject.GetComponent<DisplayScores>();
		_randomSpawn = gameObject.GetComponent<RandomSpawn>();
		InfoText.text = "";

	}
		
	void Update () {
		if (SuccessfulCount > 3){
			InfoText.text = "";
		}
		enterName.ActivateInputField();
		if (_timerGame.RunTime) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Restart ();
			}
		}
		if (runEscapeFromShip) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 100f)) {
					if (hit.transform.GetComponent<Rigidbody> () != null) {
						VerifyChecked vc = hit.transform.GetComponent<VerifyChecked> ();
						if (vc == null)
							return;
						vc.AddCount ();
					} 
				}
			}
		}
	}

	public void Reseter(){
		_displayScores.scoreCount = _displayScores.oldScore;
		_timerGame.seconds = _timerGame.oldTime;
		_timerGame.RunTime = false;
		_timerGame.EndTime = false;
		ErrorsCount = 0;
		SuccessfulCount = 0;
		InfoText.text = "";
		_randomSpawn.DeleteSpawnObjects ();
		runEscapeFromShip = false;

	}

	public void CloseGame(){
		OtherObjects[2].SetActive(true);
		OtherObjects[3].SetActive(false);
		GamePanels[1].SetActive (false);
		GamePanels [0].SetActive (false);
		Reseter ();
	}
	public void FunctionClose(){
		GamePanels [0].SetActive (false);
		GamePanels [2].SetActive (false);
		GamePanels [3].SetActive (false);
		GamePanels [4].SetActive (false);
		GamePanels [5].SetActive (false);
		GamePanels [1].SetActive (true);
		Reseter ();
	}
	public void OnGame(){
		FindObjectOfType<AudioManager> ().Play ("Shum_morya");
		if (nameEnetered == false) {
			GamePanels [0].SetActive (true);
		} else {
			GamePanels [1].SetActive (true);
			NamePlayer.text = "С возвращением " + "<b><color=#ffa500ff>" + enterName.text + "</color></b>" + "!";
		}
	}

	public void Restart(){
		GamePanels[2].SetActive (true);
		GamePanels[3].SetActive (false);
		Reseter ();
		_randomSpawn.RandomSpawner ();
		_timerGame.RunTime = true;
		runEscapeFromShip = true;

	}
	public void OpenLeaderBoardAffterGame(){
		GamePanels[4].SetActive (false);
		GamePanels[5].SetActive (true);
		Reseter();
	}
		


	public void Rename(){
		GamePanels[0].SetActive (true);
		GamePanels[1].SetActive (false);
	}
	public void EnterNameForm(){
		
		string username = enterName.text;
		if (enterName.text == "") {
			Debug.Log ("Пожалуйста введит ваше имя");
		} else {
			NamePlayer.text = "Здравствуй " + "<b><color=#ffa500ff>" + enterName.text + "</color></b>" + "!";
			GamePanels[0].SetActive (false);
			GamePanels[1].SetActive (true);
			nameEnetered = true;
		}
	}
	public void StartGame(){
		GamePanels[1].SetActive (false);
		GamePanels[2].SetActive (true);
		runEscapeFromShip = true;
		_timerGame.RunTime = true;
		_randomSpawn.RandomSpawner ();

	}
	public void OpenLeaderBoard(){
		GamePanels[1].SetActive (false);
		GamePanels[5].SetActive (true);
	}
	public void CloseLeaderBoard(){
		GamePanels[5].SetActive (false);
		GamePanels[1].SetActive (true);
	}
	public void OutShip(){
		
		if (ErrorsCount >= 4) {
			_timerGame.RunTime = false;
			runEscapeFromShip = false;
			GamePanels [3].SetActive (true);
			GamePanels [2].SetActive (false);
			OtherObjects [0].SetActive (false);
			OtherObjects [1].SetActive (true);
		}
		if (SuccessfulCount >=4 && ErrorsCount < 4) {
			_timerGame.RunTime = false;
			runEscapeFromShip = false;
			GamePanels [2].SetActive (false);
			GamePanels [4].SetActive (true);
			UpdatedScoreName ();
		}
		if (SuccessfulCount <= 3) {
			InfoText.text = "Выбрано мало вещей!";
		} 
	}
	public void UpdatedScoreName(){
		string username = enterName.text;
		Highscores.AddNewHighscore (username, _displayScores.ScoreTime);
	}
}
