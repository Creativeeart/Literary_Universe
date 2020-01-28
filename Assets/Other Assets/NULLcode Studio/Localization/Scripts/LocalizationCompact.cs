using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using System;
using System.Collections.Generic;
using System.Globalization;

public class LocalizationCompact : MonoBehaviour {

	public TextMeshProUGUI[] elements; // все текстовые элементы интерфейса, для которых предусмотрен перевод
	public string path; // путь где будут все локали

	public int[] idList; // создается/обновляется вместе с шаблоном языка
	public List<int> idXmlList;
	public List<int> summID;

	private string[] fileList;
	private string locale;

	void Awake()
	{
		path = Application.dataPath + "/Resources/Localization";
//		if (!File.Exists (path + "/Default.xml")) {
//			CreateXML ();
//		}
		GetXmlID ();
		LoadLocale(); // создание списка всех доступных локалей
		SummID();
	}

	// создание массива id значений, относительно текстовых элементов
	// одинаковым текстовым элементам, будет присвоен одинаковый id
	void GetID()
	{
		int i = 1;
		idList = new int[elements.Length];
		for(int j = 0; j < elements.Length; j++)
		{
			if(idList[j] == 0)
			{
				string key = elements[j].text;
				idList[j] = i;
				for(int t = j + 1; t < elements.Length; t++)
				{
					if(elements[t].text.CompareTo(key) == 0)
					{
						idList[t] = i;
					}
				}
				i++;
			}
		}

	}
	void GetXmlID(){
		idXmlList.Clear ();
		string file = path + "/Default.xml"; // имя XML
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load (file);
		XmlNodeList elemList = xmlDoc.GetElementsByTagName("content");
		for (int i = 0; i < elemList.Count; i++)
		{   
			idXmlList.Add(GetInt(elemList[i].Attributes["id"].Value));
		}
	}

	void SummID(){
		List<int> list = new List<int>();
		list.AddRange(idList);
		list.AddRange(idXmlList);
		var distinct = list.GroupBy(x=>x).Where(y=>y.Count()==1).Select(z=>z.Key);
		summID.AddRange (distinct);

//		for (int i = 0; i < summID.Count; i++)
//		{
//			Debug.Log(summID[i] + " summ");
//		}
//		System.Collections.ArrayList unique = new System.Collections.ArrayList();
//
//		for (int i = 0; i < summID.Count; i++)
//			if (unique.IndexOf(summID[i]) == -1)
//				unique.Add(summID[i]);
//		for (int i = 0; i < unique.Count; i++) {
//			Debug.Log (unique [i] + " unique");
//		}
	}	

	void SetData(string value)
	{
		Dropdown.OptionData option = new Dropdown.OptionData();
		option.text = Path.GetFileNameWithoutExtension(value);

	}

	public void LoadLocale() 
	{
		if(!Directory.Exists(path))
		{
			SetData("none");
			return;
		}

		fileList = Directory.GetFiles(path);

		if(fileList.Length == 0)
		{
			SetData("none");
			return;
		}

		for(int i = 0; i < fileList.Length; i++)
		{
			SetData(fileList[i]);
		}
			
		SetLocale();
	}
	void CreateXML()
	{
		string file = path + "/Default.xml"; // имя XML
		XmlDocument xmlDoc = new XmlDocument();
		XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
		xmlDoc.AppendChild(declaration);
		XmlNode rootNode = xmlDoc.CreateElement("locale");
		xmlDoc.AppendChild(rootNode);
		xmlDoc.Save(file);
	}
	public void AddXML() // для редактора, создание/обновление локали по умолчанию
	{
		if (!File.Exists (path + "/Default.xml"))
		{
			Debug.LogWarning(this + " Путь указан не верно!");
			return;
		}
		GetID();
		string file = path + "/Default.xml"; // имя XML

		string[] arr = new string[elements.Length];
		for(int i = 0; i < summID.Count; i++)
		{
			arr[i] = elements[summID[i]-1].text; // копируем все текстовые элементы
			Debug.Log(arr[i]);
		}

//		int Count = 0;
//		for (int i = 0; i < idList.Length; i++)
//		{
//			for (int j = 0; j < idXmlList.Count; j++)
//			{
//				if (idList [i] == idXmlList [j]) {
//					Count++;
//				}
//			}
//			Debug.LogFormat("Элемент массива idList: '{0}' повторяются в массиве idXmlList {1} раз", idList[i], Count);
//			Count = 0;
//
//		}

		string[] res_txt = arr.Distinct().ToArray(); // убираем повторяющийся текст
		int[] res_id = idList.Distinct().ToArray(); //  список id


		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load (file);
		XmlNode userNode;
		XmlAttribute attribute;

		for (int k = 0; k < res_txt.Length; k++) {
			userNode = xmlDoc.CreateElement ("content");
			userNode.InnerText = res_txt [k];
			attribute = xmlDoc.CreateAttribute ("id");
			attribute.Value = res_id [k].ToString ();
			userNode.Attributes.Append (attribute);
			xmlDoc.DocumentElement.AppendChild (userNode);
		}
		xmlDoc.Save (file);



		/////////////////////
//		XmlNode userNode;
//		XmlAttribute attribute;
//		XmlDocument xmlDoc = new XmlDocument();
//		XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
//		xmlDoc.AppendChild(declaration);
//		XmlNode rootNode = xmlDoc.CreateElement("locale");
//		xmlDoc.AppendChild(rootNode);
//
//		for(int i = 0; i < res_txt.Length; i++)
//		{
//			userNode = xmlDoc.CreateElement("content");
//			userNode.InnerText = res_txt [i];
//
//			attribute = xmlDoc.CreateAttribute("id");
//			attribute.Value = res_id[i].ToString();
//			userNode.Attributes.Append(attribute);
//			rootNode.AppendChild(userNode);
//		}
//
//		xmlDoc.Save(file);
		////////////////////

		Debug.Log("Файл обновлен");			

	}
	
	int GetInt(string text)
	{
		int value;
		if(int.TryParse(text, out value)) return value;
		return 0;
	}

	void SetLocale() // чтение файла и замена текста в сцене
	{
		locale = fileList[0];

		try
		{
			XmlTextReader reader = new XmlTextReader(locale);
			while(reader.Read())
			{
				if (reader.IsStartElement ("content")) {
					ReplaceText (GetInt (reader.GetAttribute ("id")), reader.ReadString ());
				}	
			}
			reader.Close();
		}
		catch(System.Exception)
		{
			Debug.LogError(this + " Ошибка чтения файла! --> " + locale);
		}
	}

	void ReplaceText(int id, string text) // поиск и замена всех элементов, по ключу
	{
		for(int j = 0; j < idList.Length; j++)
		{
			
			if(idList[j] == id) elements[j].text = text;
		}
	}
	string uniqueID(){
		DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
		int z1 = UnityEngine.Random.Range (0, 1000000);
		int z2 = UnityEngine.Random.Range (0, 1000000);
		string uid = currentEpochTime + ":" + z1 + ":" + z2;
		return uid;
	}
}
