using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test_newFunc : MonoBehaviour
{
    public static Test_newFunc instance;
    public bool isDontDestroyOnLoad = true;

    void Awake()
    {
        if (isDontDestroyOnLoad)
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    public int i = 0;
    public void NewFunc()
    {
        i++;
        Debug.Log(i);
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
