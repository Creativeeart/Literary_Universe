using System.Collections;
using UnityEngine;
using cakeslice;
namespace cakeslice
{
    public class DoorOpen : MonoBehaviour
    {
        
        public CameraDoorsController _cameraDoorsController;

        public GameObject textInfoToNextZone;
        public AudioClip openedDoor;
        public Animator doorAnimator;
        
        public bool isLocked = true;
        public int roomNumber = 0;

        private FortBoyardGameController _fortBoyardGameController;
        private Outline _outLine;
        private AudioSource _audioSource;

        void Start()
        {
            if (GameObject.Find("FortBoyardGameController"))
            {
                _fortBoyardGameController = GameObject.Find("FortBoyardGameController").GetComponent<FortBoyardGameController>();
            }
            else
            {
                Debug.Log("Fort Boyard Game Controller - не найден либо отключен!");
            }
            _outLine = gameObject.GetComponent<Outline>();
            gameObject.AddComponent<AudioSource>();
            _audioSource = gameObject.GetComponent<AudioSource>();
        }

        void OnMouseDown()
        {
            if (!isLocked)
            {
                _outLine.enabled = false;
                doorAnimator.enabled = true;
                StartCoroutine("DoorAnimationOpened");
                _audioSource.PlayOneShot(openedDoor);
                FortBoyardGameController.Instance.treasureZone = false;
                FortBoyardGameController.Instance.gameRooms = true;
                ////_fortBoyardGameController.AnimatorDoor = doorAnimator;
                FortBoyardGameController.AnimatorDoor = doorAnimator; //Static - обращение
                //FortBoyardGameController.Instance.AnimatorDoorConstructor = doorAnimator; // Принцип инкапсуляции
                FortBoyardGameController.TextInfoToNextZone = textInfoToNextZone;
                //textInfoToNextZone = textInfoToNextZone
                isLocked = true;
            }
        }
        IEnumerator DoorAnimationOpened()
        {
            yield return new WaitForSeconds(1f);
            _cameraDoorsController.StartGames(roomNumber);
            //for (int i = 0; i < disableObjects.Length; i++)
            //{
            //    disableObjects[i].SetActive(false);
            //}
        }
        void OnMouseEnter()
        {
            if (!isLocked) _outLine.enabled = true;
        }

        void OnMouseExit()
        {
            if (!isLocked) _outLine.enabled = false;
        }
    }
}
