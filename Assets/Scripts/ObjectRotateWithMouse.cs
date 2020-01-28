using UnityEngine;

public class ObjectRotateWithMouse : MonoBehaviour {
	public float rotSpeed;
	public float angDrag = 3.0f;
	public bool autoRotate = false;
	float rotX, rotY;
	Rigidbody rb;
	void Start(){
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.angularDrag = angDrag;
	}
	void FixedUpdate () {
		if (Input.GetMouseButton (1)) {
			rotX = Input.GetAxis ("Mouse X") * rotSpeed * Time.fixedDeltaTime;
			rotY = Input.GetAxis ("Mouse Y") * rotSpeed * Time.fixedDeltaTime;
			rb.AddTorque (Vector3.up * -rotX);
			rb.AddTorque (Vector3.right * rotY);
		}
		if (autoRotate == true) {
			rotX = Mathf.Deg2Rad * rotSpeed * Time.fixedDeltaTime;
			rb.AddTorque(Vector3.up * rotX);
		}
	}

}
