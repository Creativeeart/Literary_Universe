using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.IO;
using System.Xml;

public class GameFinishSentence : MonoBehaviour {
	[Header("Имя Игрока")]
	public string fileName = "Archives";
	public InputField enterName;
	public TextMeshProUGUI NamePlayer;
	[Header("Блоки")]
	public GameObject Login;
	public GameObject Game;
	public GameObject Archives;
	[Header("Игра")]
	public TextMeshProUGUI counterText;
	public int countSentence;
	public TextMeshProUGUI buttonNextText;
	public TextMeshProUGUI uiText;
	public TextMeshProUGUI sentenceText; //Форма где будут показываться предложения
	public SentenceList[] sentences; //Массив с обрывками предложений
	public InputField inputSentence;
	public TextMeshProUGUI MergedSentenceText; //Форма где будут показываться предложения уже соединенные
	public List <string> mergedTextList;
	List<object> sentencesList;
	SentenceList currentSentences;
	int randSentence;


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
			Game.SetActive (true);
		}
	}

	public void OnClickPlay(){
		MergedSentenceText.text = "";
		sentencesList = new List<object> (sentences);
		SentenceGenerate ();
		CounterSentence ();
	}
	void SentenceGenerate(){
		if (sentencesList.Count > 0) {
			randSentence = Random.Range (0, sentencesList.Count);
			currentSentences = sentencesList [randSentence] as SentenceList;
			sentenceText.text = currentSentences.sentences;

		} 

	}
	public void NextSentence(){
		if (sentencesList.Count > 0) {
			countSentence++;
			mergedTextList.Add (sentenceText.text + "\n" + "<b><color=#ffa500ff>Ответ:</color></b> " + inputSentence.text);
			sentencesList.RemoveAt (randSentence);
			if (inputSentence != null) {
				if (MergedSentenceText.text == "") {
					MergedSentenceText.text += sentenceText.text + "\n" + "<b><color=#ffa500ff>Ответ:</color></b> " + inputSentence.text;
				} else {
					MergedSentenceText.text += "\n" + sentenceText.text + "\n" + "<b><color=#ffa500ff>Ответ:</color></b> " + inputSentence.text;
				}
				inputSentence.text = "";
			}
			SentenceGenerate ();
		}
		if (mergedTextList.Count == sentences.Length-1 ) {
			buttonNextText.text = "Сохранить и выйти";

		}
		if (sentencesList.Count == 0){
			ExitGame ();
		}
		CounterSentence ();
	}
	public void CounterSentence(){
		counterText.text = "Предложение: <b><color=#ffa500ff>" + countSentence + "</color></b> из <b><color=#ffa500ff>" + sentences.Length + "</color></b>. ";
	}
	public void ExitGame(){
		buttonNextText.text = "Принять и далее";
		AddXML ();
		mergedTextList.Clear ();
		Login.SetActive (true);
		Game.SetActive (false);
		countSentence = 1;
	}
	public void LoadXMLbutton(){
		LoadXML ();
		Login.SetActive (false);
		Game.SetActive (false);
		Archives.SetActive (true);
	}
	public void CloseArchive(){
		Login.SetActive (true);
		Game.SetActive (false);
		Archives.SetActive (false);
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
			//Берем из массива mergedTextList, данные по очереди  и добавлем запись
			for (int i = 0; i < mergedTextList.Count; i++) {
				var text = doc.CreateElement ("Text");
				text.InnerText = mergedTextList [i];
				user.AppendChild (text);
			}
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
				uiText.text = totVal;
			}
		}
	}

}
[System.Serializable]
public class SentenceList{
	public string sentences;
}
