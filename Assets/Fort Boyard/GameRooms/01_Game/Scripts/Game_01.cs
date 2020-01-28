using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace cakeslice
{
    public class Game_01 : MonoBehaviour {
        public FortBoyardGameController _fortBoyardGameController;
        public SwitchCamOnTrigger _switchCamOnTrigger;
        public TouchKey _touchKey;
        public ParticleSystem metalHit;
        public AudioSource audioSource;
        public AudioClip vagonetkaClip, KnockWall;
        public GameObject fakePhysics;
        public Button startButton;
        public Slider sliderPower;
        public Image readyImage;
        public GameObject key;
        public GameObject keyHolder;
        public GameObject keyRotationCenter;
        public GameObject ship;
        public GameObject camViewKey;
        public GameObject camFollowShip;
        public bool isTouchKey = false;
        public bool slowTime = false;
        bool isUp = true;
        public float minF = 1.0f, maxF = 2.0f, stepF = 0.05f, currentF = 0.0f, multiplePower = 150.0f, launchForce;
        public bool isReady = true;
        public bool isBounce = true;

        void Start() {
            sliderPower.minValue = minF;
            sliderPower.maxValue = maxF;
            sliderPower.value = maxF;
        }
        public static Vector3 ClampVector(Vector3 vec, float min, float max)
        {
            vec.x = Mathf.Clamp(vec.x, min, max);
            return vec;
        }

        void Update() {

            if (isReady == false)
            {
                readyImage.fillAmount -= Time.deltaTime * 2;
            }
            else
            {
                readyImage.fillAmount += Time.deltaTime * 2;
            }
            if (readyImage.fillAmount == 1)
            {
                startButton.interactable = true;
            }
            if (isBounce)
            {
                if (isUp)
                {
                    currentF += stepF;
                    if (currentF >= maxF) isUp = false;
                }
                else
                {
                    currentF -= stepF;
                    if (currentF <= minF) isUp = true;
                }
                sliderPower.value = currentF;
            }

            launchForce = currentF * multiplePower;
            if (Input.GetKeyDown(KeyCode.A))
            {
                fakePhysics.GetComponent<Rigidbody>().AddTorque(Vector3.left * 200, ForceMode.Impulse);
            }

        }
        public void StartShip()
        {
            fakePhysics.GetComponent<Rigidbody>().AddTorque(Vector3.right * launchForce, ForceMode.Impulse);
            isReady = false;
            isBounce = false;
            startButton.interactable = false;
            audioSource.Stop();
            audioSource.PlayOneShot(vagonetkaClip);
        }
        public void DownShip()
        {

            audioSource.Stop();
            audioSource.PlayOneShot(KnockWall);
            audioSource.PlayOneShot(vagonetkaClip);

            fakePhysics.GetComponent<Rigidbody>().AddTorque(Vector3.left * 200, ForceMode.Impulse);

        }
    }
}
