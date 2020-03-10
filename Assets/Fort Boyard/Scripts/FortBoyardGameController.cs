using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FortBoyardGameController : MonoBehaviour
{
    [Header("Settings")]
    public bool isLockFPS = true;
    public float totalTime = 120; //Сколько времени требуется для испытаний. По умолчанию: 120 / 60 = 2 мин.
    public int TimeToNextDoor = 4; //Сколько времени требуется для перехода к следующей двери с испытанием (сек)

    [Header("Audio")]
    public AudioSource audioSourceDoors;
    public AudioClip closedDoor;
    public AudioClip openedDoor;

    [Header("Main Camera")]
    public GameObject mainCamera;

    [Header("UI Elements")]
    public GameObject inputGetKeyDownToContinue;
    public GameObject canvasTreasureZone;
    public GameObject fpsGameObject;
    public GameObject mainMenu;
    public GameObject gameRulers;
    public GameObject mainUconsUI;
    public GameObject watchUI;
    public Transform timeReducingParent;
    public GameObject timeReducing;
    public TextMeshProUGUI totalTimeText;

    public GameObject TextInfoToNextZone_Parent;
    public GameObject TextInfoToNextZone_Text;

    [Header("Objects in scene")]
    public GameObject fortBoyardGameObject;
    public GameObject[] game_rooms;
    public GameObject[] game_rules_in_rooms;

    [Header("Other")]
    public GameObject[] disableObjects;

    [Header("Keys & Tips")]
    public int totalKeys = 3; // Кол-во ключей.
    public int totalTips = 5; // Кол-во подсказок.
    public int CurrentKeys { get; set; }
    public int CurrentTips { get; set; }
    public Sprite ActiveKey;
    public Sprite ActiveTip;
    public Image[] KeysImage;
    public Image[] TipsImage;

    public Animator AnimatorDoor { get; set; }
    public bool IsRoomPause { get; set; }

    public bool IsGateZone { get; set; }
    public bool IsAlphabetZone { get; set; }
    public bool IsTreasureZone { get; set; }
    public bool IsTreasureCalculateZone { get; set; }

    public bool GameRooms { get; set; }
    public int CurrentNumberRoom { get; set; }
    public GameObject CurrentDoorOpen { get; set; }

    //МЕНЕДЖЕРЫ
    AlertUI AlertUI;
    GateZoneController GateZoneController;
    TimerGame TimerGame;
    FB_CamMovingController FB_CamMovingController;
    //МЕНЕДЖЕРЫ

    public static FortBoyardGameController Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        AlertUI = AlertUI.Instance;
        GateZoneController = GateZoneController.Instance;
        TimerGame = TimerGame.Instance;
        FB_CamMovingController = FB_CamMovingController.Instance;

        if (isLockFPS)
        {
            Application.targetFrameRate = 65;
        }
        totalTips = GateZoneController.allTipsList.Count;
        TimerGame.seconds = totalTime;
        ReloadTimer();
        IsRoomPause = true;
    }
    IEnumerator ShowMainMenuFloating()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float i = 0;
            while (mainMenu.GetComponent<CanvasGroup>().alpha < 1)
            {
                i = i + 0.05f;
                inputGetKeyDownToContinue.SetActive(false);
                mainMenu.GetComponent<CanvasGroup>().alpha = i;
                mainMenu.GetComponent<CanvasGroup>().interactable = true;
                yield return null;//new WaitForSeconds(0.1f);
            }
        }
    }
    void Update()
    {
        StartCoroutine(ShowMainMenuFloating());
        EndTimes();
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            fpsGameObject.SetActive(!fpsGameObject.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlertUI.ShowAlert_PAUSE();
        }
        if (!IsGateZone)
        {
            for (int i = 0; i < CurrentKeys; i++)
            {
                KeysImage[i].sprite = ActiveKey;
                KeysImage[i].color = "#FFFFFFFF".ToColor();
            }

            for (int i = 0; i < CurrentTips; i++)
            {
                if (i >= TipsImage.Length) break;
                TipsImage[i].sprite = ActiveTip;
                TipsImage[i].color = "#FFFFFFFF".ToColor();
            }
        }

        if (IsTreasureZone)
        {
            if (TimerGame.seconds <= 10.0f)
            {
                GateZoneController.isOpenGate = false;
                GateZoneController.gateAnimation.SetBool("Closed", true);
            }
            canvasTreasureZone.SetActive(true);
        }
    }

    void EndTimes()
    {
        if (TimerGame.seconds <= TimerGame.endTime)
        {
            TimerGame.RunTime = false;
            string MessageTextForAlertUI = "<color=#FF1313FF>Время вышло!</color> К сожалению вы не справились с заданием.<br> \nВы можете вернуться в главное меню и попытаться еще раз!";
            if (IsTreasureZone)
            {
                AlertUI.ShowAlert_GAMEOVER_WITHOUT_ROOM(MessageTextForAlertUI);
                IsTreasureZone = false;
            }
            if (GameRooms)
            {
                LoseRoom("<color=#FF1313FF>Время вышло!</color>\nК сожалению вы не справились с испытанием");
            }
            if (IsAlphabetZone)
            {
                AlertUI.ShowAlert_GAMEOVER_WITHOUT_ROOM(MessageTextForAlertUI);
                IsAlphabetZone = false;
            }
            if (IsGateZone)
            {
                AlertUI.ShowAlert_GAMEOVER_WITHOUT_ROOM(MessageTextForAlertUI);
                IsGateZone = false;
            }
        }
    }

    public void DisableAllCheckZones()
    {
        IsGateZone = false;
        IsAlphabetZone = false;
        IsTreasureZone = false;
        IsTreasureCalculateZone = false;
    }

    public void ExitModalShow(int numberRoom) //ПРИ НАЖАТИИ НА КНОПКУ "ПОКИНУТЬ КОМНАТУ" ПОЯВЛЯЕТСЯ МОДАЛЬНОЕ ОКНО С ПРЕДУПРЕЖДЕНИЕМ
    {
        CurrentNumberRoom = numberRoom;
        string MessageForAlertUI = string.Empty;
        if (numberRoom == 0 || numberRoom == 1 || numberRoom == 2)
        {
            MessageForAlertUI = "Выйти из комнаты?\n<size=50>При этом вы не получите <i><color=#FF8400FF>ключ</i></color>.</size>";
        }
        if (numberRoom == 3 || numberRoom == 4 || numberRoom == 5)
        {
            MessageForAlertUI = "Выйти из комнаты?\n<size=50>При этом вы не получите <i><color=#FF8400FF>подсказку</i></color>.</size>";
        }
        AlertUI.ShowAlert_EXIT_ROOM_SKIP_GAME(MessageForAlertUI);
        IsRoomPause = true;
    }

    public void Close_Game_Room() //СКРЫВАЕМ КОМНАТУ ПОСЛЕ ЗАВЕРШЕНИЯ ИСПЫТАНИЯ (ПРОИГРЫШ ИЛИ ВЫИГРЫШ)
    {
        game_rooms[CurrentNumberRoom].SetActive(false);
        TimerGame.RunTime = false;
        TimerGame.seconds = totalTime;
        mainUconsUI.SetActive(true);
        AnimatorDoor.SetBool("doorIsClosed", true);
        audioSourceDoors.PlayOneShot(closedDoor);

        TextInfoToNextZone_Parent.SetActive(true);
        TextInfoToNextZone_Parent.transform.SetParent(CurrentDoorOpen.transform);
        Vector3 LocalPos = TextInfoToNextZone_Parent.transform.localPosition; //ИЗБАВЛЯЕМСЯ ОТ ДЛИННОЙ ПЕРЕМЕННОЙ(ДЛЯ УДОБСТВА)
        TextInfoToNextZone_Parent.transform.localPosition = new Vector3(0, LocalPos.y, LocalPos.z); //ПЕРЕМЕЩЕНИЕ ТЕКСТА ОТ ДВЕРИ К ДВЕРИ ПОСЛЕ ИСПЫТАНИЙ
        if (CurrentNumberRoom >= 5) //НУЖНО ПОВЕРНУТЬ ТЕКСТ НА 180 ГРАДУСОВ ЧТО БЫ ОТОБРАЖАЛСЯ КОРРЕКТНО НА ДРУГОЙ СТОРОНЕ ФОРТА
        {
            TextInfoToNextZone_Parent.transform.localPosition = new Vector3(0, LocalPos.y, 0);
            TextInfoToNextZone_Parent.transform.localEulerAngles = new Vector3(0, 180, 0);
        }

        StartCoroutine(ReverseTime(TimeToNextDoor, CurrentNumberRoom));
        ReloadTimer();
        EnabledObjects();
        AlertUI.CloseModalWindow();
        IsRoomPause = false;
        FB_CamMovingController.cameraToMovingFromScene.GetComponent<Camera>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = true;
    }

    public void DisabledObjects() //ВО ВРЕМЯ ИСПЫТАНИЯ СКРЫТИЕ ТЯЖЕЛЫХ ОБЪЕКТОВ НА СЦЕНЕ (ВОДА, ФОРТ)
    {
        for (int i = 0; i < disableObjects.Length; i++)
        {
            if (disableObjects[i] == null) continue;
            disableObjects[i].SetActive(false);
        }
        if (CurrentNumberRoom == 5) //НА 6 ИСПЫТАНИИ СКРЫВАЕТ ВРЕМЯ
        {
            watchUI.SetActive(false);
        }
    }
    public void EnabledObjects() //ПОСЛЕ ИСПЫТАНИЯ ОТОБРАЖАЕТ ТЯЖЕЛЫЕ ОБЪЕКТЫ НА СЦЕНЕ (ВОДА, ФОРТ)
    {
        for (int i = 0; i < disableObjects.Length; i++)
        {
            if (disableObjects[i] == null) continue;
            disableObjects[i].SetActive(true);
        }
        if (CurrentNumberRoom == 5) //ПОСЛЕ 6 ИСПЫТАНИЯ ОБРАТНО ПОКАЗЫВАЕМ ВРЕМЯ
        {
            watchUI.SetActive(true);
        }
    }

    IEnumerator ReverseTime(float time, int currentRoom)
    {
        while (time > 0)
        {
            time--;
            TextInfoToNextZone_Text.GetComponent<TextMeshProUGUI>().text = string.Format("ПЕРЕХОД К СЛЕДУЮЩЕМУ ИСПЫТАНИЮ ({0})", time.ToString());
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
                        StartCoroutine(FB_CamMovingController.GoToGateZone()); // Переход к зоне с воротами
                        break;
                }
                yield return new WaitForSeconds(1);
                TextInfoToNextZone_Parent.SetActive(false);
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void StartGame() //НАЧАТЬ ИГРУ ПОСЛЕ ВЫБОРА ПОЛЬЗОВАТЕЛЯ
    {
        mainMenu.SetActive(false);
        FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToBriefing);
        StartCoroutine(ShowRules());
    }

    IEnumerator ShowRules() //ПОКАЗАТЬ ПРАВИЛА ИГРЫ - ОБЩАЯ
    {
        yield return new WaitForSeconds(FB_CamMovingController.speedDurationMovingCamera);
        gameRulers.SetActive(true);
    }

    public void CloseRules() //ЗАКРЫТЬ ПРАВИЛА ИГРЫ - ОБЩАЯ
    {
        gameRulers.SetActive(false);
        FB_CamMovingController.CameraMovingToPoint(FB_CamMovingController.pointToDoor1);
        mainUconsUI.SetActive(true);
        watchUI.SetActive(true);
    }

    public void Close_Game_Rule(int numberRule) //ЗАКРЫТЬ ПРАВИЛА У ИСПЫТАНИЙ
    {
        game_rules_in_rooms[numberRule].SetActive(false);
        RunTimer();
        IsRoomPause = false;
    }


    //ФУНКЦИИ ИСПЫТАНИЙ - ПОБЕДА ИЛИ ПРОИГРЫШ
    public void WinnerRoom(string typeRoom)
    {
        if (typeRoom == "Keys") CurrentKeys += 1;
        else if (typeRoom == "Tips") CurrentTips += 1;
        totalTime += 10;
        AlertUI.ShowAlert_WIN_OR_LOSE_WITH_ROOM("<color=#87FF00FF>Поздравляем!</color>\nВы успешно справились с испытанием!", "Испытание пройдено!");
    }

    public void LoseRoom(string text)
    {
        AlertUI.ShowAlert_WIN_OR_LOSE_WITH_ROOM(text, "Испытание провалено!");
        TimerGame.RunTime = false;
        IsRoomPause = true;
    }
    //ФУНКЦИИ ИСПЫТАНИЙ - ПОБЕДА ИЛИ ПРОИГРЫШ


    //УПРАВЛЕНИЕ ТАЙМЕРОМ - ЗАПУСК, СТОП
    public void RunTimer()
    {
        TimerGame.RunTime = true;
        TimerGame.seconds = totalTime;
    }
    public void StopTimer()
    {
        TimerGame.RunTime = false;
        TimerGame.seconds = totalTime;
    }
    public void ReloadTimer()
    {
        totalTimeText.text = string.Format("{0:00}:{1:00}", (int)totalTime / 60, (int)totalTime % 60);
    }
    //УПРАВЛЕНИЕ ТАЙМЕРОМ - ЗАПУСК, СТОП
}
