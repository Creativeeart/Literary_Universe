using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Game_06_Controller : MonoBehaviour
{
    public Button OpenCasesButton;
    public Animator playerCase;
    public Animator computerCase;
    public GameObject ArrowOne, ArrowTwo;
    [Header("Main Settings")]
    AudioSource _audioSource;
    public AudioClip metalGong;
    public AudioClip tikTakTimer;
    public AudioClip gameOverSound;
    public Animator safeGlassAnimator;
    public Animator keyAnimator;
    public float mainTime = 15f;
    public float min, max;
    public Material defaultMaterial;
    public Material greenMaterial;
    public Material redMaterial;
    public Material tumblerDefault;
    public Material tumblerEmission;
    public TextMeshProUGUI countDownTimer;
    public Sprite closedLock, openedLock;
    public GameObject drawGameWindow;
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public Animator camAnim;
    public GameObject blackScreenCanvas;
    public Animator blackScreen;

    [Header("User")]
    public Animator handleUser;
    public Animator suspensionUser;
    public Animator roofTimeCaseUser;
    public GameObject bulbUser;
    public TextMeshProUGUI timeUser_TMPro;
    public Image userLockIconCase;


    [Header("Computer")]
    public Animator handleComputer;
    public Animator suspensionComputer;
    public Animator roofTimeCaseComputer;
    public GameObject bulbComputer;
    public TextMeshProUGUI timeComputer_TMPro;
    public Image computerLockIconCase;

    private float computerTime = 0;
    private float userTime = 0;
    private bool isStartGame = false;
    private float rand = 0;
    public bool isStopUserTime = false;
    public bool isStopComputerTime = false;
    public bool isUserCaseOpen = false;
    public bool isComputerCaseOpen = false;

    public static Game_06_Controller Instance { get; private set; }
    FortBoyardGameController FortBoyardGameController;

    public void Awake()
    {
        Instance = this;
    }

    public void StartGameWithButton()
    {
        StartCoroutine(CountDown());
    }
    private void Start()
    {
        FortBoyardGameController = FortBoyardGameController.Instance;
        gameObject.AddComponent<AudioSource>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2);
        float i = 3;
        while (i > 0)
        {
            i -= Time.deltaTime;
            countDownTimer.text = "Приготовьтесь!\n<size=120>" + (int)i + "</size>";
            yield return null;
        }
        if (i <= 1f)
        {
            roofTimeCaseUser.enabled = true;
            roofTimeCaseComputer.enabled = true;
        }
        yield return new WaitForSeconds(1);
        if (i <= 0)
        {
            userLockIconCase.enabled = true;
            userLockIconCase.sprite = closedLock;
            computerLockIconCase.enabled = true;
            computerLockIconCase.sprite = closedLock;
            StartGame();
        }
        countDownTimer.text = "<size=120>Старт!</size>";
        yield return new WaitForSeconds(1);
        countDownTimer.enabled = false;
        _audioSource.PlayOneShot(tikTakTimer);
    }
    void Update()
    {
        if (isStartGame)
        {
            if (computerTime < rand)
            {
                computerTime += Time.deltaTime;
                timeComputer_TMPro.text = computerTime.ToString("0.00");
            }
            if (!isStopUserTime)
            {
                userTime += Time.deltaTime;
                timeUser_TMPro.text = userTime.ToString("0.00");
            }

            if (computerTime > rand)
            {
                handleComputer.enabled = true;
                suspensionComputer.enabled = true;
                isStopComputerTime = true;
                computerLockIconCase.sprite = openedLock;
                //_audioSource.PlayOneShot(metalGong);
            }
            if ((computerTime > rand) && isStopUserTime)
            {
                isStartGame = false;
                _audioSource.Stop();
                StartCoroutine(OpenCases());
                OpenCasesButton.interactable = true;
            }
        }
    }
    public void OpenCase()
    {
        StartCoroutine(OpenCases());
        OpenCasesButton.interactable = false;
    }
    IEnumerator OpenCases()
    {
        yield return new WaitForSeconds(1);
        playerCase.GetComponent<Animator>().SetBool("isOpen", true);
        isUserCaseOpen = true;
        userLockIconCase.enabled = false;
        yield return new WaitForSeconds(1);
        computerCase.GetComponent<Animator>().SetBool("isOpen", true);
        Instance.isComputerCaseOpen = true;
        computerLockIconCase.enabled = false;
        yield return new WaitForSeconds(1);
        if (isComputerCaseOpen && isUserCaseOpen)
        {
            StartCoroutine(CalculateNumbers());
        }
    }
    public void StartGame()
    {
        isStartGame = true;
        rand = Random.Range(min, max);
    }
    public void StopUserTime()
    {
        if (isStartGame)
        {
            isStopUserTime = true;
            handleUser.enabled = true;
            suspensionUser.enabled = true;
            userLockIconCase.sprite = openedLock;
            _audioSource.PlayOneShot(metalGong);
            //ArrowOne.SetActive(true);
        }
    }
    public IEnumerator CalculateNumbers()
    {
        if ((computerTime > rand) && isStopUserTime)
        {
            camAnim.enabled = true;
            camAnim.SetBool("StartPos", false);
            yield return new WaitForSeconds(1);
            float computer = Mathf.Abs(mainTime - computerTime);
            float user = Mathf.Abs(userTime - mainTime);
            ArrowTwo.SetActive(false);
            ArrowOne.SetActive(false);
            if (computer < user)
            {
                Debug.Log("Computer Winner!");
                _audioSource.PlayOneShot(gameOverSound);
                bulbComputer.GetComponent<MeshRenderer>().material = greenMaterial;
                bulbUser.GetComponent<MeshRenderer>().material = redMaterial;
                GameOver();
                //StartCoroutine(ActiveLoseWindow());
            }
            else if (computer > user)
            {
                Debug.Log("User Winner!");
                bulbComputer.GetComponent<MeshRenderer>().material = redMaterial;
                bulbUser.GetComponent<MeshRenderer>().material = greenMaterial;
                GameWinner();
                //StartCoroutine(ActiveWinWindow());
            }
            else
            {
                Debug.Log("Draw!");
                bulbComputer.GetComponent<MeshRenderer>().material = redMaterial;
                bulbUser.GetComponent<MeshRenderer>().material = greenMaterial;
                GameWinner();
                //StartCoroutine(ActiveDrawWindow());
            }
        }
    }
    IEnumerator WinnerAnimations()
    {
        yield return new WaitForSeconds(1.5f);
        safeGlassAnimator.enabled = true;
        yield return new WaitForSeconds(1.0f);
        keyAnimator.SetBool("GameWinner", true);
        yield return new WaitForSeconds(2f);
        FortBoyardGameController.WinnerRoom("Tips");
    }
    public void RestartGame()
    {
        drawGameWindow.SetActive(false);
        StartCoroutine(RestartGameCoroutine(true));
    }

    
    public void GameOver()
    {
        Debug.Log("Покинуть комнату");
        loseGameWindow.SetActive(false);
        camAnim.SetBool("StartPos", true);
        //StartCoroutine(RestartGameCoroutine(false));
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        FortBoyardGameController.LoseRoom("Мастер победил!\nК сожалению вы не справились с испытанием");
    }

    public void GameWinner()
    {
        winGameWindow.SetActive(false);
        camAnim.SetBool("StartPos", true);
        //StartCoroutine(RestartGameCoroutine(false));
        StartCoroutine(WinnerAnimations());
    }
    IEnumerator RestartGameCoroutine(bool isRestart)
    {
        //blackScreenCanvas.SetActive(true);
        //blackScreen.enabled = true;
        //yield return new WaitForSeconds(1);
        //blackScreen.SetBool("BlackScreenOUT", true);

        camAnim.SetBool("StartPos", true);

        yield return new WaitForSeconds(1);
        blackScreen.SetBool("BlackScreenOUT", false);
        blackScreenCanvas.SetActive(false);
    }
    //IEnumerator ActiveDrawWindow()
    //{
    //    yield return new WaitForSeconds(2);
    //    drawGameWindow.SetActive(true);
    //}
    //IEnumerator ActiveWinWindow()
    //{
    //    yield return new WaitForSeconds(2);
    //    winGameWindow.SetActive(true);
    //}
    //IEnumerator ActiveLoseWindow()
    //{
    //    yield return new WaitForSeconds(2);
    //    loseGameWindow.SetActive(true);
    //}
}