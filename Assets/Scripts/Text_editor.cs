using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using TMPro;
using System.Security.Cryptography;
using System.Text;
public class Text_editor : MonoBehaviour {
	public TextMeshProUGUI currentTextMeshPro;
	public TextComponent _textComponent;
	public GameObject inputFieldModal;
	public InputField inputFieldText;
	public TextMeshProUGUI[] allInterface;
	public TextMeshProUGUI[] allContent;
	public bool useEncryptor;

	string FilePathInterface, FilePathContent;

	void Start () {
		FilePathInterface = Application.dataPath +  "/StreamingAssets/AllText/Interface/interface.xml";
		FilePathContent = Application.dataPath + "/StreamingAssets/AllText/Content/content.xml";
		InterfaceFormationXML ();
		ContentFormationXML ();
	}
	public static string Encrypt(string clearText)
	{
		string EncryptionKey = "abc123";
		byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
		using (Aes encryptor = Aes.Create())
		{
			Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
			encryptor.Key = pdb.GetBytes(32);
			encryptor.IV = pdb.GetBytes(16);
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cs.Write(clearBytes, 0, clearBytes.Length);
					cs.Close();
				}
				clearText = System.Convert.ToBase64String(ms.ToArray());
			}
		}
		return clearText;
	}




	public void InterfaceFormationXML(){ //Формируется список, если узел уже есть то не будет создан, а лишь добавлен новый
		XmlDocument XmlDoc = new XmlDocument();
		XmlDoc.Load(FilePathInterface);
		XmlNode userNode;
		for (int i = 0; i < allInterface.Length; i++) {
			if (allInterface[i] == null) return;
			userNode = XmlDoc.SelectSingleNode ("/Interface/" + allInterface[i].name);

			if (userNode != null) {
				Debug.Log ("Узел существует: " + userNode.Name);
			} else {
				userNode = XmlDoc.CreateElement (allInterface[i].name);
				if (useEncryptor == true) {
					userNode.InnerText = Encrypt (allInterface [i].text);
				} else {
					userNode.InnerText = allInterface [i].text;
				}
				XmlDoc.DocumentElement.AppendChild (userNode);
				Debug.Log ("Узел: " + userNode.Name + " создан");
			}
		}
		XmlDoc.Save(FilePathInterface);
	}
	public void ContentFormationXML(){ //Формируется список, если узел уже есть то не будет создан, а лишь добавлен новый
		XmlDocument XmlDoc = new XmlDocument();
		XmlDoc.Load(FilePathContent);
		XmlNode userNode;
		for (int i = 0; i < allContent.Length; i++) {
			if (allContent[i] == null) return;
			userNode = XmlDoc.SelectSingleNode ("/Content/" + allContent[i].name);

			if (userNode != null) {
				Debug.Log ("Узел существует: " + userNode.Name);
			} else {
				userNode = XmlDoc.CreateElement (allContent[i].name);
				if (useEncryptor == true) {
					userNode.InnerText = Encrypt (allContent [i].text);
				} else {
					userNode.InnerText = allContent [i].text;
				}
				XmlDoc.DocumentElement.AppendChild (userNode);
				Debug.Log ("Узел: " + userNode.Name + " создан");
			}
		}
		XmlDoc.Save(FilePathContent);
	}

	public void InterfaceWriteToXML()
	{
		XmlDocument XmlDoc = new XmlDocument();
		if (_textComponent.isInterface == true) {
			XmlDoc.Load (FilePathInterface);
		} else {
			XmlDoc.Load (FilePathContent);
		}
			XmlNode userNode;
		if (_textComponent.isInterface == true) {
			userNode = XmlDoc.SelectSingleNode ("/Interface/" + currentTextMeshPro.name);
		} else {
			userNode = XmlDoc.SelectSingleNode ("/Content/" + currentTextMeshPro.name);
		}
			
			if (userNode != null) {
				Debug.Log ("Узел существует: " + userNode.Name);
			} else {
				userNode = XmlDoc.CreateElement (currentTextMeshPro.name);
			if (useEncryptor == true) {
				userNode.InnerText = Encrypt (currentTextMeshPro.text);
			} else {
				userNode.InnerText = currentTextMeshPro.text;
			}
				XmlDoc.DocumentElement.AppendChild (userNode);
				Debug.Log ("Узел: " + userNode.Name + " создан");
			}
			XmlNodeList AchievementList = XmlDoc.GetElementsByTagName(currentTextMeshPro.name);
			foreach(XmlNode node in AchievementList)
			{
				if (node.Name == currentTextMeshPro.name) {
				if (useEncryptor == true) {
					node.InnerText = Encrypt (currentTextMeshPro.text);
				} else {
					node.InnerText = currentTextMeshPro.text;
				}
					Debug.Log ("Текст у узла: " + node.Name + " обновлен");
				}
			}
		if (_textComponent.isInterface == true) {
			XmlDoc.Save (FilePathInterface);
		} else {
			XmlDoc.Save (FilePathContent);
		}
	}

	public void TextEditorActivate(GameObject button){
		currentTextMeshPro = button.GetComponentInParent<TextMeshProUGUI> ();
		_textComponent = currentTextMeshPro.GetComponent<TextComponent> ();
		inputFieldText.text = currentTextMeshPro.text;
		inputFieldModal.SetActive (true);
	}
	public void TextEditorClose(){
		currentTextMeshPro.text = inputFieldText.text;
		inputFieldModal.SetActive (false);
		InterfaceWriteToXML();
	}
}
