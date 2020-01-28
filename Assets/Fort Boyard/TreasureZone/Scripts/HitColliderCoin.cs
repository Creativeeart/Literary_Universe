using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HitColliderCoin : MonoBehaviour
{
    public AlertUI alertUI;
    public Slider slider;
    public Camera treasureCam;
    public TextMeshProUGUI text;
    public TextMeshProUGUI maxTextCoins;
    public float maxCoins = 100;
    public float currentCoins = 0;
    public GameObject roundHitParticle;
    public AudioSource audioSource;

    public Button startButton;
    public Image readyImage;
    public bool isReady = true;
    public GameObject[] goldFilleds;
    private void Start()
    {
        text.text = "0";
        slider.maxValue = maxCoins;
    }
    void Update()
    {
        if (isReady == false) readyImage.fillAmount -= Time.deltaTime * 0.5f;
        else readyImage.fillAmount += Time.deltaTime * 0.5f;
        if (readyImage.fillAmount == 1) startButton.interactable = true;
        if (readyImage.fillAmount == 0) isReady = true;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = treasureCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.GetComponent<Rigidbody>() != null)
                {
                    if (hit.collider.tag == "CoinHit")
                    {
                        if (slider.value < maxCoins)
                        {
                            if (isReady)
                            {
                                GameObject ins = Instantiate(roundHitParticle, hit.collider.gameObject.transform.position, Quaternion.identity);
                                Destroy(ins, 4f);
                                Destroy(hit.collider.gameObject);
                                audioSource.Play();
                                int randomNumber = hit.transform.GetComponent<RandomNumberCoins>().randomNumber;
                                Debug.Log(randomNumber);
                                currentCoins += randomNumber;
                                slider.value += randomNumber;
                                text.text = currentCoins.ToString();
                                maxTextCoins.text = slider.value.ToString() + " / " + maxCoins.ToString();
                            }
                            else
                            {
                                alertUI.ShowWarningModalWindow("Ждите. Золото высыпается");
                            }
                        }
                        else
                        {
                            alertUI.ShowWarningModalWindow("Вы не можете взять больше. Вам нужно высыпать золото в весы!");
                        }
                    }
                }
            }
        }
    }

    public void PourTheCoins()
    {
        slider.value = 0;
        maxTextCoins.text = slider.value.ToString() + " / " + maxCoins.ToString();
        isReady = false;
        if (currentCoins >= 100)  goldFilleds[0].SetActive(true);
        if (currentCoins >= 200)  goldFilleds[1].SetActive(true);
        if (currentCoins >= 300)  goldFilleds[2].SetActive(true);
        if (currentCoins >= 400)  goldFilleds[3].SetActive(true);
        if (currentCoins >= 500)  goldFilleds[4].SetActive(true);
        if (currentCoins >= 600)  goldFilleds[5].SetActive(true);
    }
}
