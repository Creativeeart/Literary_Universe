using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {
	public int xName; //Line number in the file save.
	public int yTime; //Line number in the file save.
	public Text _textName;
	public Text _textTime;
	public InputField _input;
//	public Text lastText;
	// Use this for initialization
	void Start () {
		_textName.text = GlobalSave._GlobalSave.GetString (xName);
		_textTime.text = GlobalSave._GlobalSave.GetString (yTime);

	}

	// Update is called once per frame
	void Update () {
//		if (lastText.text != _text.text){ //If the position has been changed
		GlobalSave._GlobalSave.SetString(yTime,_textTime.text);

//			LastPosSave ();
//		}
	}
	void LastPosSave (){
//		lastText.text = _text.text;
	}
	public void UpdatedText(){
		GlobalSave._GlobalSave.SetString(xName, _input.text);
//		_text.text = GlobalSave._GlobalSave.GetString (xSave);
	}
}
