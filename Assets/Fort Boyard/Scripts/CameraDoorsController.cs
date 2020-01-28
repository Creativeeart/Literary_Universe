using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace cakeslice
{
    public class CameraDoorsController : MonoBehaviour
    {
        public GameObject alphabet;
        public GameObject gateZone;
        //public Animator DoorsCamera;
        public GameObject[] game_room;
        //public Camera doorCam;
        public TimerGame _timerGame;
        public FortBoyardGameController _fortBoyardGameController;

        public void StartGames(int numberRoom)
        {
            game_room[numberRoom].SetActive(true);
            //doorCam.enabled = false;
            _fortBoyardGameController.currentNumberRoom = numberRoom;
        }
        public void GoToCamToNextDoor(string nameAnimBoolString)
        {
            //DoorsCamera.SetBool(nameAnimBoolString, true);
        }

        public void GoToGateZone()
        {
            //DoorsCamera.SetBool("GoToGateZone", true);
            StartCoroutine(WaitingFunc(gateZone, true, 2f));
        }
        public void GoToAlphabet()
        {
            //DoorsCamera.SetBool("GoToAlphabet", true);
            _fortBoyardGameController.FB_CamMovingController.CameraMovingToPoint(_fortBoyardGameController.FB_CamMovingController.pointToAlphabetZoneA);
            StartCoroutine(Wait());
            StartCoroutine(WaitingFunc(alphabet, true, 2f));
        }
        IEnumerator WaitingFunc(GameObject currentGameObject, bool status, float timeWaiting)
        {
            yield return new WaitForSeconds(timeWaiting);
            currentGameObject.SetActive(status);
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
            _fortBoyardGameController.FB_CamMovingController.CameraMovingToPoint(_fortBoyardGameController.FB_CamMovingController.pointToAlphabetZoneB);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                //GoToGateZone();
            }
        }
    }
}
