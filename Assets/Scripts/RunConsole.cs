using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace cakeslice
{
    public class RunConsole : MonoBehaviour
    {
        public GameObject[] UI;
        public string[] consoleComands;
        public GameObject consolePanel;
        public ScrollRect _scrollRect;
        public TextMeshProUGUI outputComands;
        public TMP_InputField inputComands;
        SupportScripts _supportScripts;
        bool isOpenConsole = false;
        void Start()
        {
            FindSupportScripts();
            //Loading Scenes
            consoleComands = new string[11];
            consoleComands[0] = "[0] - Главная сцена";
            consoleComands[1] = "[1] - Страница Жюля Верна";
            consoleComands[2] = "[2] - Страница Предсказания Жюля Верна";
            consoleComands[3] = "[3] - Страница Планета Жюля Верна";
            consoleComands[4] = "[4] - Страница Форт Боярд";
            consoleComands[5] = "[5] - Страница Кира Булычева";
            consoleComands[6] = "[6] - Страница Станислава Востокова";
            consoleComands[10] = "[7] - Страница 'Созвездия читателей'";
            //---//
            consoleComands[7] = "[reload] - Перезагрузить сцену";
            consoleComands[8] = "[hideui] - Скрыть UI";
            consoleComands[9] = "[showui] - Показать UI";
            
            //Loading Scenes
            for (int i = 0; i < consoleComands.Length; i++)
            {
                outputComands.text += "\n" + consoleComands[i];
            }
        }

        void FindSupportScripts()
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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                isOpenConsole = !isOpenConsole;
                consolePanel.SetActive(!consolePanel.activeSelf);
                inputComands.ActivateInputField();
            }
            if (isOpenConsole)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    FindSupportScripts();
                    if (inputComands.text == "Help")
                    {
                        for (int i = 0; i < consoleComands.Length; i++)
                        {
                            outputComands.text += "\n" + consoleComands[i];
                        }
                    }
                    if (inputComands.text == "0")
                    {
                        _supportScripts.LoadLevel("Main_Scene");
                    }
                    if (inputComands.text == "1")
                    {
                        _supportScripts.LoadLevel("Jule_Verne_Scene");
                    }
                    if (inputComands.text == "2")
                    {
                        _supportScripts.LoadLevel("Jule_Verne_Prophecy");
                    }
                    if (inputComands.text == "3")
                    {
                        _supportScripts.LoadLevel("Jule_Verne_Planet");
                    }
                    if (inputComands.text == "4")
                    {
                        _supportScripts.LoadLevel("FortBoyard");
                    }
                    if (inputComands.text == "5")
                    {
                        _supportScripts.LoadLevel("Bylechev_Scene");
                    }
                    if (inputComands.text == "6")
                    {
                        _supportScripts.LoadLevel("Vostokov_Scene");
                    }
                    if (inputComands.text == "7")
                    {
                        _supportScripts.LoadLevel("Scene_Create_Star");
                    }
                    if (inputComands.text == "reload")
                    {
                        _supportScripts._gameController.ReloadScene();
                    }
                    if (inputComands.text == "hideui")
                    {
                        for (int i = 0; i < UI.Length; i++)
                        {
                            //UI[i].GetComponent<CanvasGroup>().alpha = 0;
                            //UI[i].GetComponent<CanvasGroup>().interactable = false;
                            UI[i].SetActive(false);
                        }
                    }
                    if (inputComands.text == "showui")
                    {
                        for (int i = 0; i < UI.Length; i++)
                        {
                            //UI[i].GetComponent<CanvasGroup>().alpha = 1;
                            //UI[i].GetComponent<CanvasGroup>().interactable = true;
                            UI[i].SetActive(true);
                        }
                    }
                    SubmitValue();
                }
            }
        }

        public void SubmitValue()
        {
            if (inputComands != null)
            {
                if (outputComands.text == string.Empty)
                {
                    outputComands.text += inputComands.text;
                }
                else
                {
                    outputComands.text += "\n" + inputComands.text;
                }
                inputComands.text = string.Empty;
                consolePanel.SetActive(false);
                consolePanel.SetActive(true);
                _scrollRect.normalizedPosition = new Vector2(_scrollRect.normalizedPosition.x, 0);
            }
        }
    }
}