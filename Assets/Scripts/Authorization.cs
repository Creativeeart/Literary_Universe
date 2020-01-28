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

        [Header("Текущий пользователь")]
        public TextMeshProUGUI currentUserUI;
        public string currentUser = string.Empty;

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


        Highscores_FortBoyard _highscores_FortBoyard;

        bool internetConnection = false;
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
        void Start()
        {
            currentUser = "Гость";
        }
        public void ContinueFromGuest()
        {
            _highscores_FortBoyard = GameObject.Find("HighScoreManager").GetComponent<Highscores_FortBoyard>();
            _highscores_FortBoyard.ContinueFromGuest();
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
            currentUser = "Гость";
            currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + currentUser + "</u></color></i></b>";
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
                Debug.Log("Сервер не отвечает: " + Query.error);
                loginErrorUI.text = Query.error;
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
                        currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + currentUser + "</u></color></i></b>";
                        loginAndRegistrationButtons.SetActive(false);
                        exitButtons.SetActive(true);
                        CloseLoginAndRegistrationForm();
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
        IEnumerator CheckInternetConnection()
        {
            WWW www = new WWW("http://google.com");
            yield return www;
            if (www.error != null)
            {
                internetConnection = false;
                Debug.Log("Ошибка интернета");
            }
            else
            {
                internetConnection = true;
                Debug.Log("Интернет работает");
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
                registrationErrorUI.text = Query.text;
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

    }
}
