using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjects : MonoBehaviour {
    public GameObject [] gameObjects;
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
                for (int i = 0; i < gameObjects.Length; i++)
                    gameObjects[i].SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
                for (int i = 0; i < gameObjects.Length; i++)
                    gameObjects[i].SetActive(false);
            }
        }
    }
}
