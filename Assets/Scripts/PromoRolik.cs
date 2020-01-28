using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PromoRolik : MonoBehaviour {
    public GameObject[] interfaces;
    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < interfaces.Length; i++)
        {
            //interfaces[i].SetActive(false);
        }
    }
    void Start () {
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.I)) {
            for (int i = 0; i < interfaces.Length; i++) {
                interfaces[i].SetActive(!interfaces[i].activeSelf);
            }
            
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Cursor.visible = false;
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            Cursor.visible = true;
        }

    }
}
