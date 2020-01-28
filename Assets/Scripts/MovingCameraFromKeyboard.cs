using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
public class MovingCameraFromKeyboard : MonoBehaviour {
	public GameObject[] menus;
	private OrbitCam _orbitCam;
	private int i = 0;

	void Start () {
		_orbitCam = GameObject.Find("Main Camera").GetComponent<OrbitCam>();
		menus = GameObject.FindGameObjectsWithTag ("Target"); 
		menus = menus.OrderBy(go => go.name).ToArray();
		_orbitCam.Target = menus [i].transform.GetComponent<Transform> ();
		_orbitCam.Distance = 4f;
	}
	

	void Update () {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			LeftMoving ();
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			RightMoving ();
		}
	}
	void LeftMoving(){
		i--;
		if (i < 0) {
			i = menus.Length - 1;
		}
		_orbitCam.Target = menus [i].transform.GetComponent<Transform> ();
	}

	void RightMoving(){
		i++;
		if (i > menus.Length-1) {
			i = 0;
		}
		_orbitCam.Target = menus [i].transform.GetComponent<Transform> ();
	}
}
