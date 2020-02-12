using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using cakeslice;
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

    [Header("Game Menu")]
    public GameObject FirstMainMenu;
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
    public Button StatusCreateUserMenu_StartGameBTN;
    public bool isUserCreate = false;
    public bool isStatusCreateUserMenu = false;

    [Header("Select Users Menu")]
    public Transform UserInfo_Parent;
    public GameObject UserInfo_Prefab;
    public GameObject SelectUserMenu; //Общая форма выбора пользователя из списка
    public GameObject InputPasswordLockedUser; //Форма ввода пароля для защищеного паролем пользователя
    public TMP_InputField InputPasswordUI;
    public TextMeshProUGUI StatusInputPasswordText; //Текст отображения статуса ввода пароля (правильно, неправильно)
    public TextMeshProUGUI HeaderTitleInputPasswordText; //Текст отображения статуса ввода пароля (правильно, неправильно)

    [Header("ConfirmAfterSelectUser")]
    public GameObject ConfirmAfterSelectUserMenu; //Форма подтверждения выбранного пользователя
    public TextMeshProUGUI CurrentSelectUserNameText; //Текст отображения выбранного пользователя
    public bool isConfirmAfterSelectUser = false;

    [Header("Guest User Menu")]
    public GameObject GuestUserMenu; //Общая форма выбора пользователя из списка
    public bool isGuestMenu = false;

    [Header("Profile User Menu")]
    public GameObject ProfileUserMenu; //Общая форма выбора пользователя из списка
    public TextMeshProUGUI UserLoginUI;
    public TextMeshProUGUI UserRealNameUI;
    public TextMeshProUGUI UserRecordsUI;
    public bool isProfileUserMenu = false;


    [SerializeField]
    public UserList[] usersList;

    public static FB_GameMenuController Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(CheckInternetConnection()); //Проверка соединения с интернетом
        LoadPlayerPrefs();
        StartCoroutine(DownloadUsersFromDatabase());
    }
    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetString("currentLogin", currentLogin.ToString());
        PlayerPrefs.SetString("currentRealName", currentRealName.ToString());
        PlayerPrefs.SetString("currentPassword", currentPassword.ToString());
    }

    public void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("currentLogin"))
        {
            isProfileUserMenu = true;
            currentLogin = PlayerPrefs.GetString("currentLogin");
            currentRealName = PlayerPrefs.GetString("currentRealName");
            currentPassword = PlayerPrefs.GetString("currentPassword");
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

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("currentLogin");
        PlayerPrefs.DeleteKey("currentRealName");
        PlayerPrefs.DeleteKey("currentPassword");
        currentLogin = string.Empty;
        currentRealName = string.Empty;
        currentPassword = string.Empty;
        isProfileUserMenu = false;
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
                Debug.Log("Интернет - соединение: Онлайн");
                CreateUserBTN.interactable = true;
                SelectUserBTN.interactable = true;
            }
            else
            {
                isInternetConnection = false;
                StatusInternetConnectionText.text = "Интернет-соединение: <color=red>Оффлайн</color>";
                Debug.Log("Интернет - соединение: Оффлайн");
                CreateUserBTN.interactable = false;
                SelectUserBTN.interactable = false;
            }
            yield return new WaitForSeconds(1);
        }
    }

    //ГЛАВНЫЙ КАНВАС
    public void ShowFBMainMenuCanvas() //Показать канвас главного меню
    {
        if (PlayerPrefs.HasKey("currentLogin"))
        {
            FBGameMenuCanvas.SetActive(true);
            ShowUserProfile();
        }
        else
        {
            FBGameMenuCanvas.SetActive(true);
            ShowMainMenu();
        }
    }

    public void CloseFBMainMenuCanvas() //Закрыть канвас главного меню
    {
        FBGameMenuCanvas.SetActive(false);
        CloseMainMenu();
        CloseUserProfile();
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
        if (isUserCreate)
        {
            CloseMainMenu();
            LoadPlayerPrefs();
            ShowUserProfile();
            StatusCreateUserMenu.SetActive(false);
        }
        else
        {
            ShowMainMenu();
            StatusCreateUserMenu.SetActive(false);
        }
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
        StatusInputPasswordText.text = string.Empty;
    }
    //ОКНО ВВОДА ПАРОЛЯ ДЛЯ ПОЛЬЗОВАТЕЛЯ ИЗ СПИСКА


    //ОКНО ПОДТВЕРЖДЕНИЯ ПОЛЬЗОВАТЕЛЯ ПОСЛЕ ВЫБОРА ИЗ СПИСКА
    string TempUserName = string.Empty;
    string TempRealName = string.Empty;
    string TempPassword = string.Empty;
    public void ShowConfirmAfterSelectUserMenu(string UserName, string RealName, string Password, bool isLocked) //Открыть окно после выбора пользователя из списка
    {
        TempUserName = UserName;
        TempRealName = RealName;
        TempPassword = Password;
        if (isLocked)
        {
            ShowInputPasswordLockedUser();
            HeaderTitleInputPasswordText.text = "Ввод пароля для пользователя - <color=#ffa800>" + RealName + "</color>";
        }
        else
        {
            CloseInputPasswordLockedUser();
            ConfirmAfterSelectUserMenu.SetActive(true);
            CurrentSelectUserNameText.text = "Продолжить как: <color=#ffae00>" + RealName +"</color> ?";
        }
    }
    public void CheckPassword()
    {
        if (InputPasswordUI.text == TempPassword)
        {
            CloseInputPasswordLockedUser();
            AcceptConfirmAfterSelectUserMenu();
        }
        else
        {
            Debug.Log("Not Correct Password");
            StatusInputPasswordText.text = "Пароль не верный";
        }
    }

    public void CloseConfirmAfterSelectUserMenu() //Закрыть окно после выбора пользователя из списка и отобразить список пользователей
    {
        CloseMainMenu();
        SelectUserMenu.SetActive(true);
        CloseInputPasswordLockedUser();
        ConfirmAfterSelectUserMenu.SetActive(false);
    }

    public void AcceptConfirmAfterSelectUserMenu()
    {
        isConfirmAfterSelectUser = true;
        isStatusCreateUserMenu = false;
        isGuestMenu = false;
        currentLogin = TempUserName;
        currentRealName = TempRealName;
        currentPassword = TempPassword;
        SavePlayerPrefs();
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


    //МЕНЮ ПРОФИЛЬ ПОЛЬЗОВАТЕЛЯ
    public void ShowUserProfile()
    {
        ProfileUserMenu.SetActive(true);
        UserLoginUI.text = "Текущий профиль - " + currentLogin;
        UserRealNameUI.text = "Добро пожаловать, <color=#ffae00><b>" + currentRealName + "</color> !" ;
    }

    public void CloseUserProfile()
    {
        ProfileUserMenu.SetActive(false);
    }

    public void ExitUserProfile()
    {
        DeletePlayerPrefs();
        CloseUserProfile();
        ShowFBMainMenuCanvas();
    }
    //МЕНЮ ПРОФИЛЬ ПОЛЬЗОВАТЕЛЯ



    public void StartGame()
    {
        if (isStatusCreateUserMenu)
        {
            CloseStatusAfterCreateUser();
            CloseFBMainMenuCanvas();
            FirstMainMenu.SetActive(false);
            FortBoyardGameController.Instance.StartGame();
            Debug.Log("Start game after CREATE user");
        }
        if (isConfirmAfterSelectUser)
        {
            CloseConfirmAfterSelectUserMenu();
            CloseSelectUserMenu();
            CloseMainMenu();
            //CloseFBMainMenuCanvas();
            ShowUserProfile();
            Debug.Log("Start game after SELECT user");
        }
        if (isGuestMenu)
        {
            CloseGuestMenu();
            CloseFBMainMenuCanvas();
            Debug.Log("Start game after GUEST profile");
            currentLogin = "guest";
            currentRealName = "guest";
            currentPassword = "guest";
            FirstMainMenu.SetActive(false);
            FortBoyardGameController.Instance.StartGame();
        }
        if (isProfileUserMenu)
        {
            CloseUserProfile();
            Debug.Log("Start game after USER profile");
            CloseFBMainMenuCanvas();
            FirstMainMenu.SetActive(false);
            FortBoyardGameController.Instance.StartGame();
        }
    }

    public string GenerateLogin()
    {
        System.DateTime localDate = System.DateTime.Now;
        string tempDate = localDate.ToString().ReplaceFromCollection(new char[] { ':', '/', ' ', 'A', 'M', 'P' });
        string result = string.Empty;
        result = "User" + tempDate;
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
                    isUserCreate = false;
                    Debug.Log("Ошибка регистрации");
                    StatusCreateUserMenu_StartGameBTN.interactable = false;
                    StatusCreateUserText.text = "<color=red>Ошибка регистрации, попытайтесь еще раз</color>";
                    break;
                case 1:
                    isUserCreate = true;
                    Debug.Log("Регистрация успешна");
                    StatusCreateUserText.text = "Пользователь: <color=#ffae00>"+ registrationRealName + "</color>, <color=green>успешно создан</color>";
                    StatusCreateUserMenu_StartGameBTN.interactable = true;
                    currentLogin = registrationLogin;
                    currentRealName = registrationRealName;
                    currentPassword = registrationPassword;
                    SavePlayerPrefs();
                    //StartCoroutine(Login_POST());
                    break;
                case 2:
                    isUserCreate = false;
                    Debug.Log("Имя пользователя уже занято");
                    StatusCreateUserText.text = "<color=red>Имя пользователя уже занято</color>";
                    StatusCreateUserMenu_StartGameBTN.interactable = false;
                    break;
                case 3:
                    isUserCreate = false;
                    StatusCreateUserText.text = "<color=red>Поля не могут быть пустыми</color>";
                    StatusCreateUserMenu_StartGameBTN.interactable = false;
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

    IEnumerator DownloadUsersFromDatabase()
    {
        WWW www = new WWW(webURL + "/users.txt");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            for (int i = 0; i < usersList.Length; i++)
            {
                GameObject ins = Instantiate(UserInfo_Prefab, UserInfo_Parent);
                ins.GetComponent<UserInfo>().UserName = usersList[i].username;
                ins.GetComponent<UserInfo>().RealName = usersList[i].realname;
                ins.GetComponent<UserInfo>().Password = usersList[i].password;
                ins.GetComponent<UserInfo>().DateRegistration = usersList[i].dateRegistration;
                ins.GetComponent<UserInfo>().Score = usersList[i].score;
                ins.GetComponent<UserInfo>().DateScore = usersList[i].dateScore;
                ins.GetComponent<UserInfo>().DonationName = usersList[i].donationName;
            }
            Debug.Log("База загружена");
        }
        else
        {
            Debug.Log("Ошибка Загрузки: " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        usersList = new UserList[entries.Length];
        //records = new int[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            string realname = entryInfo[1];
            string password = entryInfo[2];
            string dateRegistration = entryInfo[3];
            int score = int.Parse(entryInfo[4]);
            string dateScore = entryInfo[5];
            string donationName = entryInfo[6];
            usersList[i] = new UserList(username, realname, password, dateRegistration, score, dateScore, donationName);
        }
    }



}
[System.Serializable]
public struct UserList
{
    public string username;
    public string realname;
    public string password;
    public string dateRegistration;
    public int score;
    public string dateScore;
    public string donationName;

    public UserList(string _username, string _realname, string _password, string _dateRegistration, int _score, string _dateScore, string _donationName)
    {
        username = _username;
        realname = _realname;
        password = _password;
        dateRegistration = _dateRegistration;
        score = _score;
        dateScore = _dateScore;
        donationName = _donationName;
    }
}

