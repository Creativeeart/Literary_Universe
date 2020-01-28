using UnityEngine; 
using DG.Tweening;

public class Book_animation : MonoBehaviour {
	public GameObject ParentModal;
	public GameObject modal;
	public bool checkedButton = false;
	//private Pause_game pauseGame;
	//private CheckActivateModalWindow chekActivateModalWindow;

	void Start(){
		//chekActivateModalWindow = GameObject.Find ("Game Controller").GetComponent<CheckActivateModalWindow> ();
		//pauseGame = GameObject.Find ("Pause Game Controller").GetComponent<Pause_game> ();
	}
//	public void OnMouseDown(){
//		DOTween.defaultTimeScaleIndependent = true;
//		checkedButton = !checkedButton;
////		pauseGame.CheckPause ();
//		if (checkedButton == true) {
//			modal.GetComponent<RectTransform>().DOScale(new Vector3(1,1,1), 1.0f);
//		} 
////		else if(checkedButton == false) {
////			modal.GetComponent<RectTransform>().DOScale(new Vector3(0,0,0), 1.0f);
////		} 
//	}
//	public void CloseModal(){
//		checkedButton = !checkedButton;
//		if(checkedButton == false) {
//			modal.GetComponent<RectTransform>().DOScale(new Vector3(0,0,0), 1.0f);
//
//		} 
//	}
//	void Update(){
//		if (checkedButton == false) {
//			if (modal.transform.localScale == new Vector3 (0, 0, 0)) {
//				Debug.Log ("Complete");
//				ParentModal.SetActive (false);
//				chekActivateModalWindow.isActivate = false;
//				pauseGame.CheckPause ();
//			}
//		}
//	}
}
