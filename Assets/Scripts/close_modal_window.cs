using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_modal_window : MonoBehaviour {
	public void CloseModal(GameObject modal){
		modal.SetActive (!modal.activeSelf);
	}
}
