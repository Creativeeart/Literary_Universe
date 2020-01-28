using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUsers : MonoBehaviour {
    public GameObject[] gameObjectsForEnabled;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            
        }
    }
    public void HideInfoUsers()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < gameObjectsForEnabled.Length; i++)
        {
            gameObjectsForEnabled[i].SetActive(true);
        }
    }
}
