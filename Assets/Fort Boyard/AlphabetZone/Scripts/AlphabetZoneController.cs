using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
namespace cakeslice
{
    public class AlphabetZoneController : MonoBehaviour
    {
        public string CorrectWord;
        public GameObject UI_AlphabetZone;
        public TextMeshProUGUI WordUI_TextMeshPro;
        public TextMeshProUGUI Tips;
        public Animator headLion;

        public List<string> Word { get; set; }
        public string LastChar { get; set; }
        public string InputWord { get; set; }
        public bool IsEnableChars { get; set; }
        public bool IsWordCorrect { get; set; }
        public bool RunTime { get; set; }
        public float Seconds { set; get; }
        public int MaxChar { get; set; }
        public int CurentChar { get; set; }
        bool runFakeSekonds = false;
        float waitForMoneyFalling = 0f;
        GameObject[] selectChars;
        
        public static AlphabetZoneController Instance { get; private set; }

        public string MergeText()
        {
            string result = string.Empty;
            foreach (string item in Word)
            {
                result += item;
            }
            return result;
        }

        public void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            selectChars = GameObject.FindGameObjectsWithTag("Words").OrderBy(go => go.name).ToArray();
        }
        public void AlphabetZoneEntered()
        {
            FortBoyardGameController.Instance.IsAlphabetZone = true;
            UI_AlphabetZone.SetActive(true);
            WordUI_TextMeshPro.text = string.Empty;
            Tips.text = GateZoneController.Instance.tipsText.text;
            MaxChar = CorrectWord.Length;
            for (int i = 0; i < selectChars.Length; i++) selectChars[i].GetComponent<Outline>().enabled = false;
            for (int i = 0; i < selectChars.Length; i++) selectChars[i].GetComponent<SelectChar>().selectObject = false;
            IsEnableChars = true;
        }

        public void ClearOutline()
        {
            for (int i = 0; i < selectChars.Length; i++) selectChars[i].GetComponent<Outline>().enabled = false;
            for (int i = 0; i < selectChars.Length; i++) selectChars[i].GetComponent<SelectChar>().selectObject = false;
        }
        void Update()
        {
            if (FortBoyardGameController.Instance.IsAlphabetZone)
            {
                if (RunTime) Seconds += Time.deltaTime;
                if (Seconds >= 3)
                {
                    if (IsWordCorrect)
                    {
                        WordUI_TextMeshPro.color = Color.green;
                        RotationHead();
                        RunTime = false;
                        Seconds = 0;
                        ClearOutline();
                    }
                    else
                    {
                        //cameraShake.shakeDuration = 0.5f;
                        WordUI_TextMeshPro.color = Color.red;
                        GateZoneController.Instance.TimeReducing(5);
                        ReturnToAlphabet();
                        RunTime = false;
                        Seconds = 0;
                    }
                }
                if (runFakeSekonds) waitForMoneyFalling += Time.deltaTime;
                if ((waitForMoneyFalling >= 3) && (runFakeSekonds == true))
                {
                    
                    UI_AlphabetZone.SetActive(false);
                    StartCoroutine(FB_CamMovingController.Instance.GoToTreasureZone()); // Переход к сокровищнице
                    runFakeSekonds = false;
                }
            }
        }
        public void RotationHead()
        {
            headLion.enabled = true;
            runFakeSekonds = true;
        }

        public void ReturnToAlphabet()
        {
            FB_CamMovingController.Instance.CameraMovingToPoint(FB_CamMovingController.Instance.pointToAlphabetZoneB);
            StartCoroutine("ClearInterfaceWaiting");
        }

        public void ClearInput()
        {
            InputWord = string.Empty;
            CurentChar = 0;
            ClearOutline();
        }
        public void ClearInputByInterface()
        {
            ClearInput();
            WordUI_TextMeshPro.text = string.Empty;
            WordUI_TextMeshPro.color = Color.white;
            Word.Clear();
        }
        IEnumerator ClearInterfaceWaiting()
        {
            yield return new WaitForSeconds(1.0f);
            ClearInputByInterface();
        }

    }
}
