using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour {
    Animator test;
	// Use this for initialization
	void Start () {
		
	}
	public void Test()
    {
        test.Play("Back");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
