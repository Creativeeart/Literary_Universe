using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WordAddController : MonoBehaviour
{
    public List<string> words;
    public string lastWord;
    public Text currentText;
    // Use this for initialization
    void Start()
    {
        //words = new List<string>(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.GetComponent<Rigidbody>() != null)
                {
                    if (!hit.transform.GetComponent<CurrentWord>().isWordPressed)
                    {
                        words.Add(hit.transform.name);
                        lastWord = hit.transform.name;
                        hit.transform.GetComponent<CurrentWord>().isWordPressed = true;
                        currentText.text = MergeText();
                    }
                    else
                    {
                        if (words.Contains(lastWord))
                        {
                            if (lastWord == hit.transform.name)
                            {
                                words.RemoveAt(words.Count-1);
                                lastWord = hit.transform.name;
                                hit.transform.GetComponent<CurrentWord>().isWordPressed = false;
                                currentText.text = MergeText();
                            }
                            else
                            {
                                Debug.LogFormat("Вы можете удалить только последний введенный символ: {0}", lastWord);
                            }
                        }
                    }
                    
                }
            }
        }
    }

    string MergeText()
    {
        string result = string.Empty;
        foreach (string item in words)
        {
            result += item;
        }
        return result;
    }

}
