using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuShip : MonoBehaviour {
	
	public GameObject menu;
	bool menuChecked = false;
	public GameObject fpsController;
	public GameObject dopCamera;
	public GameObject mainCamera;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			menuChecked = !menuChecked;
		}
		if (menuChecked == true) {
			menu.SetActive (true);
			Time.timeScale = 0;
			dopCamera.SetActive (true);
			fpsController.SetActive (false);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else{
			MenuHidden ();
		}
	}
	public void MenuHidden(){
		menu.SetActive (false);
		Time.timeScale = 1;
		dopCamera.SetActive (false);
		fpsController.SetActive (true);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

}
