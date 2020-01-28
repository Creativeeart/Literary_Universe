using UnityEngine;
namespace cakeslice
{
	public class Book_controller : MonoBehaviour {

        public GameObject[] booksUI;
        SupportScripts _supportScripts;
        private void Awake()
        {
            for (int i = 0; i < booksUI.Length; i++)
                booksUI[i].SetActive(true);
        }

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
            for (int i = 0; i < booksUI.Length; i++)
            {
                booksUI[i].SetActive(false);
                booksUI[i].transform.GetChild(0).localScale = new Vector3(0, 0, 0);
            }
        }

        GameObject FindGameObject()
        {
            GameObject result = null;
            for (int i = 0; i < booksUI.Length; i++)
            {
                if (booksUI[i].activeSelf)
                {
                    result = booksUI[i];
                    break;
                }
            }
            return result;
        }

        public void CloseModal()
        {
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            FindGameObject().SetActive(!FindGameObject().activeSelf);
            _supportScripts._UI_Controller.CloseOverlayUI();
        }
    }
}