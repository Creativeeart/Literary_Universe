using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScores : MonoBehaviour {
	[Header("Очки")]
	public Text textScore;
	public float scoreCount;
	[HideInInspector] public int ScoreTime;
	[HideInInspector] public float oldScore;
	
	private TimerGame _timeGame;

	void Start () {
		_timeGame = gameObject.GetComponent<TimerGame>();
		oldScore = scoreCount;
	}

	void Update () {
		if (_timeGame.RunTime) {
			scoreCount = scoreCount - (Time.deltaTime * 10);
			ScoreTime = (int)(scoreCount);
			textScore.text = ScoreTime.ToString ();
		}
	}
}
