using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.Characters.FirstPerson;
namespace cakeslice
{
	public class SelectWorkers : MonoBehaviour
	{
		public GameObject dopCamera;
		public GameObject mainCamera;
		public GameObject popovaForm;
		public GameObject commanderCamerea;
		public GameObject popovaCamera;
		public GameObject commanderInterface;
		public GameObject fpsController;
		public Outline [] _outLine;
		public GameObject[] info;
		//private bool enterObject;
		public CheckActivateModalWindow chekActivateModalWindow;

		void Start(){
			chekActivateModalWindow = GameObject.Find ("Game Controller").GetComponent<CheckActivateModalWindow> ();
			for (int i = 0; i < _outLine.Length; i++) {
				_outLine [i].enabled = false;
			}
		}
		void Update(){
			
			dopCamera.transform.position = mainCamera.transform.position;
			dopCamera.transform.rotation = mainCamera.transform.rotation;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector3 forward = transform.TransformDirection(Vector3.forward) * 2f;
			Debug.DrawRay (transform.position, forward, Color.yellow);
			if (Physics.Raycast (ray, out hit, 2f)) {
				if (hit.transform.GetComponent<Rigidbody> ()) {
					Debug.Log (hit.transform.name);
					if (chekActivateModalWindow.isActivate == false) {
						//enterObject = true;
						DOTween.defaultTimeScaleIndependent = true;
						switch (hit.transform.name) {
						case "Front_aside_terminals_left":
							_outLine [0].enabled = true;
							info [0].SetActive (true);
							if (Input.GetKeyDown (KeyCode.E) || (Input.GetMouseButtonDown(0)))  {
								popovaCamera.SetActive (true);
								fpsController.SetActive (false);
								Cursor.lockState = CursorLockMode.None;
								Cursor.visible = true;
							}
							break;
						case "Front_aside_terminals_right":
							_outLine [1].enabled = true;
							info [2].SetActive (true);
							break;
						case "Main_Terminal_center":
							_outLine [2].enabled = true;
							_outLine [3].enabled = false;
							_outLine [4].enabled = false;
							info [5].SetActive (true);
							info [4].SetActive (false);
							info [6].SetActive (false);
							break;
						case "Main_Terminal_left":
							_outLine [3].enabled = true;
							_outLine [2].enabled = false;
							_outLine [4].enabled = false;
							info [4].SetActive (true);
							info [5].SetActive (false);
							info [6].SetActive (false);
							break;
						case "Main_Terminal_right":
							_outLine [4].enabled = true;
							_outLine [2].enabled = false;
							_outLine [3].enabled = false;
							info [6].SetActive (true);
							info [4].SetActive (false);
							info [5].SetActive (false);
							break;
						case "Black_aside_terminals_left_02":
							_outLine [5].enabled = true;
							info [3].SetActive (true);
							break;
						case "Black_aside_terminals_right_02":
							_outLine [6].enabled = true;
							info [7].SetActive (true);
							break;
						case "Chair_Commander":
							_outLine [7].enabled = true;
							info [1].SetActive (true);
							if (Input.GetKeyDown (KeyCode.E) || (Input.GetMouseButtonDown(0)))  {
								commanderCamerea.SetActive (true);
								commanderInterface.SetActive (true);
								fpsController.SetActive (false);
								Cursor.lockState = CursorLockMode.None;
								Cursor.visible = true;
							}
							break;
						default:
							for (int i = 0; i < _outLine.Length; i++) {
								_outLine [i].enabled = false;
							}
							break;
						}
					} 
				}
			}
			else {
				for (int i = 0; i < _outLine.Length; i++) {
					_outLine [i].enabled = false;
				}
				for (int i = 0; i < info.Length; i++) {
					info [i].SetActive(false);
				}
			}
			}
		public void PopovaFormClose(){
			popovaForm.SetActive (false);
			dopCamera.SetActive (false);
			fpsController.SetActive (true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		public void CloseInterface(){
			commanderCamerea.SetActive (false);
			// commanderInterface.SetActive (false);
			fpsController.SetActive (true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		public void CloseInterfacePopova(){
			popovaCamera.SetActive (false);
			// commanderInterface.SetActive (false);
			fpsController.SetActive (true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		void OnMouseEnter(){
//			if (chekActivateModalWindow.isActivate == false) {
//				enterObject = true;
//				_outLine.enabled = true;
//				DOTween.defaultTimeScaleIndependent = true;
//			}
		}
		void OnMouseExit(){
//			enterObject = false;
//			_outLine.enabled = false;
//			DOTween.defaultTimeScaleIndependent = true;
		}
	}
}
