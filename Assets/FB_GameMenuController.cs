using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FB_GameMenuController : MonoBehaviour {
    [Header("Settings")]
    const string webURL = "http://literaryuniverse.unitycoding.ru";
    public string currentLogin = string.Empty;
    public string currentRealName = string.Empty;
    public string currentPassword = string.Empty;
    string registrationLogin = string.Empty;
    string registrationRealName = string.Empty;
    string registrationPassword = string.Empty;
    int statusRegistration = 0;  //0 = ошибка регистрации; 1 = успешная регистрация; 2 = имя персонажа занято;        3 = поля не могут быть пустыми;
    //[SerializeField]
    //public UserList[] usersList;

    [Header("Game Menu")]
    public GameObject FBGameMenuCanvas; //Главный Canvas

    [Header("Internet Status")]
    public TextMeshProUGUI StatusInternetConnectionText; //Статус состояния интернета (онлайн, оффлайн)
    public bool isInternetConnection = false;

    [Header("Main Menu")]
    public GameObject MainMenu; //Главное меню
    public Button CreateUserBTN;
    public Button SelectUserBTN;

    [Header("Create User Menu")]
    public GameObject CreateUserMenu; //Общая форма создания пользователя
    public TMP_InputField InputNameField; //Форма ввода имени
    public TMP_InputField InputPasswordField; //Форма ввода пароля

    [Header("Status Create User Menu")]
    public GameObject StatusCreateUserMenu; //Форма стадии статуса после создания пользователя
    public TextMeshProUGUI StatusCreateUserText; //Текст отображения статуса создания пользователя (успешно, ошибка)
    public bool isStatusCreateUserMenu = false;

    [Header("Select Users Menu")]
    public GameObject SelectUserMenu; //Общая форма выбора пользователя из списка
    public GameObject InputPasswordLockedUser; //Форма ввода пароля для защищеного паролем пользователя
    public TextMeshProUGUI StatusInputPasswordText; //Текст отображения статуса ввода пароля (правильно, неправильно)

    [Header("ConfirmAfterSelectUser")]
    public GameObject ConfirmAfterSelectUserMenu; //Форма подтверждения выбранного пользователя
    public TextMeshProUGUI CurrentSelectUserNameText; //Текст отображения выбранного пользователя
    public bool isConfirmAfterSelectUser = false;

    [Header("Guest User Menu")]
    public GameObject GuestUserMenu; //Общая форма выбора пользователя из списка
    public bool isGuestMenu = false;

    public static FB_GameMenuController Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadPlayerPrefs();
        StartCoroutine(CheckInternetConnection()); //Проверка соединения с интернетом
    }
    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetString("currentLogin", currentLogin.ToString());
        PlayerPrefs.SetString("currentRealName", currentRealName.ToString());
    }

    public void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("currentLogin"))
        {
            currentLogin = PlayerPrefs.GetString("currentLogin");
            currentRealName = PlayerPrefs.GetString("currentRealName");
            //currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + currentUser + "</u></color></i></b>";
            //loginAndRegistrationButtons.SetActive(false);
            //exitButtons.SetActive(true);
        }
        else
        {
            //currentUser = guestLogin;
            //currentRealName = guestRealName;
            //currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + currentUser + "</u></color></i></b>";
            //loginAndRegistrationButtons.SetActive(false);
            //exitButtons.SetActive(true);
        }
    }
    IEnumerator CheckInternetConnection() //Проверка соединения с интернетом
    {
        while (true)
        {
            WWW www = new WWW("http://google.com");
            yield return www;
            if (www.error == null)
            {
                isInternetConnection = true;
                StatusInternetConnectionText.text = "Интернет-соединение: <color=green>Онлайн</color>";
                CreateUserBTN.interactable = true;
                SelectUserBTN.interactable = true;
            }
            else
            {
                isInternetConnection = false;
                StatusInternetConnectionText.text = "Интернет-соединение: <color=red>Оффлайн</color>";
                CreateUserBTN.interactable = false;
                SelectUserBTN.interactable = false;
            }
            yield return new WaitForSeconds(1);
        }
    }

    //ГЛАВНЫЙ КАНВАС
    public void ShowFBMainMenuCanvas() //Показать канвас главного меню
    {
        FBGameMenuCanvas.SetActive(true);
    }

    public void CloseFBMainMenuCanvas() //Закрыть канвас главного меню
    {
        FBGameMenuCanvas.SetActive(false);
    }
    //ГЛАВНЫЙ КАНВАС


    //ГЛАВНОЕ МЕНЮ
    public void ShowMainMenu() //Показать главное меню
    {
        MainMenu.SetActive(true);
    }

    public void CloseMainMenu() //Закрыть главное меню
    {
        MainMenu.SetActive(false);
    }
    //ГЛАВНОЕ МЕНЮ


    //МЕНЮ СОЗДАНИЯ ПОЛЬЗОВАТЕЛЯ
    public void ShowCreateUserMenu() //Показать меню создания пользователя и закрыть главное меню
    {
        CloseMainMenu();
        CreateUserMenu.SetActive(true);
    }

    public void CloseCreateUserMenu() //Закрыть меню создания пользователя и открыть главное меню
    {
        ShowMainMenu();
        CreateUserMenu.SetActive(false);
    }
    //МЕНЮ СОЗДАНИЯ ПОЛЬЗОВАТЕЛЯ


    //РЕЗУЛЬТАТ СОЗДАНИЯ ПОЛЬЗОВАТЕЛЯ
    public void ShowStatusAfterCreateUser() //Показать результат создания пользователя и закрыть окно создания пользователя
    {
        if ((InputNameField.text != string.Empty))
        {
            CreateUserMenu.SetActive(false);
            StatusCreateUserMenu.SetActive(true);
            SendRegistration();
        }
    }

    public void CloseStatusAfterCreateUser() //Закрыть результат создания пользователя, вернуться в главное меню
    {
        ShowMainMenu();
        StatusCreateUserMenu.SetActive(false);
    }

    public void AcceptStatusAfterCreateUserMenu()
    {
        isStatusCreateUserMenu = true;
        isConfirmAfterSelectUser = false;
        isGuestMenu = false;
        StartGame();
    }
    //РЕЗУЛЬТАТ СОЗДАНИЯ ПОЛЬЗОВАТЕЛЯ


    //МЕНЮ ВЫБОРА ПОЛЬЗОВАТЕЛЯ ИЗ СПИСКА
    public void ShowSelectUserMenu() //Показать список пользователей и закрыть главное меню
    {
        CloseMainMenu();
        SelectUserMenu.SetActive(true);
    }
    public void CloseSelectUserMenu() //Закрыть список пользователей и показать главное меню
    {
        ShowMainMenu();
        SelectUserMenu.SetActive(false);
    }
    //МЕНЮ ВЫБОРА ПОЛЬЗОВАТЕЛЯ ИЗ СПИСКА


    //ОКНО ВВОДА ПАРОЛЯ ДЛЯ ПОЛЬЗОВАТЕЛЯ ИЗ СПИСКА
    public void ShowInputPasswordLockedUser() //Показать окно ввода пароля
    {
        InputPasswordLockedUser.SetActive(true);
    }

    public void CloseInputPasswordLockedUser() //Закрыть окно ввода пароля
    {
        InputPasswordLockedUser.SetActive(false);
    }
    //ОКНО ВВОДА ПАРОЛЯ ДЛЯ ПОЛЬЗОВАТЕЛЯ ИЗ СПИСКА


    //ОКНО ПОДТВЕРЖДЕНИЯ ПОЛЬЗОВАТЕЛЯ ПОСЛЕ ВЫБОРА ИЗ СПИСКА
    public void ShowConfirmAfterSelectUserMenu() //Открыть окно после выбора пользователя из списка
    {
        CloseInputPasswordLockedUser();
        ConfirmAfterSelectUserMenu.SetActive(true);
    }

    public void CloseConfirmAfterSelectUserMenu() //Закрыть окно после выбора пользователя из списка и отобразить список пользователей
    {
        ShowSelectUserMenu();
        CloseInputPasswordLockedUser();
        ConfirmAfterSelectUserMenu.SetActive(false);
    }

    public void AcceptConfirmAfterSelectUserMenu()
    {
        isConfirmAfterSelectUser = true;
        isStatusCreateUserMenu = false;
        isGuestMenu = false;
        StartGame();
    }
    //ОКНО ПОДТВЕРЖДЕНИЯ ПОЛЬЗОВАТЕЛЯ ПОСЛЕ ВЫБОРА ИЗ СПИСКА


    //МЕНЮ ГОСТЕВОГО ВХОДА
    public void ShowGuestMenu() //Открыть окно гостевого входа
    {
        CloseMainMenu();
        GuestUserMenu.SetActive(true);
    }

    public void CloseGuestMenu() //Закрыть окно гостевого входа
    {
        ShowMainMenu();
        GuestUserMenu.SetActive(false);
    }

    public void AcceptGuestMenu()
    {
        isGuestMenu = true;
        isStatusCreateUserMenu = false;
        isConfirmAfterSelectUser = false;
        StartGame();
    }
    //МЕНЮ ГОСТЕВОГО ВХОДА


    public void StartGame()
    {
        if (isStatusCreateUserMenu)
        {
            CloseStatusAfterCreateUser();
            CloseFBMainMenuCanvas();
            Debug.Log("Start game after CREATE user");
        }
        if (isConfirmAfterSelectUser)
        {
            CloseConfirmAfterSelectUserMenu();
            CloseSelectUserMenu();
            CloseFBMainMenuCanvas();
            Debug.Log("Start game after SELECT user");
        }
        if (isGuestMenu)
        {
            CloseGuestMenu();
            CloseFBMainMenuCanvas();
            Debug.Log("Start game after GUEST user");
        }
    }

    public string GenerateLogin()
    {
        System.DateTime localDate = System.DateTime.Now;
        string tempDate = localDate.ToString().ReplaceFromCollection(new char[] { ':', '/', ' ', 'A', 'M', 'P' });
        string result = string.Empty;
        result = "Guest" + tempDate;
        return result;
    }

    public string GeneratePassword()
    {
        string result = string.Empty;
        if (InputPasswordField.text != string.Empty)
        {
            result = InputPasswordField.text;
        }
        else
        {
            result = "guest";
        }
        return result;
    }

    IEnumerator Registration_POST()
    {
        var Data = new WWWForm();
        Data.AddField("login", registrationLogin);
        Data.AddField("pass", registrationPassword);
        Data.AddField("realname", registrationRealName);
        var Query = new WWW(webURL + "/userAdds.php", Data);
        yield return Query;
        if (Query.error != null)
        {
            Debug.Log("Сервер не отвечает: " + Query.error);
            StatusCreateUserText.text = "<color=red>"+ Query.error + " (Отсутствует подключение к интернету)</color>";
            //registrationErrorUI.text = Query.error + "(Отсутствует подключение к интернету)";
        }
        else
        {
            Debug.Log("Сервер ответил: " + Query.text);
            statusRegistration = int.Parse(Query.text);
            switch (statusRegistration)
            {
                case 0:
                    Debug.Log("Ошибка регистрации");
                    StatusCreateUserText.text = "<color=red>Ошибка регистрации, попытайтесь еще раз</color>";
                    break;
                case 1:
                    Debug.Log("Регистрация успешна");
                    StatusCreateUserText.text = "Пользователь: <color=#ffae00>"+ registrationRealName + "</color>, <color=green>успешно создан</color>";
                    currentLogin = registrationLogin;
                    currentRealName = registrationRealName;
                    currentPassword = registrationPassword;
                    SavePlayerPrefs();
                    //StartCoroutine(Login_POST());
                    break;
                case 2:
                    Debug.Log("Имя пользователя уже занято");
                    StatusCreateUserText.text = "<color=red>Имя пользователя уже занято</color>";
                    break;
                case 3:
                    StatusCreateUserText.text = "<color=red>Поля не могут быть пустыми</color>";
                    break;
            }
        }
        Query.Dispose();
    }
    public void SendRegistration()
    {
        if ((InputNameField.text != string.Empty))
        {
            registrationLogin = GenerateLogin();
            registrationRealName = InputNameField.text;
            registrationPassword = GeneratePassword();
            StartCoroutine(Registration_POST());
        }
        else
        {
            StatusCreateUserText.text = "<color=red>Поля не могут быть пустыми</color>";
        }
    }



}
