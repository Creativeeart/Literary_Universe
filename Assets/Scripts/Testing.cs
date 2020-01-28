using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Testing : MonoBehaviour {
	public bool mouseOverGUI = false; 
	
	void Update () {
		if (EventSystem.current!=null) {
			mouseOverGUI = EventSystem.current.IsPointerOverGameObject();
			if (mouseOverGUI) {
				//Debug.Log ();
				//Debug.Log (EventTrigger.FindObjectOfType<Button> ().name);
			}
		}
	}
	void OnMouseEnter()
	{
		
	}
	void OnMouseExit()
	{
		Debug.Log ("Exit");
	}
}
