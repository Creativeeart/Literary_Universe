using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTumbleStopTime : MonoBehaviour
{
    Game_06_Controller Game_06_Controller;

    void Start()
    {
        Game_06_Controller = Game_06_Controller.Instance;
    }

    private void OnMouseDown()
    {
        Game_06_Controller.StopUserTime();
    }
    private void OnMouseOver()
    {
        gameObject.GetComponent<MeshRenderer>().material = Game_06_Controller.tumblerEmission;
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = Game_06_Controller.tumblerDefault;
    }
}

