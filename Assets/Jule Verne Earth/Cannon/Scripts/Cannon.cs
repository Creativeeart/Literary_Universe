using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
	public GameObject cannonBall;
	Rigidbody cannonBallRB;
	public Transform shotPos;
	public GameObject explosion;
	public Light lightExplosion;
	public float firePower;
	public float timerFire = 5f;
	public float timerFireLight = 0.2f;
	private bool checkFire;
	public GameObject target;
	public GameObject oldTarget;
	void Update () {
		
		timerFire -= Time.deltaTime;
		if (timerFire <= 0){
			FindObjectOfType<AudioManager> ().Play ("CannonFire");
			FireCannon ();
			timerFire = 5f;
		}
		if (checkFire == true) {
			timerFireLight -= Time.deltaTime;
			if (timerFireLight <= 0){
				lightExplosion.intensity = 0f;
				checkFire = false;
				timerFireLight = 0.2f;
			}
		}
		
	}
	public void FireCannon(){
		checkFire = true;
		GameObject cannonBallCopy = Instantiate (cannonBall, shotPos.position, transform.rotation) as GameObject;
        cannonBallRB = cannonBallCopy.GetComponent<Rigidbody>();
		cannonBallRB.AddForce (transform.forward * firePower);

		/////////////////////
		target.transform.parent = cannonBallCopy.transform;
		target.transform.localPosition = new Vector3 (0f, 0f, 0f);
//		if (target.transform.parent == cannonBallCopy.transform) {
			
//		}
		/////////////////////

		GameObject expl = Instantiate (explosion, shotPos.position, shotPos.rotation);
		lightExplosion.intensity = 4f;
		Destroy (expl, 2f);
	}
}
