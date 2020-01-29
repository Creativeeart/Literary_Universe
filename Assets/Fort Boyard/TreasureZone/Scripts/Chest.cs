﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using cakeslice;
public class Chest : MonoBehaviour {
    public CameraShakeHitHammer cameraShakeHitHammer;
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
    public float timeRemainig = 60;
    public Vector3 offsetHammer;
    public float timeToFallCoin = 0.01f;
    public int countKeyOpened = 0;
    public static Chest Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (FortBoyardGameController.Instance.IsTreasureZone)
        {
            timeRemainig -= Time.deltaTime;
            timeToFallCoin -= Time.deltaTime;
            if (countKeyOpened != 5)
            {
                if (timeToFallCoin <= 0)
                {
                    coinsBoyard -= coinsFall;
                    timeToFallCoin = 0.01f;
                    allMoneyTMPro.text = "$ " + coinsBoyard.ToString();
                    allMoneyTMPro_shadow.text = allMoneyTMPro.text;
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
                                cameraShakeHitHammer.shakeDuration = 0.1f;
                                hit.transform.GetComponent<CameraShakeHitHammer>().shakeDuration = 0.1f;
                            }
                        }
                    }
                }
            }
        }
    }
}
