using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using System.Linq;
using cakeslice;
    public class Highscores_FortBoyard : MonoBehaviour
    {
        public GameObject loginFormUI, inputField;
        public GameObject CurrentFormUI;
    public TextMeshProUGUI CurrentNameUI_TextMeshProUGUI;
        public TextMeshProUGUI[] highscoreFields, highscoreRecord, highscoreDonate;
        public TMP_InputField registrationRealnameUI;
    
        //const string privateCode = "skTTCvwFf0-wg9Q_TJ-_Wws59uWyamAU6na3BTMSXjYg";
        //const string publicCode = "5d416c3a76827f1758c4b7da";
        //const string webURL = "http://dreamlo.com/lb/";
        const string webURL = "http://literaryuniverse.unitycoding.ru";
        int[] records;
        int statusRegistration = 0;  //0 = ошибка регистрации; 1 = успешная регистрация; 2 = имя персонажа занято;        3 = поля не могут быть пустыми;
        //string text;
        public int maxValue;
    public bool isInternetConnection = false;

    public static Highscores_FortBoyard Instance { get; private set; }
    [SerializeField]
    public Highscore_FortBoyard[] highscoresList;
    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(CheckInternetConnection());
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = "-";
            highscoreRecord[i].text = "-";
            highscoreDonate[i].text = "-";
        }
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
    IEnumerator CheckInternetConnection()
    {
        while (true)
        {
            WWW www = new WWW("http://google.com");
            yield return www;
            if (www.error == null)
            {
                isInternetConnection = true;
                Debug.Log("Интернет работает");
            }
            else
            {
                isInternetConnection = false;
                Debug.Log("Интернет НЕ работает");
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void SwitchName()
    {
        if (isInternetConnection)
        {
            SupportScripts.Instance._authorization.SwitchName();
            CurrentFormUI.SetActive(false);
            loginFormUI.SetActive(true);
        }
        else
        {
            Debug.Log("Сменить имя возможно только при наличии интернета");
        }
    }
    

    public void LaunchGame()
    {
        //if (SupportScripts.Instance._authorization.currentUser == "Guest")
        //{
        //    SupportScripts.Instance._authorization.OpenGuestForm();
        //}
        //else
        //{
            CurrentNameUI_TextMeshProUGUI.text = SupportScripts.Instance._authorization.currentRealName;
            CurrentFormUI.SetActive(true);
            //FortBoyardGameController.Instance.StartGame();
        //}
    }
    public void LaunchGame2()
    {
        FortBoyardGameController.Instance.StartGame();
        CurrentFormUI.SetActive(false);
    }
    
    public void EnterName()
    {
        if (isInternetConnection)
        {
            if (inputField.GetComponent<TMP_InputField>().text != string.Empty)
            {
                
                loginFormUI.SetActive(false);
                SupportScripts.Instance._authorization.registrationRealName = inputField.GetComponent<TMP_InputField>().text;
                SupportScripts.Instance._authorization.currentRealName = inputField.GetComponent<TMP_InputField>().text;

                SupportScripts.Instance._authorization.registrationUsername = SupportScripts.Instance._authorization.currentUser;
                SupportScripts.Instance._authorization.currentUser = SupportScripts.Instance._authorization.currentUser;

                SupportScripts.Instance._authorization.registrationPassword = "guest";
                SupportScripts.Instance._authorization.currentUserUI.text = "Вы вошли как: <b><i><color=#FF8E00FF><u>" + SupportScripts.Instance._authorization.currentUser + "</u></color></i></b>";
                SupportScripts.Instance._authorization.loginAndRegistrationButtons.SetActive(false);
                SupportScripts.Instance._authorization.exitButtons.SetActive(true);
                SupportScripts.Instance._authorization.SendRegistrationWithGuest();
                FortBoyardGameController.Instance.StartGame();
                //_supportScripts._authorization.CloseLoginAndRegistrationForm();
            }
        }
        else
        {

        }
    }

    public void AddNewHighscore()
    {
        StartCoroutine(UploadNewHighscore(
            SupportScripts.Instance._authorization.currentUser, 
            SupportScripts.Instance._authorization.currentRealName, 
            (int)TreasureCalculateZoneController.Instance.TotalCalculateCoins, 
            TreasureCalculateZoneController.Instance.DonationName)
            );
    }

    IEnumerator UploadNewHighscore(string username, string realname, int score, string donationName)
    {
        WWWForm Data = new WWWForm();
        Data.AddField("login", username);
        Data.AddField("realname", realname);
        Data.AddField("fb_score", score);
        Data.AddField("donation_name", donationName);
        WWW www = new WWW(webURL + "/highScoreAdds.php", Data);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("Сервер не отвечает: " + www.error);
        }
        else
        {
            Debug.Log("Сервер ответил: " + www.text);
        }
        www.Dispose();
    }


    public void DownloadHighscores() //Загрузка результатов в таблицу рекордов
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + "/scores.txt");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            OnHighscoresDownloaded(highscoresList);
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
        for (int i = 0; i < highscoresList.Length; i++)
        {
            if (SupportScripts.Instance._authorization.currentUser == highscoresList[i].username)
            {
                //CurrentRealName = highscoresList[i].realname;
                break;
            }
        }
    }
    public void OnHighscoresDownloaded(Highscore_FortBoyard[] highscoreList)
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            if (i < highscoreList.Length)
            {
                highscoreFields[i].text = highscoreList[i].realname;
                highscoreRecord[i].text = highscoreList[i].score.ToString("C0");
                highscoreDonate[i].text = highscoreList[i].donate;
                records[i] = highscoreList[i].score;
                maxValue = records.Max();
            }
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore_FortBoyard[entries.Length];
        records = new int[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            string realname = entryInfo[1];
            int score = int.Parse(entryInfo[2]);
            string donate = entryInfo[4];
            highscoresList[i] = new Highscore_FortBoyard(username, realname, score, donate);
        }
    }

}
[Serializable]
public struct Highscore_FortBoyard
{
	public string username;
    public string realname;
    public int score;
    public string donate;
    
	public Highscore_FortBoyard(string _username, string _realname, int _score, string _donate) {
		username = _username;
        realname = _realname;
        score = _score;
        donate = _donate;
	}
}

