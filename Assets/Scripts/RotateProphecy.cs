using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateProphecy : MonoBehaviour {
    public float speed = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, Time.deltaTime * speed);
    }
}
