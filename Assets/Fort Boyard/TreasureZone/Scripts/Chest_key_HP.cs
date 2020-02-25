using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chest_key_HP : MonoBehaviour {
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

    void Start()
    {
        Chest.Instance.hpBar.gameObject.SetActive(false);
        currentHealth = maxHealth;
        tempCount = (100 / (float)currentHealth) / 100;
    }

    public void HitChestKey()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 1;
            hpBar.fillAmount -= tempCount;
            Chest.Instance.hpBarStatus.text = "Прочность замка: " + currentHealth.ToString() + "/" + maxHealth.ToString(); 
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
                if (isKeyOpen) Chest.Instance.countKeyOpened++;
                if (Chest.Instance.countKeyOpened == 5)
                {
                    Chest.Instance.roofChest.enabled = true;
                    if (Chest.Instance.roofChest.enabled)
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
	
    void OnMouseOver()
    {
        Chest.Instance.hpBar.gameObject.SetActive(true);
        Chest.Instance.hpBar.maxValue = maxHealth;
        Chest.Instance.hpBar.value = currentHealth;
        Chest.Instance.hpBar.minValue = 0;
        Chest.Instance.hpBarStatus.text = "Прочность замка: " + currentHealth.ToString() + "/" + maxHealth.ToString();
        for (int i = 0; i < transform.childCount-1; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }
    }
    void OnMouseExit()
    {
        Chest.Instance.hpBar.gameObject.SetActive(false);
        for (int i = 0; i < transform.childCount-1; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }

}
