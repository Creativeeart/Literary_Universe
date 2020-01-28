using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class ConfigurationProject : MonoBehaviour {
    public bool backgroundSound = true;
    public bool IsShowTips = false;
    public bool startIntro = true;
    public float musicVolume = 0.3f;
    public float soundVolume = 0.3f;
    //public float saveFloat = 0.25f;
    //public int saveInt = 7;
    //public bool saveBool = true;
    //public string[] saveArray = { "Элемент_0", "Элемент_1", "Элемент_2" };

    //public float loadFloat = 0;
    //public int loadInt = 0;
    //public bool loadBool = false;
    //public string[] loadArray;

    void Awake()
    {
        LoadPlayerPrefs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //PlayerPrefs.DeleteAll();
        }
    }

    public void SavePlayerPrefs()
    {
        //PlayerPrefs.SetFloat("saveFloat", saveFloat);
        //PlayerPrefs.SetInt("saveInt", saveInt);
        //PlayerPrefs.SetString("saveBool", saveBool.ToString());

        //for (int i = 0; i < saveArray.Length; i++)
        //{
        //PlayerPrefs.SetString("elementArray_" + i, saveArray[i]);
        //}

        PlayerPrefs.SetString("backgroundSound", backgroundSound.ToString());
        PlayerPrefs.SetString("IsShowTips", IsShowTips.ToString());
        PlayerPrefs.SetString("startIntro", startIntro.ToString());
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
    }

    public void LoadPlayerPrefs()
    {
        //if (PlayerPrefs.HasKey("saveFloat")) loadFloat = PlayerPrefs.GetFloat("saveFloat");
        //if (PlayerPrefs.HasKey("saveInt")) loadInt = PlayerPrefs.GetInt("saveInt");
        //if (PlayerPrefs.HasKey("saveBool")) loadBool = bool.Parse(PlayerPrefs.GetString("saveBool"));

        //int j = 0;
        //List<string> tmp = new List<string>();
        //while (PlayerPrefs.HasKey("elementArray_" + j))
        //{
            //tmp.Add(PlayerPrefs.GetString("elementArray_" + j));
            //j++;
        //}

        //loadArray = new string[tmp.Count];
        //for (int i = 0; i < tmp.Count; i++)
        //{
            //loadArray[i] = tmp[i];
        //}

        if (PlayerPrefs.HasKey("backgroundSound")) backgroundSound = bool.Parse(PlayerPrefs.GetString("backgroundSound"));
        if (PlayerPrefs.HasKey("IsShowTips")) IsShowTips = bool.Parse(PlayerPrefs.GetString("IsShowTips"));
        if (PlayerPrefs.HasKey("startIntro")) startIntro = bool.Parse(PlayerPrefs.GetString("startIntro"));
        if (PlayerPrefs.HasKey("musicVolume")) musicVolume = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.HasKey("soundVolume")) soundVolume = PlayerPrefs.GetFloat("soundVolume");
    }
}
