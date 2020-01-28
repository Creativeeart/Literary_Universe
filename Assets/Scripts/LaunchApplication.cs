using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;
using System.IO;

public class LaunchApplication : MonoBehaviour {
	[Header("Укажите имя папки учитывая регистр")]
	public string folderAuthors;
	[Header("Укажите номер документа по списку начиная с 0")]
	[Range(0, 10)]
	public int numberDoc;
	List<string> filesList = new List<string>();

	void Start(){
		string path = Application.streamingAssetsPath + "/" + folderAuthors + "/Files/";
		if (Directory.Exists (path)) {
			foreach (string file in Directory.GetFiles(path,"*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".docx") || s.EndsWith(".pptx"))) 
			{
				filesList.Add (file);                 
			}
		}
		else {
			UnityEngine.Debug.Log ("Folder Not Found");
		}

	}
	public void startApp(){
		ProcessStartInfo startInfo = new ProcessStartInfo(filesList[numberDoc]);
		startInfo.WindowStyle = ProcessWindowStyle.Maximized;
		Process.Start (startInfo);

	}

}
