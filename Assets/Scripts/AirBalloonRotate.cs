using UnityEngine;

public class AirBalloonRotate : MonoBehaviour {
	public float speed;

	void Update () {
		this.transform.Rotate (Vector3.up * speed * Time.deltaTime); //Учи
	}
}
