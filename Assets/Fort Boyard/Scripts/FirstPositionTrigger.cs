using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class FirstPositionTrigger : MonoBehaviour
    {
        public FortBoyardGameController fortBoyardGameController;
        public GameObject mainMenu;
        public bool firstTouch = false;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("CamTouchTrigger");
            ShowMainMenu();
            firstTouch = true;
        }
        void ShowMainMenu()
        {
            if (firstTouch == false)
            {
                //fortBoyardGameController.camRotation = true;
                mainMenu.SetActive(true);
            }

        }
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("CamTouchCollision");
        }
    }
}
