using UnityEngine;

public class DontDestroyOnLoadObjects : MonoBehaviour {
    public static DontDestroyOnLoadObjects instance;
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
}
