using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGameQuiz : MonoBehaviour {
	[Header("Таймер")]
	public Text time;
	public float seconds;
	public float endTime; 
	public bool reverse  = false;
	public bool warningTime = true;

	[HideInInspector] public bool EndTime = false;
	[HideInInspector] public bool RunTime = false;
	[HideInInspector] public float oldTime;
	public Animation punchText;
	private GameScript _GameScript;


	void Start(){
		_GameScript = gameObject.GetComponent<GameScript>();
		oldTime = seconds;
	}

	void Update(){
		if (RunTime) {
			if (reverse) {
				endTime = 0;
				if (seconds <= endTime) {
					seconds = 0;
					_GameScript.EndTimeFunction ();
				} else {
					seconds -= Time.unscaledDeltaTime;
				}
			} 
			else {
				if (seconds >= endTime){
					seconds = endTime;
					_GameScript.EndTimeFunction ();
				} else {
					seconds += Time.unscaledDeltaTime;
				}
			}	
			time.text = string.Format ("{0:00}:{1:00}", (int)seconds / 60, (int)seconds % 60);
			if (warningTime) {
				if (seconds <= 5) {
					time.text = "<color=#FF2100FF>" + time.text + "</color>";
					punchText.Play ();
				} else {
					time.text = "<color=#ffa500ff>" + time.text + "</color>";
					foreach (AnimationState state in punchText) {
						state.time = 0;
					}
				}
			}
		}
	}


}

