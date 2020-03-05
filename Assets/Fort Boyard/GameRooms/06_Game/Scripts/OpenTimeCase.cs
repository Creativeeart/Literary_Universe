using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTimeCase : MonoBehaviour
{
    public bool caseUser = false;
    public bool caseComputer = false;
    private void OnMouseDown()
    {
        if (Game_06_Controller.Instance.isStopUserTime && Game_06_Controller.Instance.isStopComputerTime)
        {
            gameObject.GetComponent<Animator>().SetBool("isOpen", true);
            if (caseUser)
            {
                Game_06_Controller.Instance.isUserCaseOpen = true;
                Game_06_Controller.Instance.userLockIconCase.enabled = false;
            }
            if (caseComputer)
            {
                Game_06_Controller.Instance.isComputerCaseOpen = true;
                Game_06_Controller.Instance.computerLockIconCase.enabled = false;
            }
            if (Game_06_Controller.Instance.isComputerCaseOpen && Game_06_Controller.Instance.isUserCaseOpen)
            {
                StartCoroutine(CalculateWinner());
            }
        }
    }
    
    IEnumerator CalculateWinner()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(Game_06_Controller.Instance.CalculateNumbers());
    }
    void OnMouseOver()
    {
        gameObject.GetComponent<MeshRenderer>().material = Game_06_Controller.Instance.tumblerEmission;
    }
    void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = Game_06_Controller.Instance.tumblerDefault;
    }
}