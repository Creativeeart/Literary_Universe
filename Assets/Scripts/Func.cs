using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func : MonoBehaviour
{
    Test_newFunc test_NewFunc;
    public void OneFunc()
    {
        if (test_NewFunc == null)
        {
            test_NewFunc = gameObject.AddComponent<Test_newFunc>();
        }
        test_NewFunc.NewFunc();
    }
    public void LoadScene(string sceneName)
    {
        test_NewFunc.SceneLoad(sceneName);
    }
}
