using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testttt : MonoBehaviour {
	public GameObject player;
	public Text text;
	public string fps;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		text.text = player.transform.position + "\n" + fps; 
	}
}
