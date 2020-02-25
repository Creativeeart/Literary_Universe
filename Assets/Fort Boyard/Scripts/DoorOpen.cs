using System.Collections;
using UnityEngine;
using cakeslice;

public class DoorOpen : MonoBehaviour
{
    public int roomNumber = 0;

    public bool isOpened = false;
    public bool isLocked = true;
    Outline _outLine;
    AudioSource _audioSource;

    void Start()
    {
        _outLine = gameObject.GetComponent<Outline>();
        gameObject.AddComponent<AudioSource>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (!isOpened)
        {
            if (!isLocked)
            {
                _outLine.enabled = false;
                gameObject.transform.parent.gameObject.GetComponent<Animator>().enabled = true;
                StartCoroutine(DoorAnimationOpened());
                _audioSource.PlayOneShot(FortBoyardGameController.Instance.openedDoor);
                FortBoyardGameController.Instance.GameRooms = true;
                FortBoyardGameController.Instance.AnimatorDoor = gameObject.transform.parent.gameObject.GetComponent<Animator>(); //Передача переменной doorAnimator в скрипт FortBoyardGameController
                FortBoyardGameController.Instance.CurrentDoorOpen = gameObject.transform.parent.gameObject;
                isLocked = true;
                isOpened = true;
            }
        }
    }
    IEnumerator DoorAnimationOpened()
    {
        yield return new WaitForSeconds(1f);
        FortBoyardGameController.Instance.CurrentNumberRoom = roomNumber;
        FortBoyardGameController.Instance.game_rooms[roomNumber].SetActive(true);
        FortBoyardGameController.Instance.DisabledObjects();
        FB_CamMovingController.Instance.cameraToMovingFromScene.GetComponent<Camera>().enabled = false;
        FortBoyardGameController.Instance.mainUconsUI.SetActive(false);
    }
    void OnMouseEnter()
    {
        if (!isOpened)
        {
            if (!isLocked)
            {
                _outLine.enabled = true;
            }
        }
    }

    void OnMouseExit()
    {
        if (!isOpened)
        {
            if (!isLocked)
            {
                _outLine.enabled = false;
            }
        }
    }
}

