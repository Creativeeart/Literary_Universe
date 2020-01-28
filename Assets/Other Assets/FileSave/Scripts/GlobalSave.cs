using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class GlobalSave : MonoBehaviour {
	public static GlobalSave _GlobalSave;
	public string saveName = "Save";
	public string[] Saves;
	private bool saved = true;
	private string playerDataPath;
	public bool exitSave = true;

	// Use this for initialization
	void Awake(){
		_GlobalSave = this;
		ResetPath (true);
	}
	public int GetInt (int i) {
		if (Saves[i] != ""){
			return int.Parse(Saves[i]);
		}
		else {
			return 0;
		}
	}
	public float GetFloat(int i){
		if (Saves[i] != ""){
			return float.Parse(Saves[i]);
		}
		else {
			return 0f;
		}
	}

	public string GetString(int i){
		return Saves[i];
	}

	public void SetInt (int i, int v){
		Saves[i] = v.ToString();
		saved = false;
	}

	public void SetFloat (int i, float v){
		Saves[i] = v.ToString();
		saved = false;
	}

	public void SetString (int i, string v){
		Saves[i] = v;
		saved = false;
	}

	public void Save(){
		StreamWriter dataWriter = new StreamWriter(playerDataPath);
		for(int i = 1; i < Saves.Length; i++)
		{
			dataWriter.WriteLine(Saves[i]);
		}
		saved = true;
		if (Application.isEditor){
			Debug.Log("Save");
		}
		dataWriter.Flush(); 
		dataWriter.Close(); 

	}

	public void OnDisable(){
		if (!saved && exitSave){
			Save();
		}
	}

	public void ClearSaveFile (){ 
		for(int i  = 1; i < Saves.Length; i++)
		{
			Saves[i] = "";
		}
		Debug.Log("Clean Complite");
		Save();
	}

	public void DeleteSaveFile (int i){ 
		var tempPlayerDataPath = Application.streamingAssetsPath +"/Saves/"+ saveName + i + ".db";
		if (File.Exists(tempPlayerDataPath)){
			File.Delete(tempPlayerDataPath);
			Debug.Log("Delete File: " +tempPlayerDataPath);
		}
		else {
			Debug.Log("File not found");
		}
	}

	public void ResetPath (bool load){ //load boolean - Will there be a reload of the array?
		playerDataPath = Application.streamingAssetsPath +"/Saves/"+ saveName + PlayerPrefs.GetInt("CurrentSave") + ".db"; //CurrentSave - number save-file.
		if (!File.Exists(playerDataPath)){
//			var dataWriter = new StreamWriter(playerDataPath);
		}
		else if (load) {
			StreamReader dataReader = File.OpenText(playerDataPath);
			for(int i = 1; i < Saves.Length; i++)
			{
				Saves[i] = dataReader.ReadLine();
			}
		}
	}

}











































