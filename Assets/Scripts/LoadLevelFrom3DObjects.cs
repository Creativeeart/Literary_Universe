using UnityEngine;
namespace cakeslice
{
    public class LoadLevelFrom3DObjects : MonoBehaviour
    {
        public string sceneName;
        SupportScripts _supportScripts;
        void Start()
        {
            if (GameObject.Find("SupportSripts"))
                _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
            else
            {
                Debug.Log("SupportSripts - не найден либо отключен!");
                return;
            }
        }

        void OnMouseDown()
        {
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            if (!_supportScripts._states.isActivateOverlayUI)
            {
                _supportScripts.levelLoaderManager.LoadLevel(sceneName);
            }
        }
    }
}
