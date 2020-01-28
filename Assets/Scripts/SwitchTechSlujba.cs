using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTechSlujba : MonoBehaviour {
	public GameObject[] content;
	// Use this for initialization
	public void ActivateContent(int i){
		for (int j = 0; j < content.Length; j++) {
			content [j].SetActive (false);
		}
		content [i].SetActive (true);
	}
}
