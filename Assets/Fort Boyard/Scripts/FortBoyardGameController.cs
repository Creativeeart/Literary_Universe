using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace cakeslice
{
    public class FortBoyardGameController : MonoBehaviour
    {
        [Header("Settings")]
        public bool isLockFPS = true;
        public float totalTime = 120; //Сколько времени требуется для испытаний. По умолчанию: 120 / 60 = 2 мин.

        [Header("Audio")]
        public AudioSource audioSourceDoors;
        public AudioClip closedDoor;

        [Header("Main Camera")]
        public GameObject mainCamera;

        [Header("UI Elements")]
        public GameObject canvasTreasureZone;
        public GameObject fpsGameObject;
        public GameObject mainMenu;
        public GameObject gameRulers;
        public GameObject mainUconsUI;
        public GameObject watchUI;
        public Transform timeReducingParent;
        public GameObject timeReducing;
        public GameObject pauseGameModal;
        public GameObject exitGameModal;
        public GameObject failModal;
        public GameObject failGameModalInRooms;
        public TextMeshProUGUI failGameText;
        public GameObject winGameModalInRooms;
        public TextMeshProUGUI textExitModal;
        public TextMeshProUGUI totalTimeText;

        [Header("Objects in scene")]
        public GameObject fortBoyardGameObject;
        public GameObject[] game_rooms;
        public GameObject[] game_rules_in_rooms;

        [Header("Other")]
        public GameObject[] disableObjects;

        [Header("Keys & Tips")]
        public int totalKeys = 3; // Кол-во ключей.
        public int totalTips = 3; // Кол-во подсказок.
        public int CurrentKeys { get; set; }
        public int CurrentTips { get; set; }
        public GameObject[] keys;
        public GameObject[] tips;

        
        public Animator AnimatorDoor { get; set; }
        public GameObject TextInfoToNextZone { get; set; }
        public bool IsRoomPause { get; set; }

        public bool IsGateZone { get; set; }
        public bool IsAlphabetZone { get; set; }
        public bool IsTreasureZone { get; set; }
        public bool IsTreasureCalculateZone { get; set; }

        public bool GameRooms { get; set; }
        public int CurrentNumberRoom { get; set; }

        public static FortBoyardGameController Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            if (isLockFPS)
            {
                Application.targetFrameRate = 65;
            }
            totalTips = GateZoneController.Instance.allTipsList.Count;
            TimerGame.Instance.seconds = totalTime;
            ReloadTimer();
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
            if (!IsGateZone)
            {
                for (int i = 0; i < CurrentKeys; i++) keys[i].SetActive(true);
                for (int i = 0; i < CurrentTips; i++)
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
            if (IsTreasureZone)
            {
                if (TimerGame.Instance.seconds <= 10.0f)
                {
                    GateZoneController.Instance.isOpenGate = false;
                    GateZoneController.Instance.gateAnimation.SetBool("Closed", true);
                    //gateCamWithUI.SetActive(true);
                }
                canvasTreasureZone.SetActive(true);
            }
        }
        void EndTimes()
        {
            if (TimerGame.Instance.seconds <= TimerGame.Instance.endTime)
            {
                TimerGame.Instance.RunTime = false;
                if (IsTreasureZone)
                {
                    failModal.SetActive(true);
                    IsTreasureZone = false;
                }
                if (GameRooms)
                {
                    LoseRoom();
                }
                if (IsAlphabetZone)
                {
                    failModal.SetActive(true);
                    IsAlphabetZone = false;
                }
                if (IsGateZone)
                {
                    failModal.SetActive(true);
                    IsGateZone = false;
                }
            }
        }
        public IEnumerator GoToGateZone()
        {
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToGateZoneA);
            yield return new WaitForSeconds(FB_CamMovingController.Instance.speedDurationMovingCamera);
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToGateZoneB);
            GateZoneController.Instance.GateZoneEntered();
        }

        public IEnumerator GoToAlphabetZone()
        {
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToAlphabetZoneA);
            yield return new WaitForSeconds(FB_CamMovingController.Instance.speedDurationMovingCamera);
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToAlphabetZoneB);
            AlphabetZoneController.Instance.AlphabetZoneEntered();
        }

        public IEnumerator GoToTreasureZone()
        {
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToTreasure_Zone);
            yield return new WaitForSeconds(FB_CamMovingController.Instance.speedDurationMovingCamera);
            TreasureZoneController.Instance.TreasureZoneEntered();
        }

        public IEnumerator GoToTreasureCalculateZone()
        {
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToTreasure_Calculate_Zone_A);
            yield return new WaitForSeconds(FB_CamMovingController.Instance.speedDurationMovingCamera + TreasureCalculateZoneController.Instance.time);
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToTreasure_Calculate_Zone_B);
            TreasureCalculateZoneController.Instance.TreasureCalculateZoneEntered();
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


        public void ExitModalShow(int numberRoom)
        {
            CurrentNumberRoom = numberRoom;
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
                TimerGame.Instance.RunTime = false;
            }
            exitGameModal.SetActive(true);
            Time.timeScale = 0;
            IsRoomPause = true;
        }
        public void ExitModalHide()
        {
            Time.timeScale = 1;
            exitGameModal.SetActive(false);
            TimerGame.Instance.RunTime = true;
            IsRoomPause = false;
        }
        public void Close_Game_Room()
        {
            game_rooms[CurrentNumberRoom].SetActive(false);
            //camDoor.enabled = true;
            TimerGame.Instance.RunTime = false;
            TimerGame.Instance.seconds = totalTime;
            mainUconsUI.SetActive(true);
            watchUI.SetActive(true);
            AnimatorDoor.SetBool("doorIsClosed", true);
            audioSourceDoors.PlayOneShot(closedDoor);
            TextInfoToNextZone.SetActive(true);
            StartCoroutine(ReverseTime(4, CurrentNumberRoom));
            ReloadTimer();
            for (int i = 0; i < disableObjects.Length; i++)
            {
                if (disableObjects[i] == null) continue;
                disableObjects[i].SetActive(true);
            }
            Time.timeScale = 1;
            exitGameModal.SetActive(false);
            IsRoomPause = false;
            failGameModalInRooms.SetActive(false);
            FB_CamMovingController.Instance.cameraToMovingFromScene.GetComponent<Camera>().enabled = true;
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
                            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToDoor2);
                            break;
                        case 1:
                            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToDoor3);
                            break;
                        case 2:
                            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToDoor4);
                            break;
                        case 3:
                            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToDoor5);
                            break;
                        case 4:
                            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToDoor6);
                            break;
                        case 5:
                            Instance.IsGateZone = true;
                            Instance.IsAlphabetZone = false;
                            Instance.IsTreasureZone = false;
                            Instance.IsTreasureCalculateZone = false;
                            StartCoroutine(GoToGateZone()); // Переход к зоне с воротами
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
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToBriefing);
            StartCoroutine(ShowRules());
        }

        private IEnumerator ShowRules()
        {
            yield return new WaitForSeconds(FB_CamMovingController.Instance.speedDurationMovingCamera);
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
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToDoor1);
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
                CurrentKeys = CurrentKeys + 1;
            }
            else if (typeRoom == "Tips") CurrentTips = CurrentTips + 1;
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
            TimerGame.Instance.RunTime = false;
            IsRoomPause = true;
        }
        public void LoseRoom(string text)
        {
            failGameModalInRooms.SetActive(true);
            failGameText.text = text;
            TimerGame.Instance.RunTime = false;
            IsRoomPause = true;
        }
        public void RunTimer()
        {
            TimerGame.Instance.RunTime = true;
            TimerGame.Instance.seconds = totalTime;
        }
        public void StopTimer()
        {
            TimerGame.Instance.RunTime = false;
            TimerGame.Instance.seconds = totalTime;
        }
        public void ReloadTimer()
        {
            //_timerGame.seconds = totalTime;
            totalTimeText.text = string.Format("{0:00}:{1:00}", (int)totalTime / 60, (int)totalTime % 60);
        }
    }
}