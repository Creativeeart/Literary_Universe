using UnityEngine;
namespace cakeslice
{
    public class DisableMouseOrbitCam : MonoBehaviour
    {
        SupportScripts _supportScripts;
        void Start()
        {
            if (GameObject.Find("SupportSripts"))
            {
                _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
            }
            else
            {
                Debug.Log("SupportSripts - не найден либо отключен!");
                return;
            }
        }
        private void OnMouseDown()
        {
            _supportScripts._orbitCamMouse.enabled = false;
        }
    }
}
