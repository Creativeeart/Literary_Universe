using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chest_key_HP : MonoBehaviour {
    public GameObject one;
    public GameObject two;
    public Chest chest;
    public int maxHealth = 20;
    public int currentHealth = 0;
    public Animator basePartKey;
    public Animator steelPartKey;
    public Animator dynamicPartKey;
    public Image hpBar;
    float tempCount = 0;
    public bool isKeyOpen = false;
    public bool isFakeOpen = false;
    // Use this for initialization
    
    public void HitChestKey()
    {
        if (currentHealth > 0) {
            currentHealth -= 1;
            hpBar.fillAmount -= tempCount;
            chest.hpBarStatus.text = "Прочность замка: " + currentHealth.ToString() + "/" + maxHealth.ToString(); 
        }
        if (!isKeyOpen)
        {
            if (currentHealth <= 0)
            {
                //Debug.Log("Key crash");
                isKeyOpen = true;
                StartCoroutine(Fake());
                basePartKey.enabled = true;
                steelPartKey.enabled = true;
                dynamicPartKey.enabled = true;
                if (isKeyOpen)
                    chest.countKeyOpened++;
                if (chest.countKeyOpened == 5)
                {
                    chest.roofChest.enabled = true;
                    if (chest.roofChest.enabled)
                    {
                        TimerGame.Instance.RunTime = false;
                        StartCoroutine(TreasureZoneController.Instance.Spawn());
                    }
                }
            }
        }
    }
    IEnumerator Fake() //Чтобы последняя анимация молота успевала проигрываться
    {
        yield return new WaitForSeconds(0.1f);
        isFakeOpen = true;
    }
	void Start () {
        chest.hpBar.gameObject.SetActive(false);
        currentHealth = maxHealth;
        tempCount = (100 / (float)currentHealth) / 100;
    }
    void OnMouseOver()
    {
        chest.hpBar.gameObject.SetActive(true);
        chest.hpBar.maxValue = maxHealth;
        chest.hpBar.value = currentHealth;
        chest.hpBar.minValue = 0;
        chest.hpBarStatus.text = "Прочность замка: " + currentHealth.ToString() + "/" + maxHealth.ToString();
        one.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        two.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }
    void OnMouseExit()
    {
        chest.hpBar.gameObject.SetActive(false);
        one.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        two.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }

}
