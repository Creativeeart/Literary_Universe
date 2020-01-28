using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRotate : MonoBehaviour {

	public float rotSpeed;
	public float angDrag = 3.0f;
	float rotX, rotY;
	float random;
	Rigidbody rb;
	void Start(){
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.angularDrag = angDrag;
	}
	void Update () {
		random = Random.Range (0.0f, 360.0f);
		rotX = random * rotSpeed * Time.fixedDeltaTime;
		rotY = random * rotSpeed * Time.fixedDeltaTime;
		rb.AddTorque (Vector3.up * -rotX);
		rb.AddTorque (Vector3.right * rotY);
	}
}
