using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class UI_Controller : MonoBehaviour
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

        public void ShowOverlayUI()
        {
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            _supportScripts._states.isActivateOverlayUI = true;
            //_supportScripts._pause_Game.isPaused = true;
            if (_supportScripts._orbitCam)
            {
                _supportScripts._orbitCam.isEnable = false;
            }
        }

        public void CloseOverlayUI()
        {
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            _supportScripts._states.isActivateOverlayUI = false;
            //_supportScripts._pause_Game.isPaused = false;
            if (_supportScripts._orbitCam)
            {
                _supportScripts._orbitCam.isEnable = true;
            }
        }
    }
}
