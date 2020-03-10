using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bow : MonoBehaviour
{
    public Camera mainCam;
    public int MaxArrows = 10;
    public float ReturnTime;
    public float ArrowSpeed;

    [HideInInspector]
    public int ArrowsCount = 0;
    [HideInInspector]
    public float curentHitTargets = 0;
    [HideInInspector]
    public bool isWinner = false;

    public TextMeshProUGUI countArrow, countHitTargets;
    
    public GameObject hitPrefab;
    public GameObject Arrow;
    public GameObject keyRotationCenter;

    public Transform RopeTransform;
    
    public Vector3 RopeFarLocalPosition;
    public AnimationCurve RopeReturnAnimation;

    public AudioSource BowTensionAudioSource;
    public AudioSource BowWhistlingAudioSource;
    public AudioSource ArrowHit;

    
    float Tension;
    bool isShoot = false;
    GameObject CurrentArrow;
    Vector3 RopeNearLocalPosition;
    
    public static Bow Instance { get; private set; }
    FortBoyardGameController FortBoyardGameController;

    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        FortBoyardGameController = FortBoyardGameController.Instance;
        ArrowsCount = MaxArrows;
        RopeNearLocalPosition = RopeTransform.localPosition;
    }

    void Update()
    {
        if (!FortBoyardGameController.IsRoomPause)
        {
            if (!isWinner)
            {
                if (ArrowsCount > 0)
                {                    
                    if (Input.GetMouseButtonDown(0)) //Нажимаем один раз ЛЕВУЮ клавишу мыши
                    {
                        isShoot = false;
                        BowTensionAudioSource.pitch = Random.Range(0.8f, 1.2f);
                        BowTensionAudioSource.Play();
                        CurrentArrow = Instantiate(Arrow); //Взяли стрелу
                    }

                    if (Input.GetMouseButton(0)) //Удерживаем ЛЕВУЮ клавишу мыши
                    {
                        if (Tension < 1f) Tension += Time.deltaTime / 1.4f;
                        RopeTransform.localPosition = Vector3.Lerp(RopeNearLocalPosition, RopeFarLocalPosition, Tension); //Натянули стрелу
                        CurrentArrow.GetComponent<Arrow>().SetToRope(RopeTransform); //Помещение стрелы в тетиву
                    }

                    if (Input.GetMouseButtonUp(0)) //Отпустили ЛЕВУЮ клавишу мыши
                    {
                        Shoot();
                    }
                }
            }
        }
    }
    void Shoot()
    {
        if (Tension > 0.1f)
        {
            isShoot = true;
            ArrowsCount--;
            countArrow.text = "Осталось стрел: " + ArrowsCount;
            StartCoroutine(RopeReturn());
            CurrentArrow.GetComponent<Arrow>().Shot(ArrowSpeed * Tension);
            Tension = 0;
            BowTensionAudioSource.Stop();
            BowWhistlingAudioSource.pitch = Random.Range(0.8f, 1.2f);
            BowWhistlingAudioSource.Play();
            BowTensionAudioSource.Stop();
        }
        else
        {
            AbortShoot();
        }
    }

    void AbortShoot()
    {
        if (!isShoot)
        {
            Tension = 0;
            RopeTransform.localPosition = Vector3.Lerp(RopeNearLocalPosition, RopeFarLocalPosition, Tension);
            Destroy(CurrentArrow);
        }
    }
    public IEnumerator RopeReturn()
    {
        Vector3 startLocalPosition = RopeTransform.localPosition;
        for (float f = 0; f < 1f; f += Time.deltaTime / ReturnTime)
        {
            RopeTransform.localPosition = Vector3.LerpUnclamped(startLocalPosition, RopeNearLocalPosition, RopeReturnAnimation.Evaluate(f));
            yield return null;
        }
        RopeTransform.localPosition = RopeNearLocalPosition;
    }
}

