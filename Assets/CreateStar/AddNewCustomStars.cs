using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Xml;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace cakeslice
{
    public class AddNewCustomStars : MonoBehaviour
    {
        public GameObject loginAndRegPanel;
        public CustomStars[] CustomStars;
        public string fileName = "CustomStars";

        [Header("Поля для регистрации")]
        public TMP_InputField registrationFioUI;
        public TMP_InputField registrationAgeUI;
        public TMP_InputField registrationActivityUI;
        public TMP_InputField registrationFavorite_BookUI;
        public TMP_InputField registrationAboutUI;

        string fioXML;
        string ageXML;
        string activityXML;
        string favorite_bookXML;
        string aboutXML;
        public GameObject customStar;

        readonly string url = "http://literaryuniverse.unitycoding.ru";
        bool internetConnection = false;
        GameObject[] customStars;
        public int gridX = 3;
        public int gridY = 3;
        public int gridZ = 3;
        public Vector3 space;
        public int summ;
        public float time = 5f;
        public float timeCreationStars = 0.3f;
        public bool timeRun = true;
        float tempTime = 0;
        public int localCurrentStarsCount = 0;
        public int internetCurrentStarsCount = 0;
        public GameObject synch_icon;
        //public Animator synch_animator;
        SupportScripts _supportScripts;

        void Start()
        {
            if (GameObject.Find("SupportSripts"))
            {
                _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
            }
            else
            {
                Debug.Log("SupportSripts - не найден либо отключен!");
                return;
            }
            tempTime = time;
            if (!File.Exists(Application.dataPath + "/" + fileName + ".xml"))
            {
                CreateXML();
            }
            CheckInternet();
        }
        void Update()
        {
            if (timeRun)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    time = tempTime;
                    localCurrentStarsCount = CustomStars.Length;
                    StartCoroutine(CounterSite());
                }
            }
            else
            {
                time = tempTime;
            }
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void Synch()
        {
            CheckInternet();
        }
        IEnumerator Reload()
        {
            yield return new WaitForSeconds(1);
            CheckInternet();
        }

        public void CreateCustomStar() //Создание звезды, и занесение в базу
        {
            if (registrationFioUI.text != string.Empty && registrationAgeUI.text != string.Empty)
            {
                AddXML();
                CheckInternet();
                StartCoroutine(Reload());
            }
        }

        void CreateXML() // СОЗДАНИЕ XML ФАЙЛА
        {
            var doc = new XmlDocument();
            var rootNode = doc.CreateElement("Archives");
            doc.AppendChild(rootNode);
            doc.Save(Application.dataPath + "/" + fileName + ".xml");
        }
        void AddXML() // СОХРАНЕНИЕ В XML БАЗУ НА ПК
        {
            var doc = new XmlDocument();
            doc.Load(Application.dataPath + "/" + fileName + ".xml");
            //Создаем элемент записи пользователей.
            var user = doc.CreateElement("User");
            //Создаем элемент с именем пользователя
            var fio = doc.CreateAttribute("fio");
            //Значение берется из поля ввода имени
            fio.Value = registrationFioUI.text.ToString();
            //Добавляем запись
            user.Attributes.Append(fio);

            var age = doc.CreateElement("Age");
            age.InnerText = registrationAgeUI.text;
            user.AppendChild(age);

            var activity = doc.CreateElement("Activity");
            activity.InnerText = registrationActivityUI.text;
            user.AppendChild(activity);

            var favorite_book = doc.CreateElement("Favorite_book");
            favorite_book.InnerText = registrationFavorite_BookUI.text;
            user.AppendChild(favorite_book);

            var about = doc.CreateElement("About");
            about.InnerText = registrationAboutUI.text;
            user.AppendChild(about);

            //Добавляем запись "Users"
            doc.DocumentElement.AppendChild(user);
            doc.Save(Application.dataPath + "/" + fileName + ".xml");
        }
        void StarsFormation() //ФОРМИРОВАНИЕ И СОЗДАНИЕ ЗВЕЗД НА СЦЕНЕ
        {
            customStars = GameObject.FindGameObjectsWithTag("CustomStar");
            for (int i = 0; i < customStars.Length; i++)
            {
                Destroy(customStars[i]);
            }
            StartCoroutine(StarsFormationInTime(timeCreationStars));
            
        }
        IEnumerator StarsFormationInTime(float time)
        {
            summ = 0;
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    for (int k = 0; k < gridZ; k++)
                    {
                        var ins = Instantiate(customStar, new Vector3(i * space.x, j * space.y, k * space.z), Quaternion.identity);
                        ins.transform.position = new Vector3(ins.transform.position.x - space.x, ins.transform.position.y - space.y, ins.transform.position.z - space.z); //Центровка общего куба
                        ins.GetComponent<CheckContactStars>().fio = CustomStars[summ].fio;
                        ins.GetComponent<CheckContactStars>().age = CustomStars[summ].age;
                        ins.GetComponent<CheckContactStars>().activity = CustomStars[summ].activity;
                        ins.GetComponent<CheckContactStars>().favorite_book = CustomStars[summ].favorite_book;
                        ins.GetComponent<CheckContactStars>().about = CustomStars[summ].about;
                        summ++;
                        yield return new WaitForSeconds(time);
                        if (summ >= CustomStars.Length) break;
                    }
                    if (summ >= CustomStars.Length) break;
                }
                if (summ >= CustomStars.Length) break;
                
            }
            
        }
        void LoadXML() //ЗАГРУЗКА ИЗ XML ФАЙЛА В UNITY 
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.dataPath + "/" + fileName + ".xml");
            XmlElement xRoot = xmlDoc.DocumentElement;
            int elementsInXML = 0;
            foreach (XmlNode xnode in xRoot)
            {
                elementsInXML++;
            }
            CustomStars = new CustomStars[elementsInXML];
            int count = -1;
            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибут name
                if (xnode.Attributes.Count > 0)
                {
                    count++;
                    XmlNode attr = xnode.Attributes.GetNamedItem("fio");
                    if (attr != null)
                    {
                        fioXML = attr.Value;
                    }
                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "Age")
                        {
                            ageXML = childnode.InnerText;
                        }
                        if (childnode.Name == "Activity")
                        {
                            activityXML = childnode.InnerText;
                        }
                        if (childnode.Name == "Favorite_book")
                        {
                            favorite_bookXML = childnode.InnerText;
                        }
                        if (childnode.Name == "About")
                        {
                            aboutXML = childnode.InnerText;
                        }
                    }
                    CustomStars[count] = new CustomStars(fioXML, int.Parse(ageXML), activityXML, favorite_bookXML, aboutXML);
                }
            }
            StarsFormation();
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
                    XmlNode attr = xnode.Attributes.GetNamedItem("fio");
                    if (attr != null)
                    {
                        fioXML = attr.Value;
                    }
                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "Age")
                        {
                            ageXML = childnode.InnerText;
                        }
                        if (childnode.Name == "Activity")
                        {
                            activityXML = childnode.InnerText;
                        }
                        if (childnode.Name == "Favorite_book")
                        {
                            favorite_bookXML = childnode.InnerText;
                        }
                        if (childnode.Name == "About")
                        {
                            aboutXML = childnode.InnerText;
                        }
                    }
                    StartCoroutine(SendToSite(fioXML, ageXML, activityXML, favorite_bookXML, aboutXML));
                }
            }
        }
        public void LoadFromSite()
        {
            StartCoroutine(LoadFromSiteIEnumerator());
        }
        IEnumerator LoadFromSiteIEnumerator() //ЗАГРУЗКА ИЗ ИНТЕРНЕТ БАЗЫ В XML 
        {
            WWW www = new WWW(url + "/custom_stars_lists.txt");
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                FormatText(www.text);
                var doc = new XmlDocument();
                doc.Load(Application.dataPath + "/" + fileName + ".xml");
                for (int i = 0; i < CustomStars.Length; i++)
                {
                    //Создаем элемент записи пользователей.
                    XmlElement user = doc.CreateElement("User");
                    //Создаем элемент с именем пользователя
                    XmlAttribute attr = doc.CreateAttribute("fio");
                    //Значение берется из поля ввода имени
                    attr.Value = CustomStars[i].fio;
                    //Добавляем запись
                    user.Attributes.Append(attr);

                    XmlElement age = doc.CreateElement("Age");
                    age.InnerText = CustomStars[i].age.ToString();
                    user.AppendChild(age);

                    XmlElement activity = doc.CreateElement("Activity");
                    activity.InnerText = CustomStars[i].activity;
                    user.AppendChild(activity);

                    XmlElement favorite_book = doc.CreateElement("Favorite_book");
                    favorite_book.InnerText = CustomStars[i].favorite_book;
                    user.AppendChild(favorite_book);

                    XmlElement about = doc.CreateElement("About");
                    about.InnerText = CustomStars[i].about;
                    user.AppendChild(about);

                    //Добавляем запись "Users"
                    doc.DocumentElement.AppendChild(user);
                }
                doc.Save(Application.dataPath + "/" + fileName + ".xml");
                StarsFormation();
                loginAndRegPanel.SetActive(false);
                _supportScripts.UIControllerCloseOverlayUI();
                Debug.Log("База загружена");
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
                SendToSiteWithXML();
                Debug.Log("SendToSiteWithXML");
                CreateXML();
                Debug.Log("CreateXML");
                LoadFromSite();
                Debug.Log("LoadFromSite");
                timeRun = true;
            }
            else
            {
                internetConnection = false;
                Debug.Log("Ошибка интернета");
                LoadXML();
                Debug.Log("LoadXML");
            }
        }
        IEnumerator SendToSite(string fio, string age, string activity, string favorite_book, string about)
        {
            var Data = new WWWForm();
            Data.AddField("fio", fio);
            Data.AddField("age", int.Parse(age));
            Data.AddField("activity", activity);

            Data.AddField("favorite_book", favorite_book);
            Data.AddField("about", about.Replace('\n', ' '));

            var Query = new WWW(url + "/add_custom_stars.php", Data);
            yield return Query;
            if (Query.error != null)
            {
                Debug.Log("Сервер не отвечает: " + Query.error);
            }
            else
            {
                Debug.Log("Сервер ответил: " + Query.text);
            }
            Query.Dispose();
        }
        void FormatText(string textStream)
        {
            string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            CustomStars = new CustomStars[entries.Length];
            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string fio = entryInfo[0];
                int age = int.Parse(entryInfo[1]);
                string activity = entryInfo[2];
                string favorite_book = entryInfo[3];
                string about = entryInfo[4];
                CustomStars[i] = new CustomStars(fio, age, activity, favorite_book, about);
            }
        }
        IEnumerator CounterSite() //ПОДСЧЕТ КОЛ-ВА СТРОК ИЗ ИНТЕРНЕТ БАЗЫ
        {
            WWW www = new WWW(url + "/custom_stars_lists.txt");
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                string[] entries = www.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                internetCurrentStarsCount = entries.Length;
                Debug.Log("База загружена");
                if (localCurrentStarsCount != internetCurrentStarsCount)
                {
                    timeRun = false;
                    Debug.Log("(!)Обнаружено расхождение");
                    synch_icon.SetActive(true);
                    StartCoroutine(Reload());
                }
                else
                {
                    timeRun = true;
                    Debug.Log("Расхождение не обнаружено.");
                    synch_icon.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Ошибка Загрузки: " + www.error);
            }
        }
    }

    [Serializable]
    public class CustomStars
    {
        public string fio;
        public int age;
        public string activity;
        public string favorite_book;
        public string about;

        public CustomStars(string _fio, int _age, string _activity, string _favorite_book, string _about)
        {
            fio = _fio;
            age = _age;
            activity = _activity;
            favorite_book = _favorite_book;
            about = _about;
        }
    }
}