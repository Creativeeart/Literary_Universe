using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class CallTips : MonoBehaviour
    {
        public int _indexTip;
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

        void OnMouseDown()
        {
            if (_supportScripts._tips == null) return;
            _supportScripts._tips.Tip(_indexTip);
        }
    }
}
