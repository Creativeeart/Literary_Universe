using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_01 : MonoBehaviour
{
    public GameObject DownBlock;
    public Transform RezinaRope_Parent;
    public Vector3 StartPos_Ship_Parent;
    public Vector3 StartRot_Ship_Parent;
    public GameObject Ship_Parent;
    public GameObject Ship;
    public float MaxStamina = 5;
    public float CurrentStamina = 0;
    public Vector3 com;

    public SkinnedMeshRenderer Rezina;
    float Tension;
    public Transform RezinaTransform;
    Vector3 RezinaNearLocalPosition;
    public Vector3 RezinaFarLocalPosition;
    public AnimationCurve RezinaReturnAnimation;

    public GameObject StopperDown;
    public float ReturnTime;
    public SwitchCamOnTrigger _switchCamOnTrigger;
    public TouchKey _touchKey;
    public ParticleSystem metalHit;
    public AudioSource audioSource;
    public AudioClip vagonetkaClip, KnockWall;

    public Slider SliderStamina;
    public GameObject key;
    public GameObject keyHolder;
    public GameObject keyRotationCenter;
    public GameObject ObjectForCam2View;
    public GameObject camViewKey;
    public GameObject camFollowShip;
    public bool isTouchKey = false;
    public bool slowTime = false;
    public float multiplePower = 150.0f;
    float launchForce = 0;
    public bool isReady = true;

    bool IsStaminaLose = false;

    public static Game_01 Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        StartPos_Ship_Parent = Ship_Parent.transform.position;
        StartRot_Ship_Parent = Ship_Parent.transform.localEulerAngles;
        SliderStamina.minValue = 0;
        SliderStamina.maxValue = MaxStamina;
        SliderStamina.value = MaxStamina;
        RezinaNearLocalPosition = RezinaTransform.localPosition;
        //Ship.GetComponent<Rigidbody>().centerOfMass = com;
        CurrentStamina = MaxStamina;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Ship.GetComponent<Rigidbody>().AddTorque(Vector3.left * 200, ForceMode.Impulse);
        }
        if (isReady)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CurrentStamina = MaxStamina;
                IsStaminaLose = false;
            }
            if (Input.GetMouseButton(1) && !IsStaminaLose)
            {
                DownBlock.SetActive(false);
                StopperDown.SetActive(false);
                Ship.GetComponent<Rigidbody2D>().simulated = false;
                if (Tension < 1f)
                {
                    Tension += Time.deltaTime;
                }
                //Ship.transform.SetParent(RezinaRope_Parent);
                //Ship_Parent.transform.localEulerAngles = new Vector3(Ship_Parent.transform.localEulerAngles.x, Ship_Parent.transform.localEulerAngles.y, -Tension * 3.5f);
                Ship_Parent.transform.localPosition = new Vector3(Ship_Parent.transform.localPosition.x, Ship_Parent.transform.localPosition.y, Tension * 2f);
                RezinaTransform.localPosition = Vector3.Lerp(RezinaNearLocalPosition, RezinaFarLocalPosition, Tension);
                CurrentStamina -= Time.deltaTime;
                SliderStamina.value = CurrentStamina;
                if (CurrentStamina > 0)
                {
                    Rezina.material.color = Color.Lerp(Color.red, "262626FF".ToColor(), CurrentStamina / MaxStamina);
                }
                if (CurrentStamina <= 0)
                {
                    IsStaminaLose = true;
                    CurrentStamina = MaxStamina;
                    if (Tension > 0.1f)
                    {
                        StartShip();
                    }
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                if (Tension > 0.1f)
                {
                    StartShip();
                }
            }
        }
        launchForce = (RezinaTransform.localPosition.z / 2) * multiplePower;
    }

    public IEnumerator RopeReturn(bool Shooting)
    {
        if (!Shooting)
        {
            RezinaTransform.localPosition = new Vector3(RezinaFarLocalPosition.x, RezinaFarLocalPosition.y, RezinaFarLocalPosition.z);
        }
        Vector3 startLocalPosition = RezinaTransform.localPosition;
        for (float f = 0; f < 1f; f += Time.deltaTime / ReturnTime)
        {
            RezinaTransform.localPosition = Vector3.LerpUnclamped(startLocalPosition, RezinaNearLocalPosition, RezinaReturnAnimation.Evaluate(f));
            yield return null;
        }
        RezinaTransform.localPosition = RezinaNearLocalPosition;
    }
    public void StartShip()
    {
        Ship_Parent.transform.position = StartPos_Ship_Parent;
        Ship_Parent.transform.localEulerAngles = StartRot_Ship_Parent;
        StartCoroutine(RopeReturn(true));
        Tension = 0;
        Rezina.material.color = "262626FF".ToColor();
        Ship.GetComponent<Rigidbody2D>().simulated = true;
        Ship.GetComponent<Rigidbody2D>().AddTorque(launchForce, ForceMode2D.Impulse);
        //Ship.GetComponent<Rigidbody>().AddTorque(Vector3.right * launchForce, ForceMode.Impulse);
        isReady = false;
        audioSource.Stop();
        audioSource.PlayOneShot(vagonetkaClip);
        CurrentStamina = MaxStamina;
        SliderStamina.value = MaxStamina;
        StartCoroutine(EnableBlockAfterShot());
    }
    IEnumerator EnableBlockAfterShot()
    {
        yield return new WaitForSeconds(0.5f);
        DownBlock.SetActive(true);
    }
    public void DownShip()
    {
        CurrentStamina = MaxStamina;
        SliderStamina.value = MaxStamina;
        StopperDown.SetActive(true);

        audioSource.Stop();
        audioSource.PlayOneShot(KnockWall);
        audioSource.PlayOneShot(vagonetkaClip);
        //Ship.GetComponent<Rigidbody>().AddTorque(Vector3.left * 200, ForceMode.Impulse);
    }
}

