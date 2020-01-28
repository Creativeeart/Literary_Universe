using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo_Login : MonoBehaviour {

    public Text text;
    public InputField realnameIf;
    public InputField usernameIF;
    public InputField passIF;

    int status = 0;
    readonly string url = "http://literaryuniverse.unitycoding.ru"; //Переменная для хранения адреса
    string username = ""; //Переменная для хранения имени
    string pswd = ""; //Переменная для хранения пароля
    string realname = "";
    public int score = 0;

    IEnumerator Highscore_POST()
    {
        var Data = new WWWForm();
        Data.AddField("login", username);
        Data.AddField("fb_score", score);
        var Query = new WWW(url + "/highScoreAdds.php", Data);
        yield return Query;
        if (Query.error != null)
        {
            Debug.Log("Сервер не отвечает: " + Query.error);
        }
        else
        {
            Debug.Log("Сервер ответил: " + Query.text);
        }
        Query.Dispose();
    }

    public void SendHigscore()
    {
        username = usernameIF.text;
        StartCoroutine(Highscore_POST());
    }


    IEnumerator Registration_POST()
    {
        var Data = new WWWForm();
            Data.AddField("login", username);
            Data.AddField("pass", pswd);
            Data.AddField("realname", realname);
        var Query = new WWW(url + "/userAdds.php", Data);
        yield return Query;
        if (Query.error != null)
        {
            Debug.Log("Сервер не отвечает: " + Query.error);
        }
        else
        {
            Debug.Log("Сервер ответил: " + Query.text);
        }
        Query.Dispose();
    }
    public void Send()
    {
        realname = realnameIf.text;
        username = usernameIF.text;
        pswd = passIF.text;
        StartCoroutine(Registration_POST());
    }

    IEnumerator Login_GET()
    {
        var Data = new WWWForm();
        Data.AddField("login", username);
        Data.AddField("pass", pswd);
        var Query = new WWW(url + "/login.php", Data);
        yield return Query;
        if (Query.error != null)
        {
            Debug.Log("Сервер не отвечает: " + Query.error);
        }
        else
        {
            Debug.Log("Сервер ответил: " + Query.text);
            status = int.Parse(Query.text);
        }
        Query.Dispose();
    }
    public void CheckLogin()
    {
        username = usernameIF.text;
        pswd = passIF.text;
        StartCoroutine(Login_GET());
    }

    IEnumerator ListUsers_GET()
    {
        WWW www = new WWW(url + "/users.txt");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            text.text = www.text;
            Debug.Log("Список пользователей получен");
        }
        else
        {
            Debug.Log("Ошибка получения списка пользователей: " + www.error);
        }
    }
    public void GetText()
    {
        StartCoroutine(ListUsers_GET());
    }
}
