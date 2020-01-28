using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotateArroundMoon : MonoBehaviour {
	public GameObject axisMoon;
	public float speed;

	void Update () {
		RotateArround ();
	}
	void RotateArround(){
		transform.RotateAround (axisMoon.transform.position, - axisMoon.transform.up, speed * Time.deltaTime);
	}
}
