using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class OpenTimeCase : MonoBehaviour
    {
        public bool caseUser = false;
        public bool caseComputer = false;
        public Game_06_Controller game_06_Controller;
        private void OnMouseDown()
        {
            if (game_06_Controller.isStopUserTime && game_06_Controller.isStopComputerTime)
            {
                gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                if (caseUser)
                {
                    game_06_Controller.isUserCaseOpen = true;
                    game_06_Controller.userLockIconCase.enabled = false;
                }
                if (caseComputer)
                {
                    game_06_Controller.isComputerCaseOpen = true;
                    game_06_Controller.computerLockIconCase.enabled = false;
                }
                if (game_06_Controller.isComputerCaseOpen && game_06_Controller.isUserCaseOpen)
                {
                    StartCoroutine(OpenCases());
                }
            }
        }
        IEnumerator OpenCases()
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(game_06_Controller.CalculateNumbers());
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