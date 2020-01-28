using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace cakeslice
{
    public class FortBoyardGameController : MonoBehaviour
    {
        public FB_CamMovingController FB_CamMovingController;
        public bool isLockFPS = true;
        public GameObject fpsGameObject;
        public AudioSource audioSourceDoors;
        public AudioClip closedDoor;
        public GameObject[] disableObjects;
        public CameraDoorsController _cameraDoorsController;
        public EndTime _endTime;
        //public bool camRotation = false;
        //public GameObject mainCamera;
        public GameObject fortBoyardGameObject;
        //public float speedRotation;
        //public Animator camAnimator;
        public GameObject mainMenu, gameRulers, mainUconsUI, watchUI, timeReducing;
        public Transform timeReducingParent;
        public GameObject[] game_rooms;
        public GameObject[] game_rules_in_rooms;
        public TextMeshProUGUI totalTimeText;
        //public Camera camDoor;
        public TimerGame _timerGame;
        public float totalTime;
        public int totalKeys = 5;
        public int totalTips;
        public int currentKeys;
        public int currentTips;
        public GameObject[] keys;
        public GameObject[] tips;

        public GateZoneController gateZoneController;
        //public GameObject gateCamWithUI;

        //public GameObject door_2, door_7;
        public GameObject pauseGameModal;
        public GameObject exitGameModal;
        public TextMeshProUGUI textExitModal;
        public int currentNumberRoom = 0;
        public bool isRoomPause = false;
        public GameObject failGameModalInRooms;
        public TextMeshProUGUI failGameText;
        public GameObject winGameModalInRooms;

        public static GameObject TextInfoToNextZone { set; get; }




        //FortBoyardGameController.AnimatorDoor = doorAnimator; //Способ - 1; * - обращение через другой скрипт

        public static Animator AnimatorDoor; // Статик переменная; Способ - 1



        //FortBoyardGameController.Instance.AnimatorDoorConstructor = doorAnimator; // Способ - 2; * - обращение через другой скрипт 

        private Animator _AnimatorDoor; // Приватная переменная - используется через AnimatorDoorConstructor; Способ - 2
        public Animator AnimatorDoorConstructor
        {
            get
            {
                return _AnimatorDoor;
            }
            set
            {
                _AnimatorDoor = value;
            }
        }


        //FortBoyardGameController.Instance.AnimatorDoorConstructorAuto = doorAnimator; // Спосбо - 3; * - обращение через другой скрипт

        public Animator AnimatorDoorConstructorAuto { get; set; } //АвтоКонструктор; Способ - 3












        public GameObject canvasTreasureZone;
        public TreasureCalculateZone _treasureCalculateZone;
        public GameObject failModal;
        public bool alphabetZone = false;
        public bool treasureZone = false;
        public bool gameRooms = false;

        public static FortBoyardGameController Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (isLockFPS)
            {
                Application.targetFrameRate = 65;
            }
            totalTips = gateZoneController.allTipsList.Count;
            _timerGame.seconds = totalTime;
            ReloadTimer();
        }
        public void ClosePauseModal()
        {
            pauseGameModal.SetActive(false);
            Time.timeScale = 1;
        }
        public void MainPauseWithEsc()
        {
            pauseGameModal.SetActive(true);
            Time.timeScale = 0;
        }
        private void Update()
        {
            EndTimes();
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                fpsGameObject.SetActive(!fpsGameObject.activeSelf);
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainPauseWithEsc();
            }
            if (!gateZoneController.isGateZone)
            {
                for (int i = 0; i < currentKeys; i++) keys[i].SetActive(true);
                for (int i = 0; i < currentTips; i++)
                {
                    if (i >= 3) break;
                    tips[i].SetActive(true);
                }
            }
            //if (camRotation)
            //{
            //    camAnimator.applyRootMotion = true;
            //    mainCamera.transform.RotateAround(fortBoyardGameObject.transform.position, -fortBoyardGameObject.transform.up, speedRotation * Time.deltaTime);
            //}
            //else
            //{
            //    camAnimator.applyRootMotion = false;
            //}
            if (treasureZone)
            {
                if (_timerGame.seconds <= 10.0f)
                {
                    gateZoneController.isOpenGate = false;
                    gateZoneController.gateAnimation.SetBool("Closed", true);
                    //gateCamWithUI.SetActive(true);
                }
                canvasTreasureZone.SetActive(true);
            }
        }

        void EndTimes()
        {
            if (_timerGame.seconds <= _timerGame.endTime)
            {
                _timerGame.RunTime = false;
                if (treasureZone)
                {
                    _treasureCalculateZone.check = true;
                }
                if (gameRooms)
                {
                    LoseRoom();
                }
                if (alphabetZone)
                {
                    failModal.SetActive(true);
                }
            }
        }

        public void ExitModalShow(int numberRoom)
        {
            currentNumberRoom = numberRoom;
            if (numberRoom == 0 || numberRoom == 1 || numberRoom == 2)
            {
                textExitModal.text = "Выйти из комнаты?\n<size=30>При этом вы не получите <i><color=#FF8400FF>ключ</i></color>.</size>";
            }
            if (numberRoom == 3 || numberRoom == 4 || numberRoom == 5)
            {
                textExitModal.text = "Выйти из комнаты?\n<size=30>При этом вы не получите <i><color=#FF8400FF>подсказку</i></color>.</size>";
            }
            if (numberRoom == 0 || numberRoom == 1 || numberRoom == 2 || numberRoom == 3)
            {
                _timerGame.RunTime = false;
            }
            exitGameModal.SetActive(true);
            Time.timeScale = 0;
            isRoomPause = true;
        }
        public void ExitModalHide()
        {
            Time.timeScale = 1;
            exitGameModal.SetActive(false);
            _timerGame.RunTime = true;
            isRoomPause = false;
        }
        public void Close_Game_Room()
        {
            game_rooms[currentNumberRoom].SetActive(false);
            //camDoor.enabled = true;
            _timerGame.RunTime = false;
            _timerGame.seconds = totalTime;
            mainUconsUI.SetActive(true);
            watchUI.SetActive(true);
            AnimatorDoor.SetBool("doorIsClosed", true);
            audioSourceDoors.PlayOneShot(closedDoor);
            TextInfoToNextZone.SetActive(true);
            StartCoroutine(ReverseTime(4, currentNumberRoom));
            ReloadTimer();
            for (int i = 0; i < disableObjects.Length; i++)
            {
                if (disableObjects[i] == null) continue;
                disableObjects[i].SetActive(true);
            }
            Time.timeScale = 1;
            exitGameModal.SetActive(false);
            isRoomPause = false;
            failGameModalInRooms.SetActive(false);
        }

        private IEnumerator ReverseTime(float time, int currentRoom)
        {
            while (time > 0)
            {
                time--;
                TextInfoToNextZone.GetComponent<TextMeshProUGUI>().text = string.Format("СЛЕДУЮЩЕЕ ИСПЫТАНИЕ ЧЕРЕЗ ({0})", time.ToString());
                if (time <= 0)
                {
                    int numberRoom = currentRoom;
                    switch (numberRoom)
                    {
                        case 0:
                            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToDoor2);
                            break;
                        case 1:
                            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToDoor3);
                            break;
                        case 2:
                            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToDoor4);
                            break;
                        case 3:
                            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToDoor5);
                            break;
                        case 4:
                            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToDoor6);
                            break;
                        case 5:
                            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToGateZoneA);
                            yield return new WaitForSeconds(2);
                            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToGateZoneB);
                            _cameraDoorsController.GoToGateZone();
                            break;
                    }
                    //else _cameraDoorsController.GoToCamToNextDoor("GoToDoorNumber_0" + numberRoom);
                    yield return new WaitForSeconds(1);
                    TextInfoToNextZone.SetActive(false);
                }
                yield return new WaitForSeconds(1);
            }
        }

        private void ShowMainMenu()
        {
            mainMenu.SetActive(true);
        }

        public void StartGame()
        {
            //camRotation = false;
            //camAnimator.SetBool("StartGame", true);
            mainMenu.SetActive(false);
            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToBriefing);
            StartCoroutine(ShowRules());
        }

        private IEnumerator ShowRules()
        {
            yield return new WaitForSeconds(FB_CamMovingController.speedDurationMovingCamera);
            ShowGameRulers();
        }

        private void ShowGameRulers()
        {
            gameRulers.SetActive(true);
        }
        public void GoalGamesClose()
        {
            gameRulers.SetActive(false);
            //mainCam.SetActive(false);
            //FB_CamMovingController.cameraToMovingFromScene.gameObject.SetActive(true);
            FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToDoor1);
            //doorsCam.SetActive(true);
            mainUconsUI.SetActive(true);
            watchUI.SetActive(true);
        }

        public void Close_Game_Rule(int numberRule)
        {
            game_rules_in_rooms[numberRule].SetActive(false);
            RunTimer();
            mainUconsUI.SetActive(false);
        }
        public void WinnerRoom(string typeRoom)
        {
            if (typeRoom == "Keys")
            {
                currentKeys = currentKeys + 1;
            }
            else if (typeRoom == "Tips") currentTips = currentTips + 1;
            totalTime = totalTime + 10;
            winGameModalInRooms.SetActive(true);
        }
        public void CloseRoomAfterWinner()
        {
            Close_Game_Room();
            for (int i = 0; i < disableObjects.Length; i++)
            {
                disableObjects[i].SetActive(true);
            }
            winGameModalInRooms.SetActive(false);
        }
        
        public void LoseRoom()
        {
            failGameModalInRooms.SetActive(true);
            failGameText.text = "Время вышло!\nК сожалению вы не справились с испытанием";
            _timerGame.RunTime = false;
            isRoomPause = true;
        }
        public void LoseRoom(string text)
        {
            failGameModalInRooms.SetActive(true);
            failGameText.text = text;
            _timerGame.RunTime = false;
            isRoomPause = true;
        }
        public void RunTimer()
        {
            _timerGame.RunTime = true;
            _timerGame.seconds = totalTime;
        }
        public void StopTimer()
        {
            _timerGame.RunTime = false;
            _timerGame.seconds = totalTime;
        }
        public void ReloadTimer()
        {
            //_timerGame.seconds = totalTime;
            totalTimeText.text = string.Format("{0:00}:{1:00}", (int)totalTime / 60, (int)totalTime % 60);
        }
    }
}