using UnityEngine;
namespace cakeslice
{
    public class SwitchObjects : MonoBehaviour
    { 
        public GameObject SwitchableObjects;
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
        public void OnMouseDown()
        {
            if (_supportScripts._swipe_scroll_menu.checkedButton == false)
            {
                if (_supportScripts._states.isActivateOverlayUI == false)
                {
                    if (SwitchableObjects != null)
                    {
                        if (_supportScripts.chekActivateModalWindow.isActivate == false)
                        {
                            SwitchableObjects.SetActive(true);
                            _supportScripts._UI_Controller.ShowOverlayUI();
                            //pauseGame.CheckPause ();
                        }
                        _supportScripts.chekActivateModalWindow.isActivate = true;
                    }
                }
            }
        }
    }

}