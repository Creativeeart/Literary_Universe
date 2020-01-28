using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEndFunction : MonoBehaviour {
	private TimerGame _timerGame;
	private EscapeFromShip _escapeFromShip;
	void Start(){
		_timerGame = gameObject.GetComponent<TimerGame>();
		_escapeFromShip = gameObject.GetComponent<EscapeFromShip>();
	}
	public void FunctionEndTime(){
		_timerGame.EndTime = true;
		if (_timerGame.EndTime) {
			_timerGame.RunTime = false;
			if (_escapeFromShip.runEscapeFromShip == true) {
				FailGameActivate ();
			}
		}
	}
	public void FailGameActivate(){
		_escapeFromShip.GamePanels [3].SetActive (true);
		_escapeFromShip.GamePanels [2].SetActive (false);
		_escapeFromShip.OtherObjects[0].SetActive(true);
	}
}
