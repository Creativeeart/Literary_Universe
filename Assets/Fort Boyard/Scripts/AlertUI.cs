using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using cakeslice;
using UnityEngine.SceneManagement;
public class AlertUI : MonoBehaviour
{
    public GameObject Alert_BASE;                               //Главная форма с фоновым размытием экрана

    public GameObject Alert_DEFAULT;                            //Простое предупреждение с одной кнопкой
    public TextMeshProUGUI Alert_DEFAULT_TextUI;                //

    public GameObject Alert_GAMEOVER_WITHOUT_ROOM;              //Предупреждение о проигрыше при прохождени испытаний ВНЕ комнат
    public TextMeshProUGUI Alert_GAMEOVER_WITHOUT_ROOM_TextUI;  //

    public GameObject Alert_WIN_OR_LOSE_WITH_ROOM;              //Предупреждение о проигрыше или выигрыше при прохождени испытаний ВНУТРИ комнат
    public TextMeshProUGUI Alert_WIN_OR_LOSE_WITH_ROOM_TextUI;  //
    public TextMeshProUGUI Alert_WIN_OR_LOSE_WITH_ROOM_Header_TextUI;

    public GameObject Alert_EXIT_ROOM_SKIP_GAME;                //Предупреждение если игрок пытается покинуть комнату не пройдя испытание
    public TextMeshProUGUI Alert_EXIT_ROOM_SKIP_GAME_TextUI;    //

    public GameObject Alert_PAUSE;                              //Предупреждение если игрок нажимает клавишу ESC.
    
    public bool isAlertUIActive = false;
    public static AlertUI Instance { get; private set; }
    SupportScripts SupportScripts;
    FortBoyardGameController FortBoyardGameController;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SupportScripts = SupportScripts.Instance;
        FortBoyardGameController = FortBoyardGameController.Instance;
    }

    public void ShowModalWindow()
    {
        Alert_BASE.SetActive(true);
        isAlertUIActive = true;
    }

    public void CloseModalWindow()
    {
        Alert_BASE.SetActive(false);
        isAlertUIActive = false;

        Alert_DEFAULT.SetActive(false);
        Alert_DEFAULT_TextUI.text = string.Empty;

        Alert_GAMEOVER_WITHOUT_ROOM.SetActive(false);
        Alert_GAMEOVER_WITHOUT_ROOM_TextUI.text = string.Empty;

        Alert_WIN_OR_LOSE_WITH_ROOM.SetActive(false);
        Alert_WIN_OR_LOSE_WITH_ROOM_TextUI.text = string.Empty;

        Alert_EXIT_ROOM_SKIP_GAME.SetActive(false);
        Alert_EXIT_ROOM_SKIP_GAME_TextUI.text = string.Empty;

        Alert_PAUSE.SetActive(false);
    }

    public void ReloadGame()
    {
        SupportScripts.UI.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SupportScripts.UI.SetActive(true);
    }

    public void ShowAlert_DEFAULT(string Message)
    {
        ShowModalWindow();

        Alert_DEFAULT.SetActive(true);
        Alert_DEFAULT_TextUI.text = Message;
    }

    public void ShowAlert_GAMEOVER_WITHOUT_ROOM(string Message)
    {
        ShowModalWindow();

        Alert_GAMEOVER_WITHOUT_ROOM.SetActive(true);
        Alert_GAMEOVER_WITHOUT_ROOM_TextUI.text = Message;
    }

    public void ShowAlert_WIN_OR_LOSE_WITH_ROOM(string Message, string HeaderMessage)
    {
        ShowModalWindow();

        Alert_WIN_OR_LOSE_WITH_ROOM.SetActive(true);
        Alert_WIN_OR_LOSE_WITH_ROOM_TextUI.text = Message;
        Alert_WIN_OR_LOSE_WITH_ROOM_Header_TextUI.text = HeaderMessage;
    }

    public void ShowAlert_EXIT_ROOM_SKIP_GAME(string Message)
    {
        ShowModalWindow();

        Alert_EXIT_ROOM_SKIP_GAME.SetActive(true);
        Alert_EXIT_ROOM_SKIP_GAME_TextUI.text = Message;
        FortBoyardGameController.IsRoomPause = true;
        Time.timeScale = 0;
    }
    public void CloseAlert_EXIT_ROOM_SKIP_GAME()
    {
        CloseModalWindow();
        FortBoyardGameController.IsRoomPause = false;
        Time.timeScale = 1;
    }

    public void ShowAlert_PAUSE()
    {
        ShowModalWindow();

        Alert_PAUSE.SetActive(true);
    }

}

