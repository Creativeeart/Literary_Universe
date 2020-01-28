using UnityEngine;
using DG.Tweening;
using TMPro;
namespace cakeslice
{
    public class Toggle : MonoBehaviour
    {
        public GameObject modalWindowObject;
        string nameBook;
        int arrayNumber;
        GameObject bookNameFormPopUp;
        Transform earthPrefabTransform;
        Quaternion pos;
        bool isCheck = false;
        SupportScripts _supportScripts;
        private Outline _outLine;
        void Start(){
            if (GameObject.Find("SupportSripts"))
            {
                _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
            }
            else
            {
                Debug.Log("SupportSripts - не найден либо отключен!");
                return;
            }
            if (GameObject.Find("Book_Names_Canvas"))
            {
                bookNameFormPopUp = GameObject.Find("Book_Names_Canvas");
            }
            else
            {
                Debug.Log("Book_Names_Canvas - не найден либо отключен!");
                return;
            }

            if (GameObject.Find("Earth_Prefab"))
            {
                earthPrefabTransform = GameObject.Find("Earth_Prefab").GetComponent<Transform>();
            }
            else
            {
                Debug.Log("Earth_Prefab - не найден либо отключен!");
                return;
            }
            _outLine = gameObject.GetComponent<Outline>();
            _outLine.enabled = false;
            FindNumberAndNameInObject();
            //AutoSearchModalGameobjects();
        }

        void FindNumberAndNameInObject()
        {
            string[] entries = gameObject.name.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '_' }); //Разделяем на массивы строку
                arrayNumber = int.Parse(entryInfo[0]);
                nameBook = entryInfo[1];
                //bool isNum = int.TryParse(entryInfo[i], out arrayNumber); //Проверяем является ли числом строка
                //if (isNum) arrayNumber = int.Parse(entryInfo[0]);
                //else arrayNumber = 0;
            }
        }

        void AutoSearchModalGameobjects()
        {
            string[] entries = gameObject.name.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            string tempName = string.Empty;
            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '_' }); //Разделяем на массивы строку
                tempName = entryInfo[0] + "_" + entryInfo[1] + "_modal";
            }
            modalWindowObject = GameObject.Find(tempName);
        }

        void FixedUpdate()
        {
            if (isCheck)
            {
                Quaternion Rot = Quaternion.Euler(pos.x, pos.y, pos.z);
                earthPrefabTransform.transform.localRotation = Quaternion.Lerp(earthPrefabTransform.transform.localRotation, Rot, Time.deltaTime);
            }
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            _supportScripts._objectPos.posRot[13] = pos;
            _supportScripts._objectPos.posRot[14] = pos;
        }

        public void OnMouseEnter(){
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            if (!_supportScripts._states.isActivateOverlayUI) {
                _outLine.enabled = true;
				DOTween.defaultTimeScaleIndependent = true;
                bookNameFormPopUp.transform.GetChild(0).gameObject.SetActive(true);
                bookNameFormPopUp.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = nameBook;
                if (arrayNumber != 0) pos = _supportScripts._objectPos.posRot[arrayNumber];
            }
            
        }
		public void OnMouseExit(){
            _outLine.enabled = false;
			DOTween.defaultTimeScaleIndependent = true;
            bookNameFormPopUp.transform.GetChild(0).gameObject.SetActive(false);
            bookNameFormPopUp.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Empty;
        }

        public void OnMouseEnterInBoookButtons()
        {
            isCheck = true;
        }

        public void OnMouseExitInBoookButtons()
        {
            isCheck = false;
        }

        public void OnMouseDown()
        {
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            if (modalWindowObject != null)
            {
                if (!_supportScripts._states.isActivateOverlayUI)
                {
                    modalWindowObject.SetActive(true);
                    _supportScripts._UI_Controller.ShowOverlayUI();
                }
            }
        }
    }
}