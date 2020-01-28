using UnityEngine;
namespace cakeslice
{
    public class StarController : MonoBehaviour
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
        void OnMouseOver()
        {
            if (!_supportScripts._pause_Game.isPaused)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        void OnMouseExit()
        {
            if (!_supportScripts._pause_Game.isPaused)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}