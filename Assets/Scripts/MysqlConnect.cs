using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class MysqlConnect : MonoBehaviour
{
    public bool isFirstRun = true;

    void Awake()
    {
        LoadPlayerPrefs();
    }
    void Start()
    {
        #if !UNITY_EDITOR
            StartCoroutine(POST());
        #endif
    }
    IEnumerator POST()
    {
        var Data = new WWWForm();
        if (isFirstRun)
        {
            Data.AddField("firstRun", 1);
            Data.AddField("otherRun", 1);
        }
        else
        {
            Data.AddField("firstRun", 0);
            Data.AddField("otherRun", 1);
        }
        var Query = new WWW("http://literaryuniverse.unitycoding.ru/update.php", Data);
        yield return Query;
        if (Query.error != null)
        {
            Debug.Log("Server does not respond : " + Query.error);
        }
        else
        {
            if (Query.text == "response") // в основном HTML код которым ответил сервер
            {
                Debug.Log("Server responded correctly");
            }
            else
            {
                Debug.Log("Server responded : " + Query.text);
            }
        }
        Query.Dispose();
    }
    public void LoadPlayerPrefs()
    { 
        if (PlayerPrefs.HasKey("isFirstRun"))
        {
            isFirstRun = bool.Parse(PlayerPrefs.GetString("isFirstRun"));
            isFirstRun = false;
        }
        PlayerPrefs.SetString("isFirstRun", isFirstRun.ToString());
    }
}
