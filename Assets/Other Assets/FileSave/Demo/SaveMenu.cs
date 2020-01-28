using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour {
	public int saves; 
	private int currentSave; 

	//GUI variables
	private bool loadSelect;
	private bool saveSelect;
	private bool deleleSelect;
	private float guiHight;
	private float guiWight;
	// Use this for initialization
	void Start () {
		currentSave = PlayerPrefs.GetInt("CurrentSave"); //Getting the current file
	}

	void OnGUI(){
		if (!loadSelect && !saveSelect && !deleleSelect) {
			if (GUI.Button (new Rect (10, 10, 100, 50), "Save")) {
				GlobalSave._GlobalSave.Save(); //Saving the current file
			}

			//GUI
			if (GUI.Button (new Rect (10, 70, 100, 50), "Load")) {
				loadSelect = true;
			}
			if (GUI.Button (new Rect (10, 130, 100, 50), "Delete")) {
				deleleSelect = true;
			}
			if (saves != 0) {
				if (GUI.Button (new Rect (10, 190, 200, 50), "Save in other Slot")) {
					saveSelect = true;
				}
				GUI.Label (new Rect (120, 10, 100, 50), "Current Save: Save" + currentSave);
			}
		} else {
			if (GUI.Button (new Rect (10, 10, 100, 50), "Back")) {
				loadSelect = false;
				saveSelect = false;
				deleleSelect = false;
			}
			for (int i = 0; i < saves; i++) {
				if (i == 0) {
					guiWight = 10;
					guiHight = 70;
				} else {
					if (guiWight < 340) {
						guiWight += 110;
					} else {
						guiWight = 10;
						guiHight += 60;
					}
				}


				if (GUI.Button (new Rect (guiWight, guiHight, 100, 50), "Save" + i)) {
					PlayerPrefs.SetInt ("CurrentSave", i); //Setting file number
					if (loadSelect) { //Loading other save
//						Application.LoadLevel (); //Reload level
						SceneManager.LoadScene("Demo");
					} else if (saveSelect) { //Save the current save it in another file
						GlobalSave._GlobalSave.ResetPath (false); //Resetting file link.
						saveSelect = false;
					} else if (deleleSelect) {
						GlobalSave._GlobalSave.DeleteSaveFile (i);
						deleleSelect = false;
					}
				}
			}
		}
	}}