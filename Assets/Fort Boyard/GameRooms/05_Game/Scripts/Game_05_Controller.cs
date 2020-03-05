using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Game_05_Controller : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip lampPickSound;
    public AudioClip toggleTumblerSound;
    public AudioClip tikTakTimer;
    public AudioClip gameOverSound;
    public Animator safeGlassAnimator;
    public Animator keyAnimator;
    public GameObject drawGameWindow;
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject startBtnGameObject;
    public Animator camAnim;
    public GameObject blackScreenCanvas;
    public Animator blackScreen;
    public GameObject accepBtn;
    public TextMeshProUGUI rememberTimeUI;
    public float rememberTime = 20f;
    public float rememberTimeTemp;

    [Header("Main bulbs")]
    public GameObject[] mainBulbs;

    [Header("Player bulbs & tumblers")]
    public bool isActivateTumbler = false;
    public GameObject[] playerBulbs;
    public GameObject[] playerTumblers;
    public GameObject[] playerLines;
    public Material defaultTumbler;
    public Material emissionTumbler;

    [Header("Computer bulbs & tumblers")]
    public GameObject[] computerBulbs;
    public GameObject[] computerTumblers;
    public GameObject[] computerLines;

    [Header("Materials")]
    public Material LineDefaultMaterial;
    public Material LineRedMaterial;
    public Material LineGreenMaterial;
    ////////////////////////////////
    public Material defaultMaterial;
    public Material[] materials;
    public Material[] randomMaterials;
    public Material[] computerInputMaterials;

    [Header("Computer Settings")]
    public float startPercentage = 99f;
    public int easyToRemember = 5;
    public float chanceDecrease = 5f;

    [Space]
    public int countRotateTumblers = 0;
    int status = 0;

    public int maxRepeat = 2;

    public int[] randomNumbers;

    public static Game_05_Controller Instance { get; private set; }
    FortBoyardGameController FortBoyardGameController;


    public void Awake()
    {
        Instance = this;
    }

    public void ToggleTumblerSound()
    {
        _audioSource.PlayOneShot(toggleTumblerSound);
    }

    int[] Randomize(int arraySize, int min, int max, int maxRepeat)
    {
        int[] rands = new int[arraySize];
        int countRepeates = 1;
        for (int i = 0; i < rands.Length; i++)
        {
            rands[i] = Random.Range(min, max);
            if (i > 0)
            {
                if (rands[i - 1] == rands[i])
                {
                    countRepeates++;
                    if (countRepeates - 1 > maxRepeat)
                    {
                        Debug.Log("Повтор числа: " + rands[i] + " " + countRepeates + " раз. Повторилось больше допустимого");
                        while (rands[i - 1] == rands[i])
                        {
                            rands[i] = Random.Range(min, max);
                        }
                    }
                }
                else
                {
                    countRepeates = 1;
                }
            }
        }
        return rands;
    }

    void Start()
    {
        FortBoyardGameController = FortBoyardGameController.Instance;
        rememberTimeTemp = rememberTime;
        randomMaterials = new Material[mainBulbs.Length];
        computerInputMaterials = new Material[mainBulbs.Length];
        gameObject.AddComponent<AudioSource>();
        _audioSource = gameObject.GetComponent<AudioSource>();

    }
    public void StartGame()
    {
        StartCoroutine(MainRandomColors());
        startBtnGameObject.SetActive(false);
    }

    IEnumerator MainRandomColors()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            for (int j = 0; j < randomMaterials.Length; j++)
            {
                mainBulbs[j].GetComponent<MeshRenderer>().material = materials[i];
                _audioSource.pitch = 1.5f;
                _audioSource.PlayOneShot(lampPickSound);
                if (j > 0)
                    mainBulbs[j - 1].GetComponent<MeshRenderer>().material = defaultMaterial;
                yield return new WaitForSeconds(0.1f);
                mainBulbs[randomMaterials.Length - 1].GetComponent<MeshRenderer>().material = defaultMaterial;
            }
        }
        randomNumbers = Randomize(mainBulbs.Length, 0, materials.Length, maxRepeat);
        for (int i = 0; i < randomMaterials.Length; i++)
        {
            //int rand = Random.Range(0, materials.Length);
            randomMaterials[i] = materials[randomNumbers[i]];
            mainBulbs[i].GetComponent<MeshRenderer>().material = materials[randomNumbers[i]];
            _audioSource.pitch = 1.2f;
            _audioSource.PlayOneShot(lampPickSound);
            yield return new WaitForSeconds(0.5f);
        }
        // rand = 0, rand2 = 0, и если rand3 = 0 то заново рандомим
        _audioSource.pitch = 1;
        _audioSource.PlayOneShot(tikTakTimer);
        while (rememberTime > 0)
        {
            rememberTime -= Time.deltaTime;
            int tempTime = (int)rememberTime;
            rememberTimeUI.text = tempTime.ToString();
            if (rememberTime <= 0)
            {
                for (int i = 0; i < randomMaterials.Length; i++)
                {
                    mainBulbs[i].GetComponent<MeshRenderer>().material = defaultMaterial;
                    rememberTime = 0;
                }
            }
            yield return null;
        }
        if (rememberTime <= 0)
        {
            _audioSource.Stop();
        }
        isActivateTumbler = true;
        StartCoroutine(ComputerInputColors());
    }

    IEnumerator ComputerInputColors()
    {
        for (int i = 0; i < randomMaterials.Length; i++)
        {
            float randValue = Random.Range(0, 100f);
            float decrease = Mathf.Clamp(i - easyToRemember, 0, int.MaxValue) * chanceDecrease;
            float randLimit = Mathf.Clamp(startPercentage - decrease, 0, 100f);

            if (randValue < randLimit)
            {
                computerBulbs[i].GetComponent<MeshRenderer>().material = randomMaterials[i];
                computerInputMaterials[i] = randomMaterials[i];
                Debug.Log("Запомнил! " + i + ", " + randLimit + "%");
            }
            else
            {
                int rand = Random.Range(0, materials.Length);
                computerBulbs[i].GetComponent<MeshRenderer>().material = materials[rand];
                computerInputMaterials[i] = materials[rand];
                Debug.Log("Не уверен! " + i + ", " + randLimit + "%");
            }
            for (int j = 0; j < materials.Length; j++)
            {
                if (computerInputMaterials[i].name == materials[j].name)
                {
                    int angle = 0;
                    if (j == 0) angle = 90;
                    if (j == 1) angle = 180;
                    if (j == 2) angle = 270;
                    computerTumblers[i].transform.Rotate(new Vector3(computerTumblers[i].transform.rotation.x, angle, computerTumblers[i].transform.rotation.z));
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void CheckSumm()
    {
        countRotateTumblers = 0;
        for (int i = 0; i < playerTumblers.Length; i++)
        {
            if (playerTumblers[i].GetComponent<RotateTumbler>().isRotateTumbler)
            {
                countRotateTumblers++;
            }
        }
        if (countRotateTumblers == playerTumblers.Length)
        {
            accepBtn.SetActive(true); //Все тумблеры прокручены
        }
    }
    IEnumerator OpenedColors()
    {
        camAnim.enabled = true;
        camAnim.SetBool("StartPos", false);
        yield return new WaitForSeconds(1);
        //Camera.main.orthographic = true;
        //Camera.main.orthographicSize = 2.43f;
        //yield return new WaitForSeconds(2);
        for (int i = 0; i < randomMaterials.Length; i++)
        {
            mainBulbs[i].GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        status = 0;  //0 - Победил игрок; 1 - Победил компьютер; 2 - Ничья(оба неправильно); 3 - Ничья(оба правильно)

        string MainBulbMaterialName = string.Empty;
        string PlayerBulbMaterialName = string.Empty;
        string CmputerBulbMaterialName = string.Empty;

        for (int i = 0; i < randomMaterials.Length; i++)
        {
            mainBulbs[i].GetComponent<MeshRenderer>().material = randomMaterials[i];
            _audioSource.pitch = 1.2f;
            _audioSource.PlayOneShot(lampPickSound);

            MainBulbMaterialName = mainBulbs[i].GetComponent<MeshRenderer>().material.name;
            PlayerBulbMaterialName = playerBulbs[i].GetComponent<MeshRenderer>().material.name;
            CmputerBulbMaterialName = computerBulbs[i].GetComponent<MeshRenderer>().material.name;

            if ((MainBulbMaterialName == PlayerBulbMaterialName) && (MainBulbMaterialName != CmputerBulbMaterialName))
            {
                playerLines[i].GetComponent<MeshRenderer>().material = LineGreenMaterial;
                computerLines[i].GetComponent<MeshRenderer>().material = LineRedMaterial;
                status = 0;
                break;
            }
            else if ((MainBulbMaterialName != PlayerBulbMaterialName) && (MainBulbMaterialName == CmputerBulbMaterialName))
            {
                playerLines[i].GetComponent<MeshRenderer>().material = LineRedMaterial;
                computerLines[i].GetComponent<MeshRenderer>().material = LineGreenMaterial;
                status = 1;
                break;
            }
            else if ((MainBulbMaterialName != PlayerBulbMaterialName) && (MainBulbMaterialName != CmputerBulbMaterialName))
            {
                playerLines[i].GetComponent<MeshRenderer>().material = LineRedMaterial;
                computerLines[i].GetComponent<MeshRenderer>().material = LineRedMaterial;
                status = 2;
                //continue;
            }
            else if ((MainBulbMaterialName == PlayerBulbMaterialName) && (MainBulbMaterialName == CmputerBulbMaterialName))
            {
                playerLines[i].GetComponent<MeshRenderer>().material = LineGreenMaterial;
                computerLines[i].GetComponent<MeshRenderer>().material = LineGreenMaterial;
                if (i == mainBulbs.Length - 1)
                {
                    status = 3;
                    break;
                }
            }
            yield return new WaitForSeconds(1f);
        }
        switch (status)
        {
            case 0:
                Debug.Log("Победил игрок");
                _audioSource.PlayOneShot(gameOverSound);
                StartCoroutine(ActiveWinWindow());
                break;
            case 1:
                _audioSource.PlayOneShot(gameOverSound);
                Debug.Log("Победил компьютер");
                StartCoroutine(ActiveLoseWindow());
                break;
            case 2:
                _audioSource.PlayOneShot(gameOverSound);
                Debug.Log("Ничья - оба неправильно"); //Начать заново
                StartCoroutine(ActiveDrawWindow());
                break;
            case 3:
                _audioSource.PlayOneShot(lampPickSound);
                Debug.Log("Ничья - польностью оба правильно"); //Начать заново
                StartCoroutine(ActiveDrawWindow());
                break;
        }
    }
    IEnumerator ActiveDrawWindow() //Открыть окно ничьи
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
        FortBoyardGameController.LoseRoom("Мастер победил!\nК сожалению вы не справились с испытанием");
    }

    public void GameWinner()
    {
        winGameWindow.SetActive(false);
        StartCoroutine(RestartGameCoroutine(false));
        StartCoroutine(WinnerAnimations());
    }
    IEnumerator WinnerAnimations()
    {
        yield return new WaitForSeconds(3);
        safeGlassAnimator.enabled = true;
        yield return new WaitForSeconds(2.2f);
        keyAnimator.SetBool("GameWinner", true);
        yield return new WaitForSeconds(2f);
        FortBoyardGameController.WinnerRoom("Tips");
    }
    IEnumerator RestartGameCoroutine(bool isRestart)
    {
        //Camera.main.orthographic = false;
        blackScreenCanvas.SetActive(true);
        blackScreen.enabled = true;
        yield return new WaitForSeconds(1);
        blackScreen.SetBool("BlackScreenOUT", true);

        camAnim.SetBool("StartPos", true);
        rememberTime = rememberTimeTemp;
        countRotateTumblers = 0;
        for (int i = 0; i < mainBulbs.Length; i++)
        {
            mainBulbs[i].GetComponent<MeshRenderer>().material = defaultMaterial;
            playerBulbs[i].GetComponent<MeshRenderer>().material = defaultMaterial;
            computerBulbs[i].GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        for (int i = 0; i < computerTumblers.Length; i++)
        {
            computerTumblers[i].transform.localEulerAngles = Vector3.zero;
        }
        if (isRestart)
            startBtnGameObject.SetActive(true);
        else
            startBtnGameObject.SetActive(false);
        accepBtn.SetActive(false);
        for (int i = 0; i < playerTumblers.Length; i++)
        {
            playerTumblers[i].GetComponent<RotateTumbler>().isRotateTumbler = false;
            playerTumblers[i].GetComponent<RotateTumbler>().RotateAngle = 0;
            playerTumblers[i].transform.localEulerAngles = Vector3.zero;
        }
        for (int i = 0; i < playerLines.Length; i++)
        {
            playerLines[i].GetComponent<MeshRenderer>().material = LineDefaultMaterial;
            computerLines[i].GetComponent<MeshRenderer>().material = LineDefaultMaterial;
        }
        yield return new WaitForSeconds(1);
        blackScreen.SetBool("BlackScreenOUT", false);
        blackScreenCanvas.SetActive(false);
    }
    public void ColorCheck()
    {
        StartCoroutine(OpenedColors());
        accepBtn.SetActive(false);
    }
}
