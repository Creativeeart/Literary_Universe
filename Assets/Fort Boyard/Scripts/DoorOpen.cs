using System.Collections;
using UnityEngine;
using cakeslice;

public class DoorOpen : MonoBehaviour
{
    public GameObject textInfoToNextZone;
    public AudioClip openedDoor;
    public Animator doorAnimator;
    public bool isOpened = false;
    public bool isLocked = true;
    public int roomNumber = 0;

    private Outline _outLine;
    private AudioSource _audioSource;

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
                doorAnimator.enabled = true;
                StartCoroutine(DoorAnimationOpened());
                _audioSource.PlayOneShot(openedDoor);
                FortBoyardGameController.Instance.GameRooms = true;
                FortBoyardGameController.Instance.AnimatorDoor = doorAnimator; //Передача переменной doorAnimator в скрипт FortBoyardGameController
                FortBoyardGameController.Instance.TextInfoToNextZone = textInfoToNextZone;
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

