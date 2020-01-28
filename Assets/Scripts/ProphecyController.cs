using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProphecyController : MonoBehaviour {
	public GameObject modalWindow;
	// Use this for initialization

	void OnMouseDown()
	{
		modalWindow.SetActive (true);
	}
}
