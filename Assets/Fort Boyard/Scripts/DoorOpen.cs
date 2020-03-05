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

    FortBoyardGameController FortBoyardGameController;
    FB_CamMovingController FB_CamMovingController;
    void Start()
    {
        FortBoyardGameController = FortBoyardGameController.Instance;
        FB_CamMovingController = FB_CamMovingController.Instance;

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
                _audioSource.PlayOneShot(FortBoyardGameController.openedDoor);
                FortBoyardGameController.GameRooms = true;
                FortBoyardGameController.AnimatorDoor = gameObject.transform.parent.gameObject.GetComponent<Animator>(); //Передача переменной doorAnimator в скрипт FortBoyardGameController
                FortBoyardGameController.CurrentDoorOpen = gameObject.transform.parent.gameObject;
                isLocked = true;
                isOpened = true;
            }
        }
    }
    IEnumerator DoorAnimationOpened()
    {
        yield return new WaitForSeconds(1f);
        FortBoyardGameController.CurrentNumberRoom = roomNumber;
        FortBoyardGameController.game_rooms[roomNumber].SetActive(true);
        FortBoyardGameController.DisabledObjects();
        FB_CamMovingController.cameraToMovingFromScene.GetComponent<Camera>().enabled = false;
        FortBoyardGameController.mainUconsUI.SetActive(false);
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

