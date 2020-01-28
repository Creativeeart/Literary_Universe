

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class Localization : MonoBehaviour {

	// обязательная сериализация даных полей
	[SerializeField] private Canvas[] canvas; // здесь указываем Canvas, для дочерних текстовых элементов которых, предусмотрена локаль
	[SerializeField] private LocalizationComponent[] source; // базовый массив, заполняется автоматически, после генерирования локали

	private TextAsset[] XMLFiles;
	private static Localization _internal;

	public static Localization Internal
	{
		get{ return _internal; }
	}

	void Awake()
	{
		_internal = this;
		StartScene();
	}
		

	void StartScene()
	{
		Load(); // загружаем все локали в массив
	}

	void Load()
	{
		XMLFiles = Resources.LoadAll<TextAsset>("Localization"); // папка в Resources, где лежат локали

		if(XMLFiles.Length == 0)
		{
			Debug.Log(this + " файлы не обнаружены.");
			return;
		}
			

		Locale();
	}
		

	int GetInt(string text)
	{
		int value;
		if(int.TryParse(text, out value)) return value;
		return 0;
	}

	void InnerText(int id, string text)
	{
		foreach(LocalizationComponent t in source)
		{
			if(t.id == id) t.target.text = text;
		}
	}

	void Locale() // чтение XML
	{
		XmlTextReader reader = new XmlTextReader(new StringReader(XMLFiles[0].text));
		while(reader.Read())
		{
			if (reader.IsStartElement ("content")) {
				InnerText (GetInt (reader.GetAttribute ("id")), reader.ReadString ());
			}	
		}
		reader.Close();
	}
		
	public void SetComponents() // создание шаблона локали, используется только в редакторе
	{
		source = LocalizationGenerator.GenerateLocale(canvas);
	}
}