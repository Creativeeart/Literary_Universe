using UnityEngine; 
using DG.Tweening;
using cakeslice;
namespace cakeslice
{
    public class Swipe_scroll_menu : MonoBehaviour
    {
        public RectTransform m_Button;
        public GameObject Parent;
        public float endXpos;
        public float startXpos;
        public bool checkedButton = false;
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

        public void CheckedButton()
        {
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            checkedButton = !checkedButton;
            DOTween.defaultTimeScaleIndependent = true;
            if (checkedButton)
            {
                Parent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(endXpos, Parent.GetComponent<RectTransform>().anchoredPosition.y), 0.7f);
                //m_Button.DORotate (new Vector3 (0,0,180), 0.7f);
            }
            else if (!checkedButton)
            {
                Parent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startXpos, Parent.GetComponent<RectTransform>().anchoredPosition.y), 1.0f);
                //m_Button.DORotate (new Vector3 (0,0,360), 0.7f);
            }
        }
    }
}
