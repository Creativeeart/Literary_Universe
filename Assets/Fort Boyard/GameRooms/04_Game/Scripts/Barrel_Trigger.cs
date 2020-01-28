using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Trigger : MonoBehaviour {
    public bool triggerEnter = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Barrel")
        {
            triggerEnter = true;
        }
            
    }
}
