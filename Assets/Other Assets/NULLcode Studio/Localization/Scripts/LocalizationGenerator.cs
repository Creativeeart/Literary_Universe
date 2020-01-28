/**************************************************************************/
/** 	© 2016 NULLcode Studio. License: CC 0.
/** 	Разработано специально для http://null-code.ru/
/** 	WebMoney: R209469863836. Z126797238132, E274925448496, U157628274347
/** 	Яндекс.Деньги: 410011769316504
/**************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;

// инструмент для создания файла локали и определения массива, используется только в редакторе
public class LocalizationGenerator {

	public static LocalizationComponent[] GenerateLocale(Canvas[] canvas)
	{
		if(canvas.Length == 0)
		{
			Debug.Log(" неопределен массив Canvas.");
			return null;
		}

		string path = Application.dataPath + "/Resources/Localization/Default.xml";

		List<LocalizationComponent> list = new List<LocalizationComponent>();

		foreach(Canvas target in canvas)
		{
			if(target)
			{
				LocalizationComponent[] comp = target.GetComponentsInChildren<LocalizationComponent>();
				foreach(LocalizationComponent c in comp)
				{
					c.SetComponent();
					if (c.target) {
						list.Add (c);
					}
				}	
			}
		}

		if(list.Count == 0)
		{
			Debug.Log(" указанный Canvas, не содержит дочернего компонента LocalizationComponent.");
			return null;
		}

		LocalizationComponent[] copy = list.ToArray();

		// раздувать файл одинаковыми текстами нет смысла, поэтому
		// убираем из массива элементы с одинаковым хеш кодом
		// из этого получаем новый массив и его сохраняем
		LocalizationComponent[] result = list.Distinct(new HashComparer()).ToArray(); 

		XmlNode userNode;
		XmlAttribute attribute;
		XmlDocument xmlDoc = new XmlDocument();
		XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
		xmlDoc.AppendChild(declaration);
		XmlNode rootNode = xmlDoc.CreateElement("locale");
		xmlDoc.AppendChild(rootNode);

		for(int i = 0; i < result.Length; i++)
		{
			userNode = xmlDoc.CreateElement("content");
			userNode.InnerText = result[i].target.text;

			attribute = xmlDoc.CreateAttribute("id");
			attribute.Value = result[i].id.ToString();
			userNode.Attributes.Append(attribute);
			rootNode.AppendChild(userNode);
		}

		xmlDoc.Save(path);

		Debug.Log(" создан фаил локали: " + path);

		return copy;
	}
}

class HashComparer : IEqualityComparer<LocalizationComponent>
{
	public bool Equals(LocalizationComponent x, LocalizationComponent y)
	{
		return x.id == y.id;
	}

	public int GetHashCode(LocalizationComponent obj)
	{
		return obj.id;
	}
}