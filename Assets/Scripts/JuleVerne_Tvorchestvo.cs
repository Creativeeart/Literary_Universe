using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.IO;
using System.Xml;
using DG.Tweening;

public class JuleVerne_Tvorchestvo : MonoBehaviour {
    public JV_Tvorchestvo[] JV_Tvorchestvo;
    public string fileName = "ArchivesJuleVerne";
	[Header("Блоки")]
	public GameObject Login;
    public GameObject InfoMission;
	public GameObject Select;
	public GameObject Game;
    public GameObject Final;
	public GameObject Archives;

    [Header("Выходные данные")]
    public TMP_InputField userName_InputField; //Имя пользователя
    public string currentUserName; //Текущий пользователь
    public TextMeshProUGUI welcomeName_TextMeshPro; //Текст приветствия пользователя

    public TMP_InputField letterEntryForm_InputField; //Форма ввода письма
    public string currentLetter; //Текущее письмо

    public TextMeshProUGUI selectedAuthor_TextMeshPro; //Кому письмо
    public string currentSelectedAuthor; //Текуший автор
	

	[Header("Архив")]
	public TextMeshProUGUI archiveLoadXmlText;

    [Header("Прогресс бар")]
    public Image progressBarImageFilling;
    [Range(0, 100)] public int speed = 60;
    float currentPercent = 0.0f;
    float stepPercent = 0.0f;

    const string webURL = "http://literaryuniverse.unitycoding.ru";
    int statusRegistration = 0;  //0 = ошибка регистрации; 1 = успешная регистрация; 2 = имя персонажа занято;        3 = поля не могут быть пустыми;

    string nameXML;
    string selectedAuthorXML;
    string letterXML;
    public string totVal = string.Empty;
    bool internetConnection = true;
    private void Awake()
    {
        CheckInternet();
    }
    void Start()
    {
        if (!File.Exists(Application.dataPath + "/" + fileName + ".xml"))
        {
            CreateXML();
        }
	}
    public void StartGame()
    {
        CheckInternet();
        if (internetConnection)
        {
            SendToSiteWithXML();
            CreateXML();
            LoadFromSite();
        }
        else
        {
            LoadXML();
        }
    }
    public void Update()
    {
        if (currentPercent <= stepPercent)
        {
            currentPercent += speed * Time.deltaTime;
        }
        if (currentPercent >= stepPercent)
        {
            currentPercent -= speed * Time.deltaTime;
        }
        progressBarImageFilling.fillAmount = currentPercent / 100;
        //textPercent.GetComponent<Text>().text = ((int)currentPercent).ToString("F0") + "%";
    }
    void MoveDoTween(GameObject otrMoveX, GameObject zeroMoveX)
    {
        DOTween.defaultEaseType = Ease.InOutBack;
        otrMoveX.GetComponent<RectTransform>().DOLocalMoveX(-1700, 1);
        zeroMoveX.GetComponent<RectTransform>().DOLocalMoveX(0, 1);
    }
	public void EnterNameForm()
    {
        currentUserName = userName_InputField.text;
		if (!string.IsNullOrEmpty(userName_InputField.text))
        {
            welcomeName_TextMeshPro.text = "Здравствуй " + "<color=#ff9600><b>" + currentUserName + "</b></color>" + "!";
            MoveDoTween(Login, InfoMission);
            stepPercent = 20.0f;
        } 
	}
    public void GoToInfoMission()
    {
        MoveDoTween(InfoMission, Select);
        stepPercent = 40.0f;
    }

    public void SelectMissionNext()
    {
        MoveDoTween(Select, Game);
        stepPercent = 60.0f;
    }
    public void SendAndGoToFinal() //Отправляем текст
    {
        currentLetter = letterEntryForm_InputField.text;
        currentSelectedAuthor = selectedAuthor_TextMeshPro.text;
        CheckInternet();
        AddXML ();
        if (internetConnection)
        {
            SendToSiteWithXML();
            CreateXML();
            LoadFromSite();
        }
        else
        {
            LoadXML();
        }
        MoveDoTween(Game, Final);
		letterEntryForm_InputField.text = string.Empty;
        stepPercent = 100.0f;
    }
    public void ReturnToMain()
    {
        MoveDoTween(Final, Login);
        stepPercent = 0.0f;
    }
    public void backLoginIsMission()
    {
        MoveDoTween(InfoMission, Login);
        stepPercent = 0.0f;
    }
    public void backLoginIsSelect()
    {
        MoveDoTween(Select, InfoMission);
        stepPercent = 20.0f;
    }
    public void backLoginIsGame()
    {
        MoveDoTween(Game, Select);
        stepPercent = 40.0f;
    }
    public void SelectMission(string missionObject)
    {
        selectedAuthor_TextMeshPro.text = missionObject;
    }
    public void ExitGame()
    {
        selectedAuthor_TextMeshPro.text = string.Empty;
        letterEntryForm_InputField.text = string.Empty;
        Select.SetActive(false);
        Game.SetActive(false);
        Login.SetActive(true);
    }
    public void LoadXMLbutton()
    {
        CheckInternet();
        if (internetConnection)
        {
            SendToSiteWithXML();
            CreateXML();
            LoadFromSite();
        }
        else
        {
            LoadXML();
        }
        Final.GetComponent<RectTransform>().DOLocalMoveX(-1700, 1);
        Login.GetComponent<RectTransform>().DOLocalMoveX(-1700, 1);
        Archives.GetComponent<RectTransform>().DOLocalMoveX(0, 1);
        stepPercent = 0.0f;
    }
    public void CloseArchive()
    {
        MoveDoTween(Archives, Login);
    }
    void CreateXML() // СОЗДАНИЕ XML ФАЙЛА
	{
		var doc = new XmlDocument();
		var rootNode = doc.CreateElement ("Archives");
		doc.AppendChild (rootNode);
		doc.Save(Application.dataPath + "/" + fileName + ".xml");
	}
    void AddXML() // СОХРАНЕНИЕ В XML БАЗУ НА ПК
    {
		var doc = new XmlDocument ();
		doc.Load (Application.dataPath + "/" + fileName + ".xml");
            //Создаем элемент записи пользователей.
            var user = doc.CreateElement("User");
            //Создаем элемент с именем пользователя
            var attr = doc.CreateAttribute("name");
            //Значение берется из поля ввода имени
            attr.Value = userName_InputField.text.ToString();
            //Добавляем запись
            user.Attributes.Append(attr);
            var currentAuthor = doc.CreateElement("Author");
            currentAuthor.InnerText = currentSelectedAuthor;
            user.AppendChild(currentAuthor);
            var text = doc.CreateElement("Text");
            text.InnerText = currentLetter;
            user.AppendChild(text);
            //Добавляем запись "Users"
            doc.DocumentElement.AppendChild(user);
		doc.Save(Application.dataPath + "/" + fileName + ".xml");

    }
    void LoadXML() //ЗАГРУЗКА ИЗ XML ФАЙЛА В UNITY 
    {
        totVal = string.Empty;
        XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load (Application.dataPath + "/" + fileName + ".xml");
		XmlElement xRoot = xmlDoc.DocumentElement;
		foreach(XmlNode xnode in xRoot)
		{
			// получаем атрибут name
			if(xnode.Attributes.Count > 0)
			{
				XmlNode attr = xnode.Attributes.GetNamedItem("name");
				if (attr!=null){
					totVal += "<color=#ff9600><b>Имя отправителя:</b></color> " + attr.Value +"\n";
				}
				// обходим все дочерние узлы элемента user
				foreach(XmlNode childnode in xnode.ChildNodes)
				{
                    if (childnode.Name == "Author")
                    {
                        totVal += "<color=#ff9600><b>Письмо для:</b></color> " + childnode.InnerText + "\n";
                    }
                    if (childnode.Name == "Text")
					{
						totVal += "<color=#ff9600><b>Текст письма:</b></color> " + childnode.InnerText +"\n";
					}
				}
				totVal += "\n";
				archiveLoadXmlText.text = totVal;
            }
		}
    }  
    public void SendToSiteWithXML() //ОТПРАВКА ИЗ XML ФАЙЛА В ИНТЕРНЕТ БАЗУ 
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/" + fileName + ".xml");
        XmlElement xRoot = xmlDoc.DocumentElement;
        foreach (XmlNode xnode in xRoot)
        {
            // получаем атрибут name
            if (xnode.Attributes.Count > 0)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("name");
                if (attr != null)
                {
                    nameXML = attr.Value;
                }
                // обходим все дочерние узлы элемента user
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "Author")
                    {
                        selectedAuthorXML = childnode.InnerText;
                    }
                    if (childnode.Name == "Text")
                    {
                        letterXML = childnode.InnerText;
                    }
                }
                StartCoroutine(SendToSite(nameXML, selectedAuthorXML, letterXML));
            }
        }
    }
    public void LoadFromSite()
    {
        StartCoroutine(LoadFromSiteIEnumerator());
    }
    IEnumerator LoadFromSiteIEnumerator() //ЗАГРУЗКА ИЗ ИНТЕРНЕТ БАЗЫ В XML 
    {
        WWW www = new WWW(webURL + "/JV_interaktiv_tvorchestvo.txt");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            FormatText(www.text);
            var doc = new XmlDocument();
            doc.Load(Application.dataPath + "/" + fileName + ".xml");
            for (int i = 0; i < JV_Tvorchestvo.Length; i++)
            {
                //Создаем элемент записи пользователей.
                var user = doc.CreateElement("User");
                //Создаем элемент с именем пользователя
                var attr = doc.CreateAttribute("name");
                //Значение берется из поля ввода имени
                attr.Value = JV_Tvorchestvo[i].username;
                //Добавляем запись
                user.Attributes.Append(attr);
                var currentAuthor = doc.CreateElement("Author");
                currentAuthor.InnerText = JV_Tvorchestvo[i].selectedAuthor;
                user.AppendChild(currentAuthor);
                var text = doc.CreateElement("Text");
                text.InnerText = JV_Tvorchestvo[i].letter;
                user.AppendChild(text);
                //Добавляем запись "Users"
                doc.DocumentElement.AppendChild(user);
            }
            doc.Save(Application.dataPath + "/" + fileName + ".xml");
            Debug.Log("База загружена");
            LoadXML();
        }
        else
        {
            Debug.Log("Ошибка Загрузки: " + www.error);
        }
    }
    public void CheckInternet()
    {
        StartCoroutine(CheckInternetConnection());
    }
    IEnumerator CheckInternetConnection()
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error == null)
        {
            internetConnection = true;
            Debug.Log("Интернет работает");
        }
        else
        {
            internetConnection = false;
            Debug.Log("Ошибка интернета");
        }
    }
    IEnumerator SendToSite(string userName, string selectedAuthor, string letter)
    {
        var Data = new WWWForm();
        Data.AddField("userName", userName);
        Data.AddField("selectedAuthor", selectedAuthor);
        Data.AddField("letter", letter);
        var www = new WWW(webURL + "/JV_Interaktiv_Tvorch.php", Data);
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
            switch (statusRegistration)
            {
                case 0:
                    //registrationErrorUI.text = "Ошибка регистрации";
                    break;
                case 1:
                    //registrationErrorUI.text = "Регистрация успешна";
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
    }
    void FormatText(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        JV_Tvorchestvo = new JV_Tvorchestvo[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            string selectedAuthor = entryInfo[1];
            string letter = entryInfo[2];
            JV_Tvorchestvo[i] = new JV_Tvorchestvo(username, selectedAuthor, letter);
        }
    }
}

//Запустили творчество: 
//если есть ИНТЕРНЕТ:{
//1. Отправить из XML файла в интернет базу;
//2. Очистили XML файл; 
//3. Загрузили из интернет базы в XML файл; 
//4. Из XML файла отобразили в UNITY
//}
//иначе если нет ИНТЕРНЕТА:{
//5. Из XML файла отобразили в UNITY
//}

//Если сохраняем результат пользователя (новые данные): 
//1. Сохранить в XML файл НА ПК; 
//если есть ИНТЕРНЕТ:{
//2. Отправить из XML файла в интернет базу; 
//3. Очистили XML файл; 
//4. Загрузили из интернет базы в XML файл;  
//5. Из XML файла отобразили в UNITY
//}
//иначе если нет ИНТЕРНЕТА:{
//6. Из XML файла отобразили в UNITY
//}

[System.Serializable]
public class JV_Tvorchestvo
{
    public string username;
    public string selectedAuthor;
    public string letter;

    public JV_Tvorchestvo(string _username, string _selectedAuthor, string _letter)
    {
        username = _username;
        selectedAuthor = _selectedAuthor;
        letter = _letter;
    }
}