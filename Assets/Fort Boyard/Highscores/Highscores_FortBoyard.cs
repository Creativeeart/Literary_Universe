using UnityEngine;
using System.Collections;
using TMPro;
using System.Linq;
using cakeslice;
    public class Highscores_FortBoyard : MonoBehaviour
    {
        public GameObject loginFormUI, inputField;
        public TextMeshProUGUI[] highscoreFields, highscoreRecord, highscoreDonate;
        public TMP_InputField registrationRealnameUI;
        public Highscore_FortBoyard[] highscoresList;
        //const string privateCode = "skTTCvwFf0-wg9Q_TJ-_Wws59uWyamAU6na3BTMSXjYg";
        //const string publicCode = "5d416c3a76827f1758c4b7da";
        //const string webURL = "http://dreamlo.com/lb/";
        const string webURL = "http://literaryuniverse.unitycoding.ru";
        string username;
        public string realname;
        public string donationName;
        int[] records;
        int statusRegistration = 0;  //0 = ошибка регистрации; 1 = успешная регистрация; 2 = имя персонажа занято;        3 = поля не могут быть пустыми;
        //string text;
        [HideInInspector]
        public int maxValue;
        public static Highscores_FortBoyard Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            for (int i = 0; i < highscoreFields.Length; i++)
            {
                highscoreFields[i].text = "-";
                highscoreRecord[i].text = "-";
                highscoreDonate[i].text = "-";
            }
            StartCoroutine("RefreshHighscores");
        }



        IEnumerator RefreshHighscores()
        {
            while (true)
            {
                DownloadHighscores();
                yield return new WaitForSeconds(15);
            }
        }

        public void ContinueFromGuest()
        {
            SupportScripts.Instance._authorization.CloseLoginAndRegistrationForm();
            //_fortBoyardGameController.StartGame();
            //username = _supportScripts._authorization.currentUser;
            username = "Guest" + Random.Range(0, 99999) + Random.Range(0, 99999);
            loginFormUI.SetActive(true);
        }

        public void LaunchGame()
        {
            if (SupportScripts.Instance._authorization.currentUser == "Гость")
            {
            SupportScripts.Instance._authorization.OpenGuestForm();
            }
            else
            {
                username = SupportScripts.Instance._authorization.currentUser;
                FortBoyardGameController.Instance.StartGame();
            }
        }

        public void EnterName()
        {
            if (inputField.GetComponent<TMP_InputField>().text != string.Empty)
            {
                realname = inputField.GetComponent<TMP_InputField>().text;
                FortBoyardGameController.Instance.StartGame();
                loginFormUI.SetActive(false);
            SupportScripts.Instance._authorization.registrationRealName = realname;
            SupportScripts.Instance._authorization.registrationUsername = username;
            SupportScripts.Instance._authorization.registrationPassword = "guest";
            //_supportScripts._authorization.currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + _supportScripts._authorization.currentUser + "</u></color></i></b>";
            SupportScripts.Instance._authorization.loginAndRegistrationButtons.SetActive(false);
            SupportScripts.Instance._authorization.exitButtons.SetActive(true);
            SupportScripts.Instance._authorization.SendRegistrationWithGuest();
                //_supportScripts._authorization.CloseLoginAndRegistrationForm();
            }
        }
        public void UpdateDonationName()
        {
            donationName = TreasureCalculateZoneController.Instance.DonationName;
        }

        public void UpdatedScoreName()
        {
            AddNewHighscore(username, (int)TreasureCalculateZoneController.Instance.TotalCalculateCoins, donationName);
        }

        public void AddNewHighscore(string username, int score, string donationName)
        {
            StartCoroutine(UploadNewHighscore(username, score, donationName));
        }

        IEnumerator UploadNewHighscore(string username, int score, string donationName)
        {
            //WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
            var Data = new WWWForm();
            Data.AddField("login", username);
            Data.AddField("realname", realname);
            Data.AddField("fb_score", score);
            Data.AddField("donation_name", donationName);
            var www = new WWW(webURL + "/highScoreAdds.php", Data);
            yield return www;
            if (www.error != null)
            {
                Debug.Log("Сервер не отвечает: " + www.error);
                //registrationErrorUI.text = Query.text;
            }
            else
            {
                Debug.Log("Сервер ответил: " + www.text);
                //statusRegistration = int.Parse(www.text);
                //text = www.text;
                switch (statusRegistration)
                {
                    case 0:
                        //registrationErrorUI.text = "Ошибка регистрации";
                        break;
                    case 1:
                        //registrationErrorUI.text = "Регистрация успешна";
                        //loginUsername = registrationUsername;
                        //loginPassword = registrationPassword;
                        //currentUser = registrationUsername;
                        //StartCoroutine(Login_POST());
                        break;
                    case 2:
                        //registrationErrorUI.text = "Имя пользователя уже занято";
                        break;
                    case 3:
                        //registrationErrorUI.text = "Поля не могут быть пустыми";
                        break;
                }
            }
            www.Dispose();
            //if (string.IsNullOrEmpty(www.error))
            //{
            //    Debug.Log("Успешно загружено в онлайн базу");
            //    DownloadHighscores();
            //}
            //else
            //{
            //    Debug.Log("Ошибка загрузки в онлайн базу: " + www.error);
            //}
        }
        /// <summary>
        /// Кира Булычева
        /// Технический разработчик изменить у себя
        /// </summary>

        public void DownloadHighscores()
        {
            StartCoroutine("DownloadHighscoresFromDatabase");
        }

        IEnumerator DownloadHighscoresFromDatabase()
        {
            //WWW www = new WWW(webURL + publicCode + "/pipe/");
            WWW www = new WWW(webURL + "/scores.txt");
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                FormatHighscores(www.text);
                OnHighscoresDownloaded(highscoresList);
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
            highscoresList = new Highscore_FortBoyard[entries.Length];
            records = new int[entries.Length];
            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[1];
                int score = int.Parse(entryInfo[2]);
                string donate = entryInfo[4];
                highscoresList[i] = new Highscore_FortBoyard(username, score, donate);
            }
        }

        public void OnHighscoresDownloaded(Highscore_FortBoyard[] highscoreList)
        {
            for (int i = 0; i < highscoreFields.Length; i++)
            {
                if (i < highscoreList.Length)
                {
                    highscoreFields[i].text = highscoreList[i].username;
                    highscoreRecord[i].text = highscoreList[i].score.ToString("C0");
                    highscoreDonate[i].text = highscoreList[i].donate;
                    records[i] = highscoreList[i].score;
                    maxValue = records.Max();
                }
            }
        }

        public void ClearLeaderBoard()
        {
            StartCoroutine("SendServerCommandClearLeaderBoard");
            for (int i = 0; i < highscoreFields.Length; i++)
            {
                highscoreFields[i].text = i + 1 + ". Загрузка данных...";
                highscoreRecord[i].text = "";
                highscoreDonate[i].text = "";

            }
            DownloadHighscores();
        }

        //IEnumerator SendServerCommandClearLeaderBoard(){
        //	WWW www = new WWW(webURL + privateCode + "/clear/");
        //	yield return www;
        //}
    }

public struct Highscore_FortBoyard
{
	public string username;
	public int score;
    public string donate;

	public Highscore_FortBoyard(string _username, int _score, string _donate) {
		username = _username;
		score = _score;
        donate = _donate;
	}
}