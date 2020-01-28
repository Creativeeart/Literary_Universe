using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerMy: MonoBehaviour {
	public float timerFire = 5f;
	public float max = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timerFire -= Time.deltaTime;
		if (timerFire <= 0) {
			Flicker();
			timerFire = 5f;
			if (GetComponent<Light> ().intensity >= 9f) {
				GetComponent<Light> ().intensity = 0f;
			}
		}
	}
	void Flicker(){
		GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity,max,Time.deltaTime);
	}
}
