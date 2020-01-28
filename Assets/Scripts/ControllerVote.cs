using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerVote : MonoBehaviour {
	public GameObject oprosForm;
	public GameObject congratulationText;
	public GameObject failForm;
	public GameObject descriptionBook;
	public GameObject cover;
	public GameObject [] redLines;
	public Button [] buttons;
	public void StartVote(){
		oprosForm.SetActive (true);
		descriptionBook.SetActive (false);
	}
	public void CloseVote(){
		oprosForm.SetActive (false);
		congratulationText.SetActive (false);
		descriptionBook.SetActive (true);
		failForm.SetActive (false);
		for (int i = 0; i < redLines.Length; i++){
			redLines [i].SetActive (false);
		}
		for (int i = 0; i < buttons.Length; i++){
			buttons [i].interactable = true;
		}
	}

	public void CorrectAnswer(){
//		oprosForm.SetActive (false);
//		congratulationText.SetActive (true);
//		for (int i = 0; i < redLines.Length; i++){
//			redLines [i].SetActive (false);
//		}
//		for (int i = 0; i < buttons.Length; i++){
//			buttons [i].interactable = true;
//		}
	}

	public void WrongAnswer(GameObject buttonWrong){
//		buttonWrong.transform.GetComponent<Button> ().interactable= false;
//		buttonWrong.transform.GetChild (1).gameObject.SetActive (true);
	}
}
