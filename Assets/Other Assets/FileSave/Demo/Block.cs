using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	public int xSave; //Line number in the file save.
	public int ySave; //Line number in the file save.
	private Vector2 lastPos;
	// Use this for initialization
	void Start () {
		transform.position = new Vector2 (GlobalSave._GlobalSave.GetFloat (xSave), GlobalSave._GlobalSave.GetFloat (ySave));
		LastPosSave ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lastPos.x != transform.position.x || lastPos.y != transform.position.y){ //If the position has been changed
			GlobalSave._GlobalSave.SetFloat(xSave, transform.position.x); //Saving position X
			GlobalSave._GlobalSave.SetFloat(ySave, transform.position.y); //Saving position Y
			LastPosSave ();
		}
	}
	void LastPosSave (){
		lastPos.x = transform.position.x;
		lastPos.y = transform.position.y;
	}
}
