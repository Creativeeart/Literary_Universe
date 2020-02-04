using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesese : MonoBehaviour {
    public Transform parent;
	// Use this for initialization
	void Start () {
        parent = GameObject.Find("Cube").GetComponent<Transform>();
        gameObject.transform.SetParent(parent);
        gameObject.transform.position = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
