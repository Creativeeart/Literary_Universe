using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace cakeslice
{
    public class Game_06_Controller : MonoBehaviour
    {
        public GameObject ArrowOne, ArrowTwo;
        [Header("Main Settings")]
        public FortBoyardGameController fortBoyardGameController;
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
        [HideInInspector]
        public bool isStopUserTime = false;
        [HideInInspector]
        public bool isStopComputerTime = false;
        [HideInInspector]
        public bool isUserCaseOpen = false;
        [HideInInspector]
        public bool isComputerCaseOpen = false;

        public void StartGameWithButton()
        {
            StartCoroutine(CountDown());
        }
        private void Start()
        {

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
                countDownTimer.text = "<size=120>Старт!</size>";
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
                countDownTimer.enabled = false;
            }
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
                ArrowOne.SetActive(true);
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
                    StartCoroutine(ActiveLoseWindow());
                }
                else if (computer > user)
                {
                    Debug.Log("User Winner!");
                    bulbComputer.GetComponent<MeshRenderer>().material = redMaterial;
                    bulbUser.GetComponent<MeshRenderer>().material = greenMaterial;
                    StartCoroutine(ActiveWinWindow());
                }
                else
                {
                    Debug.Log("Draw!");
                    bulbComputer.GetComponent<MeshRenderer>().material = redMaterial;
                    bulbUser.GetComponent<MeshRenderer>().material = redMaterial;
                    StartCoroutine(ActiveDrawWindow());
                }
            }
        }
        IEnumerator WinnerAnimations()
        {
            yield return new WaitForSeconds(3);
            safeGlassAnimator.enabled = true;
            yield return new WaitForSeconds(2.2f);
            keyAnimator.SetBool("GameWinner", true);
            yield return new WaitForSeconds(2f);
            fortBoyardGameController.WinnerRoom("Tips");
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
            StartCoroutine(RestartGameCoroutine(false));
            fortBoyardGameController.LoseRoom("Мастер победил!\nК сожалению вы не справились с испытанием");
        }

        public void GameWinner()
        {
            winGameWindow.SetActive(false);
            StartCoroutine(RestartGameCoroutine(false));
            StartCoroutine(WinnerAnimations());
        }
        IEnumerator RestartGameCoroutine(bool isRestart)
        {
            blackScreenCanvas.SetActive(true);
            blackScreen.enabled = true;
            yield return new WaitForSeconds(1);
            blackScreen.SetBool("BlackScreenOUT", true);

            camAnim.SetBool("StartPos", true);

            yield return new WaitForSeconds(1);
            blackScreen.SetBool("BlackScreenOUT", false);
            blackScreenCanvas.SetActive(false);
        }
        IEnumerator ActiveDrawWindow()
        {
            yield return new WaitForSeconds(2);
            drawGameWindow.SetActive(true);
        }
        IEnumerator ActiveWinWindow()
        {
            yield return new WaitForSeconds(2);
            winGameWindow.SetActive(true);
        }
        IEnumerator ActiveLoseWindow()
        {
            yield return new WaitForSeconds(2);
            loseGameWindow.SetActive(true);
        }
    }
}