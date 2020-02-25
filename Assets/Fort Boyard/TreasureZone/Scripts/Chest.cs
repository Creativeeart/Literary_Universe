using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using cakeslice;
public class Chest : MonoBehaviour {
    public GameObject hammer;
    public GameObject hitParticle;
    public AudioClip hitToMetalSound;
    public AudioSource audioSource;
    public TextMeshProUGUI allMoneyTMPro, allMoneyTMPro_shadow;
    public TextMeshProUGUI hpBarStatus;
    public Slider hpBar;
    public Animator roofChest;
    public int coinsBoyard = 100000;
    public int coinsFall = 100;
    public Vector3 offsetHammer;
    public float RepeatRate = 0.1f;
    float timeToFallCoin = 0;
    public int countKeyOpened = 0;
    bool isReset = false;
    public static Chest Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (FortBoyardGameController.Instance.IsTreasureZone)
        {
            if (!isReset)
            {
                float summ = coinsBoyard * (FortBoyardGameController.Instance.totalTime / 100); //Подсчет монет в сундуке в зависимости от времени
                coinsBoyard = (int)summ;
                isReset = true;
            }
            if (countKeyOpened != 5)
            {
                timeToFallCoin += Time.deltaTime;
                if (coinsBoyard > 0)
                {
                    if (timeToFallCoin >= RepeatRate)
                    {
                        coinsBoyard -= coinsFall;
                        timeToFallCoin -= RepeatRate;
                        allMoneyTMPro.text = coinsBoyard.ToString();
                        allMoneyTMPro_shadow.text = allMoneyTMPro.text;
                    }
                }
                else
                {
                    coinsBoyard = 0;
                    FortBoyardGameController.Instance.ShowFailModal("Золото закончилось! К сожалению вы не справились с заданием.<br> \nВы можете вернуться в главное меню и попытаться еще раз!");
                    FortBoyardGameController.Instance.IsTreasureZone = false;

                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform.GetComponent<Rigidbody>() != null)
                    {
                        if (hit.collider.tag == "Chest_key")
                        {
                            hit.transform.GetComponent<Chest_key_HP>().HitChestKey();
                            if (!hit.transform.GetComponent<Chest_key_HP>().isFakeOpen)
                            {
                                var ins = Instantiate(hammer, hit.transform.position + offsetHammer, Quaternion.identity);
                                var insParticle = Instantiate(hitParticle, hit.transform.position, Quaternion.identity);
                                audioSource.PlayOneShot(hitToMetalSound);
                                Destroy(ins, 0.20f);
                                Destroy(insParticle, 1f);
                                ins.transform.LookAt(hit.transform.position);
                                //cameraShakeHitHammer.shakeDuration = 0.1f;
                                FB_CamMovingController.Instance.CameraShake();
                                hit.transform.GetComponent<CameraShakeHitHammer>().shakeDuration = 0.1f;
                            }
                        }
                    }
                }
            }
        }
    }
    //void Update()
    //{
    //    if (FortBoyardGameController.Instance.IsTreasureZone)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            RaycastHit hit;
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            if (Physics.Raycast(ray, out hit, 100f))
    //            {
    //                if (hit.transform.GetComponent<Rigidbody>() != null)
    //                {
    //                    if (hit.collider.tag == "Chest_key")
    //                    {
    //                        hit.transform.GetComponent<Chest_key_HP>().HitChestKey();
    //                        if (!hit.transform.GetComponent<Chest_key_HP>().isFakeOpen)
    //                        {
    //                            var ins = Instantiate(hammer, hit.transform.position + offsetHammer, Quaternion.identity);
    //                            var insParticle = Instantiate(hitParticle, hit.transform.position, Quaternion.identity);
    //                            audioSource.PlayOneShot(hitToMetalSound);
    //                            Destroy(ins, 0.20f);
    //                            Destroy(insParticle, 1f);
    //                            ins.transform.LookAt(hit.transform.position);
    //                            //cameraShakeHitHammer.shakeDuration = 0.1f;
    //                            FB_CamMovingController.Instance.CameraShake();
    //                            hit.transform.GetComponent<CameraShakeHitHammer>().shakeDuration = 0.1f;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
