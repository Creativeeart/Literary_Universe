using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using TMPro;
using System.Security.Cryptography;

public class TextComponent : MonoBehaviour {
	TextMeshProUGUI currentTextMeshPro;
	Text_editor _textEditor;
	public bool isInterface;
	string FilePathInterface, FilePathContent;

	void Start () {
		_textEditor = GameObject.Find ("TextEditorController").GetComponent<Text_editor> ();
		currentTextMeshPro = this.GetComponent<TextMeshProUGUI> ();
		FilePathInterface = Application.dataPath + "/StreamingAssets/AllText/Interface/interface.xml";
		FilePathContent = Application.dataPath + "/StreamingAssets/AllText/Content/content.xml";
		if (!File.Exists (FilePathInterface)) {
			CreateInterfaceXML ();
		}
		if (!File.Exists (FilePathContent)) {
			CreateContentXML ();
		}
		InterfaceLoadFromXML ();

	}
		
	void CreateInterfaceXML()
	{
		XmlDocument xmlDoc = new XmlDocument();
		XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
		xmlDoc.AppendChild(declaration);
		XmlNode rootNode = xmlDoc.CreateElement("Interface");
		xmlDoc.AppendChild(rootNode);
		xmlDoc.Save(FilePathInterface);
	}
	void CreateContentXML()
	{
		XmlDocument xmlDoc = new XmlDocument();
		XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
		xmlDoc.AppendChild(declaration);
		XmlNode rootNode = xmlDoc.CreateElement("Content");
		xmlDoc.AppendChild(rootNode);
		xmlDoc.Save(FilePathContent);
	}
	public static string Decrypt(string cipherText)
	{
		string EncryptionKey = "abc123";
		cipherText = cipherText.Replace(" ", "+");
		byte[] cipherBytes = System.Convert.FromBase64String(cipherText);
		using (Aes encryptor = Aes.Create())
		{
			Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
			encryptor.Key = pdb.GetBytes(32);
			encryptor.IV = pdb.GetBytes(16);
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cs.Write(cipherBytes, 0, cipherBytes.Length);
					cs.Close();
				}
				cipherText = Encoding.UTF8.GetString(ms.ToArray());
			}
		}
		return cipherText;
	}

	public void InterfaceLoadFromXML()
	{
		XmlDocument XmlDoc = new XmlDocument();
		if (isInterface == true) {
			XmlDoc.Load (FilePathInterface);
		} else {
			XmlDoc.Load (FilePathContent);
		}
		XmlNodeList AchievementList = XmlDoc.GetElementsByTagName(currentTextMeshPro.name);
		foreach(XmlNode node in AchievementList)
		{
			if(node.Name == currentTextMeshPro.name)
			{
				if (_textEditor.useEncryptor == true) {
					currentTextMeshPro.text = Decrypt (node.InnerText);
				} else {
					currentTextMeshPro.text = node.InnerText;
				}
			}
		}
	}
}
