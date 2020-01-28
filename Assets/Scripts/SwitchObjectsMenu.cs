using UnityEngine;

public class SwitchObjectsMenu : MonoBehaviour {
	public GameObject SwitchableObjects;
	private CheckActivateModalWindow chekActivateModalWindow;
	private Pause_game pauseGame;
	//private MovingCameraFromKeyboard _movingCameraFromKeyboard;

	void Start(){
		pauseGame = GameObject.Find ("Pause Game Controller").GetComponent<Pause_game>();
		chekActivateModalWindow = GameObject.Find ("Game Controller").GetComponent<CheckActivateModalWindow> ();
		//_movingCameraFromKeyboard = GameObject.Find ("Game Controller").GetComponent<MovingCameraFromKeyboard>();
	}
	public void OnMouseDown(){
		if (SwitchableObjects != null) {
			if (chekActivateModalWindow.isActivate == false){
					SwitchableObjects.SetActive (true);
					pauseGame.CheckPause ();
			}
			chekActivateModalWindow.isActivate = true;
		}
	}

}
