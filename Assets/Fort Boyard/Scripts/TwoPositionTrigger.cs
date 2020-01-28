using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPositionTrigger : MonoBehaviour {
    public GameObject gameRulesUI;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "MainCamera")
            ShowMainMenu();
    }
    void ShowMainMenu()
    {
        gameRulesUI.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CamTouchCollision");
    }
}
