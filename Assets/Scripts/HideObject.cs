using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour {
	public GameObject YourObject;
	bool menuChecked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.H)) {
			menuChecked = !menuChecked;

		}
		if (menuChecked == true) {
			YourObject.SetActive (false);
		} else {
			YourObject.SetActive (true);
		}
	}
}
