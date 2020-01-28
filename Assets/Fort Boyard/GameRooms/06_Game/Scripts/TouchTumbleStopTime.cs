using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class TouchTumbleStopTime : MonoBehaviour
    {
        public Game_06_Controller game_06_Controller;

        private void OnMouseDown()
        {
            game_06_Controller.StopUserTime();
        }
        private void OnMouseOver()
        {
            gameObject.GetComponent<MeshRenderer>().material = game_06_Controller.tumblerEmission;
        }
        private void OnMouseExit()
        {
            gameObject.GetComponent<MeshRenderer>().material = game_06_Controller.tumblerDefault;
        }
    }
}
