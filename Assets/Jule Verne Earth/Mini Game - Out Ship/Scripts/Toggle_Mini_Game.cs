using UnityEngine;
namespace cakeslice
{
	public class Toggle_Mini_Game : MonoBehaviour {

		public Outline _outLine;
		public bool selectObject = false;
		
		void Update(){
			if (Input.GetKeyDown (KeyCode.LeftAlt)) {
				transform.GetChild (0).gameObject.SetActive (true);
			}
			if (Input.GetKeyUp (KeyCode.LeftAlt)) {
				transform.GetChild (0).gameObject.SetActive (false);
			}
		}
		
		void Start(){
			_outLine = gameObject.GetComponent<Outline>();
			_outLine.enabled = false;
			selectObject = false;
		}
		void OnMouseEnter(){
			if (selectObject == false){
				_outLine.enabled = true;
			}
			transform.GetChild (0).gameObject.SetActive (true);
		}
		void OnMouseExit(){
			if (selectObject == false) {
				_outLine.enabled = false;
			}
			transform.GetChild (0).gameObject.SetActive (false);
		}
		void OnMouseDown(){
			_outLine.enabled = true;
			selectObject = !selectObject;
		}	
	}
}
