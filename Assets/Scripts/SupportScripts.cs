using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using cakeslice;
public class SupportScripts : MonoBehaviour
{
    public GameObject UI;
    public LevelLoaderManager levelLoaderManager;
    //public UI_States _UI_States;
    public Pause_game _pause_Game;
    public Book_controller _book_Controller;
    public ObjectsPos _objectPos;

    public CheckActivateModalWindow chekActivateModalWindow;
    public OrbitCam _orbitCam;
    public OrbitCamMouse _orbitCamMouse;
    public ConfigurationProject _configurationProject;
    public ToggleController _toggleControllerTips;
    public ToggleController _toggleControllerMusic;
    public Authorization _authorization;
    public States _states;

    public Swipe_scroll_menu _swipe_scroll_menu;
    public GameController _gameController;
    public MovingCameraFromAuthors _movingCameraFromAuthors;
    public Tips _tips;
    public TooltipV2 _tooltipV2;

    public GameObject UIManager;
    public UI_Controller _UI_Controller;
    public static SupportScripts Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
        if (GameObject.Find("LoadingManager"))
        {
            levelLoaderManager = GameObject.Find("LoadingManager").GetComponent<LevelLoaderManager>();
        }
        else
        {
            Debug.Log("LoadingManager - не найден либо отключен!");
        }
        //if (GameObject.Find("UI_StatesManager"))
        //{
        //    _UI_States = GameObject.Find("UI_StatesManager").GetComponent<UI_States>();
        //}
        //else
        //{
        //    Debug.Log("UI_StatesManager - не найден либо отключен!");
        //}
        if (GameObject.Find("Pause Game Controller"))
        {
            _pause_Game = GameObject.Find("Pause Game Controller").GetComponent<Pause_game>();
        }
        else
        {
            Debug.Log("Pause Game Controller - не найден либо отключен!");
        }
        if (GameObject.Find("Game_Controller"))
        {
            _book_Controller = GameObject.Find("Game_Controller").GetComponent<Book_controller>();
            _objectPos = GameObject.Find("Game_Controller").GetComponent<ObjectsPos>();
        }
        else
        {
            Debug.Log("Game_Controller - не найден либо отключен!");
        }
        if (GameObject.Find("Game Controller"))
        {
            chekActivateModalWindow = GameObject.Find("Game Controller").GetComponent<CheckActivateModalWindow>();
        }
        else
        {
            Debug.Log("Game Controller - не найден либо отключен!");
        }

        if (GameObject.Find("Main Camera"))
        {
            _orbitCam = GameObject.Find("Main Camera").GetComponent<OrbitCam>();
            _orbitCamMouse = GameObject.Find("Main Camera").GetComponent<OrbitCamMouse>();
        }
        else
        {
            Debug.Log("Main Camera - не найден либо отключен!");
        }

        if (GameObject.Find("ConfigurationProject"))
        {
            _configurationProject = GameObject.Find("ConfigurationProject").GetComponent<ConfigurationProject>();
        }
        else
        {
            Debug.Log("ConfigurationProject - не найден либо отключен!");
        }

        if (GameObject.Find("ToggleControllerTips"))
        {
            _toggleControllerTips = GameObject.Find("ToggleControllerTips").GetComponent<ToggleController>();
        }
        else
        {
            Debug.Log("ToggleControllerTips - не найден либо отключен!");
        }

        if (GameObject.Find("ToggleControllerMusic"))
        {
            _toggleControllerMusic = GameObject.Find("ToggleControllerMusic").GetComponent<ToggleController>();
        }
        else
        {
            Debug.Log("ToggleControllerMusic - не найден либо отключен!");
        }

        if (GameObject.Find("AuthorizationManager"))
        {
            _authorization = GameObject.Find("AuthorizationManager").GetComponent<Authorization>();
        }
        else
        {
            Debug.Log("AuthorizationManager - не найден либо отключен!");
        }

        if (GameObject.FindGameObjectWithTag("States"))
        {
            _states = GameObject.FindGameObjectWithTag("States").GetComponent<States>();
        }
        else
        {
            Debug.Log("States - не найден либо отключен!");
        }
        if (GameObject.Find("Main UI Controller"))
        {
            _swipe_scroll_menu = GameObject.Find("Main UI Controller").GetComponent<Swipe_scroll_menu>();
            _gameController = GameObject.Find("Main UI Controller").GetComponent<GameController>();
            _tips = GameObject.Find("Main UI Controller").GetComponent<Tips>();
            _UI_Controller = GameObject.Find("Main UI Controller").GetComponent<UI_Controller>();
            _tooltipV2 = GameObject.Find("Main UI Controller").GetComponent<TooltipV2>();
        }
        else
        {
            Debug.Log("Main UI Controller - не найден либо отключен!");
        }

        if (GameObject.Find("Game Controller"))
        {
            _movingCameraFromAuthors = GameObject.Find("Game Controller").GetComponent<MovingCameraFromAuthors>();
        }
        else
        {
            Debug.Log("Game Controller - не найден либо отключен!");
        }
        if (GameObject.FindGameObjectWithTag("UI"))
        {
            UI = GameObject.FindGameObjectWithTag("UI");
        }
        else
        {
            Debug.Log("UI - не найден либо отключен!");
        }

        if (GameObject.Find("UIManager"))
        {
            UIManager = GameObject.Find("UIManager");
        }
        else
        {
            Debug.Log("UIManager - не найден либо отключен!");
        }
        BreadCrumbs();
    }

    private void Start()
    {
        StartCoroutine(DisableUI());
    }

    //START BREADCRUMBS
    private string currentSceneName;
    private TextMeshProUGUI mainSceneNameTextMeshPro;
    private Image backButtonImage;
    private Button backButton;
    private TextMeshProUGUI backButtonTextMeshPro;
    private TextMeshProUGUI sceneNameWithTextMeshPro;

    void IsMainScene(bool isMainScene, string sceneName)
    {
        if (isMainScene)
        {
            mainSceneNameTextMeshPro.enabled = true;
            backButtonImage.enabled = false;
            backButton.enabled = false;
            backButtonTextMeshPro.enabled = false;
            sceneNameWithTextMeshPro.enabled = false;
            sceneNameWithTextMeshPro.text = string.Empty;
        }
        else
        {
            mainSceneNameTextMeshPro.enabled = false;
            backButtonImage.enabled = true;
            backButton.enabled = true;
            backButtonTextMeshPro.enabled = true;
            sceneNameWithTextMeshPro.enabled = true;
            sceneNameWithTextMeshPro.text = sceneName;
        }
    }
    void BreadCrumbs()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        mainSceneNameTextMeshPro = GameObject.Find("BreadCrumbs_MainSceneName_TextMeshPro_Interface").GetComponent<TextMeshProUGUI>();
        backButtonImage = GameObject.Find("BreadCrumbs_Back_Button").GetComponent<Image>();
        backButton = GameObject.Find("BreadCrumbs_Back_Button").GetComponent<Button>();
        backButtonTextMeshPro = GameObject.Find("BreadCrumbs_Back_Button_TextMeshPro_Interface").GetComponent<TextMeshProUGUI>();
        sceneNameWithTextMeshPro = GameObject.Find("BreadCrumbs_NameScene_TextMeshPro_Interface").GetComponent<TextMeshProUGUI>();
        if (currentSceneName == "Main_Scene")
        {
            IsMainScene(true, "");
        }
        if (currentSceneName == "Bylechev_Scene")
        {
            IsMainScene(false, "Кир Булычев");
        }
        if (currentSceneName == "Jule_Verne_MiniGameInShip")
        {
            IsMainScene(false, "Спастись с корабля");
        }
        if (currentSceneName == "Jule_Verne_Planet")
        {
            IsMainScene(false, "Интерактивный навигатор по книгам Жюля Верна");
            UIManager.GetComponent<CanvasGroup>().alpha = 1;
        }
        if (currentSceneName == "Jule_Verne_Prophecy")
        {
            IsMainScene(false, "Предсказания Жюля Верна");
        }
        if (currentSceneName == "Jule_Verne_Scene")
        {
            IsMainScene(false, "Жюль Верн");
        }
        if (currentSceneName == "Vostokov_Scene")
        {
            IsMainScene(false, "Станислав Востоков");
        }
        if (currentSceneName == "Scene_Create_Star")
        {
            IsMainScene(false, "Созвездие читателей");
        }

    }
    //END BREADCRUMBS

    public IEnumerator DisableUI()
    {
        yield return null;
        if (currentSceneName == "FortBoyard")
        {
            UI.SetActive(false);
            UIManager.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void EnableUI()
    {
        UI.SetActive(true);
    }

    public void LoadLevel(string sceneName)
    {
        if (levelLoaderManager)
        {
            levelLoaderManager.LoadLevel(sceneName);
        }
        else
        {
            Debug.Log("LoadingManager - не найден либо отключен!");
            return;
        }
    }
    public void UIControllerShowOverlayUI()
    {
        _UI_Controller.ShowOverlayUI();
    }

    public void UIControllerCloseOverlayUI()
    {
        _UI_Controller.CloseOverlayUI();
    }

    public void TooltipV2Fade()
    {
        _tooltipV2.Fade();
    }

    public void ExitGame()
    {
        _gameController.AppExitBtn();
    }
}
