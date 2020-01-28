using UnityEngine;

public class AllCloseForm : MonoBehaviour {
	public GameObject[] forms;

	public void CloseForms(){
		for (int i = 0; i < forms.Length; i++) {
			forms [i].SetActive (false);
		}
	}
}
