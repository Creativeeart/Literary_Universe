using UnityEngine;

public class CloseModalWindowGame : MonoBehaviour {

	public void CloseModal(GameObject modal){
		modal.SetActive (!modal.activeSelf);
	}
}
