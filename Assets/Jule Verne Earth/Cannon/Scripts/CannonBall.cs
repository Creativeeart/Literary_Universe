using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
	public float lifeTime = 2f;
	public GameObject explosionBall;
	private Cannon _cannon;

	void Start () {
		_cannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<Cannon>();
	}

	void Update () {
		StatusCheck ();

	}
	void StatusCheck(){
		lifeTime -= Time.deltaTime;
		if (lifeTime < 0) {
			Destroy ();
			FindObjectOfType<AudioManager> ().Play ("ExplosiveBall");
		}

	}
	void Destroy(){
		GameObject expl = Instantiate (explosionBall, transform.position, transform.rotation);
		/////////////////////
		_cannon.target.transform.parent = _cannon.oldTarget.transform;
		/////////////////////
		Destroy (this.gameObject);
		Destroy (expl, 3.5f);
	}
}
