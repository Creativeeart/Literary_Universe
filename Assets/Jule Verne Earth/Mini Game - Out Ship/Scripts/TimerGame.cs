using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerGame : MonoBehaviour {
	[Header("Таймер")]
	public TextMeshProUGUI time;
	public float seconds;
	public float endTime; 
	public bool reverse  = false;
	public bool warningTime = true;

	public bool EndTime = false;
	public bool RunTime = false;
	public float oldTime;
	public Animation punchText;
	private TimerEndFunction _timerEndFunction;


	void Start(){
		_timerEndFunction = gameObject.GetComponent<TimerEndFunction>();
		oldTime = seconds;
	}

	void Update(){
		if (RunTime) {
			if (reverse) {
				endTime = 0;
				if (seconds <= endTime) {
					seconds = 0;
                    if (_timerEndFunction != null) _timerEndFunction.FunctionEndTime ();
				} else {
					seconds -= Time.deltaTime;
				}
			} 
			else {
				if (seconds >= endTime){
					seconds = endTime;
					_timerEndFunction.FunctionEndTime ();
				} else {
					seconds += Time.deltaTime;
				}
			}	
			time.text = string.Format ("{0:00}:{1:00}", (int)seconds / 60, (int)seconds % 60);
			if (warningTime) {
				if (seconds <= 5) {
					time.text = "<color=#FF2100FF>" + time.text + "</color>";
					punchText.Play ();
				} else {
                    //time.text = "<color=#ffa500ff>" + time.text + "</color>";
                    time.text = time.text;
                    foreach (AnimationState state in punchText) {
						state.time = 0;
					}
				}
			}
		}
	}


}

