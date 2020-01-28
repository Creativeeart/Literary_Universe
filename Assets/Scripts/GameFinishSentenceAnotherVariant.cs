using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.IO;
using System.Xml;

public class GameFinishSentenceAnotherVariant : MonoBehaviour {
	public string fileName = "ArchivesJuleVerne";
	string MergedSentenceText; //Форма где будут показываться предложения уже соединенные
	[Header("Блоки")]
	public GameObject Login;
	public GameObject Select;
	public GameObject Game;
	public GameObject Archives;
	[Header("Логин")]
	public InputField enterName;
	[Header("Игра")]
	public TextMeshProUGUI sentenceText; //Форма где будут показываться предложения
	public InputField inputSentence;
	[Header("Выбор миссии")]
	public TextMeshProUGUI NamePlayer;
	[Header("Архив")]
	public TextMeshProUGUI archiveLoadXmlText;

	void Start(){
		if (!File.Exists (Application.dataPath + "/" + fileName + ".xml")) {
			CreateXML ();
		}
	}
	public void EnterNameForm(){
		string username = enterName.text;
		if (enterName.text == "") {
			Debug.Log ("Пожалуйста введит ваше имя");
		} else {
			NamePlayer.text = "Здравствуй " + "<b><color=#ffa500ff>" + enterName.text + "</color></b>" + "!";
			Login.SetActive (false);
			Select.SetActive (true);
		}
	}

	public void SelectMission(TextMeshProUGUI missionObject){
		sentenceText.text = missionObject.text;
		MergedSentenceText = "";
		Game.SetActive (true);
		Select.SetActive (false);
	}
	public void SendAndExit(){
		MergedSentenceText= sentenceText.text + "\n" + "<b><color=#ffa500ff>Ответ:</color></b> " + inputSentence.text;
		AddXML ();
		Login.SetActive (true);
		Select.SetActive (false);
		Game.SetActive (false);
		sentenceText.text = "";
		inputSentence.text = "";
	}
	public void ExitGame(){
		MergedSentenceText = "";
		sentenceText.text = "";
		inputSentence.text = "";
		Select.SetActive (false);
		Game.SetActive (false);
		Login.SetActive (true);
	}
	public void LoadXMLbutton(){
		LoadXML ();
		Login.SetActive (false);
		Game.SetActive (false);
		Select.SetActive (false);
		Archives.SetActive (true);
	}
	public void CloseArchive(){
		Login.SetActive (true);
		Game.SetActive (false);
		Archives.SetActive (false);
		Select.SetActive (false);
	}
	void CreateXML()
	{
		var doc = new XmlDocument();
		var rootNode = doc.CreateElement ("Archives");
		doc.AppendChild (rootNode);
		doc.Save(Application.dataPath + "/" + fileName + ".xml");
	}
	void AddXML(){
		var doc = new XmlDocument ();
		doc.Load (Application.dataPath + "/" + fileName + ".xml");
			//Создаем элемент записи пользователей.
			var user = doc.CreateElement ("User");
			//Создаем элемент с именем пользователя
			var attr = doc.CreateAttribute ("name");
			//Значение берется из поля ввода имени
			attr.Value = enterName.text.ToString ();
			//Добавляем запись
			user.Attributes.Append (attr);
			var text = doc.CreateElement ("Text");
			text.InnerText = MergedSentenceText;
			user.AppendChild (text);
			//Добавляем запись "Users"
			doc.DocumentElement.AppendChild (user);
		doc.Save(Application.dataPath + "/" + fileName + ".xml");
	}

	void LoadXML(){
		string totVal = "";
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
					totVal += "<b><color=#ffa500ff>Имя пользователя:</color></b> "+attr.Value +"\n";
				}
				var i = 0;
				// обходим все дочерние узлы элемента user
				foreach(XmlNode childnode in xnode.ChildNodes)
				{
					i++;
					if(childnode.Name == "Text")
					{
						totVal += "<b><color=#ffa500ff>Предложение " + i + ":</color></b> " +childnode.InnerText +"\n";
					}
				}
				totVal += "\n";
				archiveLoadXmlText.text = totVal;
			}
		}
	}

}