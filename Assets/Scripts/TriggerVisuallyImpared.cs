using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVisuallyImpared : MonoBehaviour {
    public bool increaseLevelOne;
    public bool increaseLevelTwo;
    // Use this for initialization
    
    public void IncreaseLevelDefault()
    {
        increaseLevelOne = false;
        increaseLevelTwo = false;
    }
    public void IncreaseLevelOne()
    {
        increaseLevelOne = true;
        increaseLevelTwo = false;
    }
    public void IncreaseLevelTwo()
    {
        increaseLevelOne = false;
        increaseLevelTwo = true;
    }
}
