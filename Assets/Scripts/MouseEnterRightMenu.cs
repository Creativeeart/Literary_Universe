using UnityEngine;

public class MouseEnterRightMenu : MonoBehaviour {

	public void EnterMouse(GameObject linear){
		linear.SetActive (true);
	}
	public void ExitMouse(GameObject linear){
		linear.SetActive (false);
	}
}
