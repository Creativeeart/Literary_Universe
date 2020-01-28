using UnityEngine;

public class Pause_game : MonoBehaviour {
	float timing = 1f;
	public bool isPaused = false;

	void Update () {
		Time.timeScale = timing;
		timing = isPaused ? 0 : 1;
	}
	public void CheckPause(){
		isPaused = !isPaused;
	}
}﻿

