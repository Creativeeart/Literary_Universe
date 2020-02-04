using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace cakeslice
{
    public class Authorization : MonoBehaviour
    {
        [Header("Профиль гостевого аккаунта")]
        public GameObject guestProfileForm;
        public string guestLogin = "Guest";
        public string guestRealName = "Гость";

        [Header("Текущий пользователь")]
        public TextMeshProUGUI currentUserUI;
        public string currentUser = string.Empty;
        public string currentRealName = string.Empty;

        [Header("Поля для авторизации")]
        public TMP_InputField loginUsernameUI;
        string loginUsername = string.Empty;
        public TMP_InputField loginPasswordUI;
        string loginPassword = string.Empty;
        public TextMeshProUGUI loginErrorUI;

        [Header("Поля для регистрации")]
        public TMP_InputField registrationRealnameUI;
        public string registrationRealName = string.Empty;
        public TMP_InputField registrationUsernameUI;
        public string registrationUsername = string.Empty;
        public TMP_InputField registrationPasswordUI;
        public string registrationPassword = string.Empty;
        public TextMeshProUGUI registrationErrorUI;

        [Header("Панели авторизации и регистрации")]
        public GameObject loginAndregistrationForms;
        public GameObject loginForm;
        public GameObject registrationForm;

        public GameObject loginAndRegistrationButtons;
        public GameObject exitButtons;


        readonly string url = "http://literaryuniverse.unitycoding.ru";
        int statusAuthorization = 0; //0 = ошибка входа;       1 = успешный вход;        2 = неопределен(не использвать); 3 = поля не могут быть пустыми;
        int statusRegistration = 0;  //0 = ошибка регистрации; 1 = успешная регистрация; 2 = имя персонажа занято;        3 = поля не могут быть пустыми;

        public static Authorization instance;
        public bool isDontDestroyOnLoad = true;

        [SerializeField]
        public UserList[] usersList;

        void Awake()
        {
            if (isDontDestroyOnLoad)
            {
                if (instance == null)
                {
                    instance = this;
                }
                else
                {
                    Destroy(gameObject);
                    return;
                }
                DontDestroyOnLoad(gameObject);
            }
        }
        public void SavePlayerPrefs()
        {
            PlayerPrefs.SetString("currentUser", currentUser.ToString());
            PlayerPrefs.SetString("currentRealName", currentRealName.ToString());
        }

        public void LoadPlayerPrefs()
        {
            if (PlayerPrefs.HasKey("currentUser"))
            {
                currentUser = PlayerPrefs.GetString("currentUser");
                currentRealName = PlayerPrefs.GetString("currentRealName");
                currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + currentUser + "</u></color></i></b>";
                loginAndRegistrationButtons.SetActive(false);
                exitButtons.SetActive(true);
            }
            else
            {
                currentUser = guestLogin;
                currentRealName = guestRealName;
                currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + currentUser + "</u></color></i></b>";
                loginAndRegistrationButtons.SetActive(false);
                exitButtons.SetActive(true);
            }
        }
        void Start()
        {
            LoadPlayerPrefs();
            StartCoroutine(RefreshHighscores());
        }

        IEnumerator RefreshHighscores() //Обновить таблицу рекордов
        {
            while (true)
            {
                DownloadHighscores();
                yield return new WaitForSeconds(10);
            }
        }
        public void OpenGuestForm()
        {
            loginAndregistrationForms.SetActive(true);
            registrationForm.SetActive(false);
            loginForm.SetActive(false);
            guestProfileForm.SetActive(true);
        }

        public void OpenLoginForm()
        {
            loginAndregistrationForms.SetActive(true);
            registrationForm.SetActive(false);
            loginForm.SetActive(true);
            guestProfileForm.SetActive(false);
        }

        public void OpenRegistrationForm()
        {
            loginAndregistrationForms.SetActive(true);
            loginForm.SetActive(false);
            registrationForm.SetActive(true);
            guestProfileForm.SetActive(false);
        }

        public void CloseLoginAndRegistrationForm()
        {
            loginAndregistrationForms.SetActive(false);
            loginForm.SetActive(false);
            registrationForm.SetActive(false);
            guestProfileForm.SetActive(false);
        }

        public void Logout()
        {
            exitButtons.SetActive(false);
            loginAndRegistrationButtons.SetActive(true);
            currentUser = guestLogin;
            currentRealName = guestRealName;
            currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + currentUser + "</u></color></i></b>";
            PlayerPrefs.DeleteKey("currentUser");
        }

        public void ContinueFromGuest()
        {
            CloseLoginAndRegistrationForm();
            System.DateTime localDate = System.DateTime.Now;
            string tempDate = localDate.ToString().ReplaceFromCollection(new char[] { ':', '/', ' ', 'A', 'M', 'P' });
            guestLogin = guestLogin + tempDate;
            currentUser = guestLogin;
        }

        public void SwitchName()
        {
            Logout();
            ContinueFromGuest();
        }

        IEnumerator Login_POST()
        {
            var Data = new WWWForm();
            Data.AddField("login", loginUsername);
            Data.AddField("pass", loginPassword);
            var Query = new WWW(url + "/login.php", Data);
            yield return Query;
            if (Query.error != null)
            {
                Debug.Log("Сервер не отвечает: " + Query.error + "(Отсутствует подключение к интернету)");
                loginErrorUI.text = Query.error + "(Отсутствует подключение к интернету)";
            }
            else
            {
                Debug.Log("Сервер ответил: " + Query.text);
                statusAuthorization = int.Parse(Query.text);
                switch (statusAuthorization)
                {
                    case 0:
                        loginErrorUI.text = "Имя пользователя или пароль введены неверно, либо пользователя не существует";
                        break;
                    case 1:
                        loginErrorUI.text = "Авторизация успешна";
                        currentUser = loginUsername;
                        DownloadHighscores();
                        currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + currentUser + "</u></color></i></b>";
                        loginAndRegistrationButtons.SetActive(false);
                        exitButtons.SetActive(true);
                        CloseLoginAndRegistrationForm();
                        SavePlayerPrefs();
                        break;
                    case 3:
                        loginErrorUI.text = "Поля не могут быть пустыми";
                        break;
                }
            }
            Query.Dispose();
        }
        public void SendLogin()
        {
            if ((loginUsernameUI.text != string.Empty) && (loginPasswordUI.text != string.Empty))
            {
                loginUsername = loginUsernameUI.text;
                loginPassword = loginPasswordUI.text;
                StartCoroutine(Login_POST());
            }
            else
            {
                loginErrorUI.text = "Поля не могут пустыми";
            }
        }

        IEnumerator Registration_POST()
        {
            var Data = new WWWForm();
            Data.AddField("login", registrationUsername);
            Data.AddField("pass", registrationPassword);
            Data.AddField("realname", registrationRealName);
            var Query = new WWW(url + "/userAdds.php", Data);
            yield return Query;
            if (Query.error != null)
            {
                Debug.Log("Сервер не отвечает: " + Query.error);
                registrationErrorUI.text = Query.error + "(Отсутствует подключение к интернету)";
            }
            else
            {
                Debug.Log("Сервер ответил: " + Query.text);
                statusRegistration = int.Parse(Query.text);
                switch (statusRegistration)
                {
                    case 0:
                        registrationErrorUI.text = "Ошибка регистрации";
                        break;
                    case 1:
                        registrationErrorUI.text = "Регистрация успешна";
                        loginUsername = registrationUsername;
                        loginPassword = registrationPassword;
                        currentUser = registrationUsername;
                        currentRealName = registrationRealName;
                        StartCoroutine(Login_POST());
                        break;
                    case 2:
                        registrationErrorUI.text = "Имя пользователя уже занято";
                        break;
                    case 3:
                        registrationErrorUI.text = "Поля не могут быть пустыми";
                        break;
                }
            }
            Query.Dispose();
        }
        public void SendRegistration()
        {
            if ((registrationRealnameUI.text != string.Empty) && (registrationUsernameUI.text != string.Empty) && (registrationPasswordUI.text != string.Empty))
            {
                registrationRealName = registrationRealnameUI.text;
                registrationUsername = registrationUsernameUI.text;
                registrationPassword = registrationPasswordUI.text;
                StartCoroutine(Registration_POST());
            }
            else
            {
                registrationErrorUI.text = "Поля не могут пустыми";
            }
        }
        public void SendRegistrationWithGuest()
        {
            StartCoroutine(Registration_POST());
        }


        public void DownloadHighscores() //Загрузка результатов в таблицу рекордов
        {
            StartCoroutine(DownloadHighscoresFromDatabase());
        }

        IEnumerator DownloadHighscoresFromDatabase()
        {
            WWW www = new WWW(url + "/users.txt");
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                FormatHighscores(www.text);
                OnRealName();
                Debug.Log("База загружена");
            }
            else
            {
                Debug.Log("Ошибка Загрузки: " + www.error);
            }
        }


        public void OnRealName()
        {
            for (int i = 0; i < usersList.Length; i++)
            {
                if (currentUser == usersList[i].username)
                {
                    currentRealName = usersList[i].realname;
                    break;
                }
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
                string realname = entryInfo[2];
                //int score = int.Parse(entryInfo[2]);
                //string donate = entryInfo[4];
                usersList[i] = new UserList(username, realname/*, score, donate*/);
            }
        }

    }

    [System.Serializable]
    public struct UserList
    {
        public string username;
        //public string realname;
        //public int score;
        //public string donate;
        public string realname;

        public UserList(string _username, string _realname/*, int _score, string _donate*/)
        {
            username = _username;
            realname = _realname;
            //score = _score;
            //donate = _donate;
        }
    }

}

static class Extensions
{
    public static string ReplaceFromCollection(this string text, IEnumerable<char> characters)
    {
        foreach (var chr in characters)
        {
            text = text.Replace(chr.ToString(), string.Empty);
        }
        return text;
    }
}
