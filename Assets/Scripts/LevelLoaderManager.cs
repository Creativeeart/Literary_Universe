using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
namespace cakeslice
{
    public class LevelLoaderManager : MonoBehaviour
    {
        public string currentSceneName;
        public GameObject loadingScreen;
        public Image loadingImage;
        public TextMeshProUGUI progressText;
        public Image image;
        public Animator animator;
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

        public void LoadLevelBreadCrumbs()
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            if ((currentSceneName == "Bylechev_Scene") 
                || (currentSceneName == "Jule_Verne_Scene")
                || (currentSceneName == "Vostokov_Scene")
                || (currentSceneName == "Scene_Create_Star")
                || (currentSceneName == "Verkin_Scene")
                || (currentSceneName == "PasternakJvalevskij_Scene")
                )
            {
                LoadLevel("Main_Scene");
            }
            if (currentSceneName == "Jule_Verne_MiniGameInShip")
            {
                LoadLevel("Jule_Verne_Planet");
            }
            if ((currentSceneName == "Jule_Verne_Planet") 
                || (currentSceneName == "Jule_Verne_Prophecy")
                )
            {
                LoadLevel("Jule_Verne_Scene");
            }
            //if (currentSceneName == "Jule_Verne_Prophecy")
            //{
            //    LoadLevel("Jule_Verne_Scene");
            //}
            //if (currentSceneName == "Jule_Verne_Scene")
            //{
            //    LoadLevel("Main_Scene");
            //}
            //if (currentSceneName == "Vostokov_Scene")
            //{
            //    LoadLevel("Main_Scene");
            //}
            //if (currentSceneName == "Scene_Create_Star")
            //{
            //    LoadLevel("Main_Scene");
            //}
            //if (currentSceneName == "Verkin_Scene")
            //{
            //    LoadLevel("Main_Scene");
            //}
            //if (currentSceneName == "PasternakJvalevskij_Scene")
            //{
            //    LoadLevel("Main_Scene");
            //}
        }

        public void LoadLevel(string sceneName)
        {
            animator.SetBool("FadeOut", true);
            if (_supportScripts == null)
            {
                if (GameObject.Find("SupportSripts"))
                {
                    _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
                }
            }
            _supportScripts._states.isActivateOverlayUI = false;
            _supportScripts._pause_Game.isPaused = false;
            StartCoroutine(LoadAsynchronously(sceneName));
        }

        IEnumerator LoadAsynchronously(string sceneName)
        {
            yield return new WaitForSeconds(1);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            loadingScreen.SetActive(true);
            while (!operation.isDone)
            {
                float progress = operation.progress / .9f;
                loadingImage.fillAmount = progress;
                progressText.text = string.Format("Пожалуйста, подождите. Идет загрузка... {0:0}%", progress * 100);
                yield return null;
            }
            if (operation.isDone)
            {
                animator.SetBool("FadeOut", false);
                loadingImage.fillAmount = 0;
                loadingScreen.SetActive(false);
            }
        }
    }
}