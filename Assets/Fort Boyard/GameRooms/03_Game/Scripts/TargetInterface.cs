using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInterface : MonoBehaviour {
    public float depth = 10f;
    public Camera mainCam;
    private Vector3 mousePos;
    public Vector3 aim;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = Input.mousePosition;
        mousePos += mainCam.transform.forward * depth;
        aim = mainCam.ScreenToWorldPoint(mousePos);
        transform.LookAt(aim);
    }
}
