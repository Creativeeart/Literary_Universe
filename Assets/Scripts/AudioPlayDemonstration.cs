using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayDemonstration : MonoBehaviour {
	//Test GIT
	//Test 2
	//Masterweb34s
	public AudioClip[] audioClips;
	public AudioSource audioSource;
	public Text text;
	public GameObject dop;
	int currentId = 0;
	void Start(){
		audioSource.clip = audioClips [currentId];
		audioSource.Play ();
		text.text = audioSource.clip.name;
	}
	void Update () {
		if (Input.GetKeyUp (KeyCode.LeftBracket)) {
			Prev ();
		}
		if (Input.GetKeyUp (KeyCode.RightBracket)) {
			Next ();
		}
		if (Input.GetKeyUp (KeyCode.S)){
			dop.SetActive (!dop.activeSelf);
		}

	}
	void Play(){
		audioSource.clip = audioClips [currentId];
		audioSource.Play ();
		text.text = audioSource.clip.name;
	}
	void Next(){
		currentId++;
		if (currentId > audioClips.Length-1) {
			currentId = 0;
		}
		Play ();
	}
	void Prev(){
		currentId--;
		if (currentId < 0) {
			currentId = audioClips.Length - 1;
		}
		Play ();
	}
}
