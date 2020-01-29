using System.Collections;
using UnityEngine;
using cakeslice;
namespace cakeslice
{
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
                    StartCoroutine("DoorAnimationOpened");
                    _audioSource.PlayOneShot(openedDoor);
                    FortBoyardGameController.Instance.IsTreasureZone = false;
                    FortBoyardGameController.Instance.GameRooms = true;
                    ////_fortBoyardGameController.AnimatorDoor = doorAnimator;
                    //FortBoyardGameController.AnimatorDoor = doorAnimator; //Static - обращение
                    //FortBoyardGameController.Instance.AnimatorDoorConstructor = doorAnimator; // Принцип инкапсуляции
                    //FortBoyardGameController.Instance.AnimatorDoorReturn(doorAnimator); // Принцип инкапсуляции
                    FortBoyardGameController.Instance.AnimatorDoor = doorAnimator; //Передача переменной doorAnimator в скрипт FortBoyardGameController
                    FortBoyardGameController.Instance.TextInfoToNextZone = textInfoToNextZone;
                    //textInfoToNextZone = textInfoToNextZone
                    isLocked = true;
                    isOpened = true;
                }
            }
        }
        IEnumerator DoorAnimationOpened()
        {
            yield return new WaitForSeconds(1f);
            CameraDoorsController.Instance.StartGames(roomNumber);
            FB_CamMovingController.Instance.cameraToMovingFromScene.GetComponent<Camera>().enabled = false;
            //for (int i = 0; i < disableObjects.Length; i++)
            //{
            //    disableObjects[i].SetActive(false);
            //}
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
}
