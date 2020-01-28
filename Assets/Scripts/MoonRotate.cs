using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotate : MonoBehaviour {
	public GameObject moon;
	public float speed;
	void Update () {
		RotateArround ();
	}
	void RotateArround(){
		moon.transform.Rotate (Vector3.up * speed/2 * Time.deltaTime);
	}
}
