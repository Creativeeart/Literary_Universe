using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CheckContactStars : MonoBehaviour {
    public string fio;
    public int age;
    public string activity;
    public string favorite_book;
    public string about;
    public TextMeshProUGUI nameUI;
    public TextMeshProUGUI otherInfoUI;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Star Contact");
        //transform.position = Vector3.zero;
        //transform.position = new Vector3(transform.position.x + Random.Range(-3,3), transform.position.y + Random.Range(-3, 3), transform.position.z + Random.Range(-3, 3));
    }
    private void Update()
    {
        //ExplosionDamage(transform.position, 3);
    }
    private void Start()
    {
        nameUI.text = fio;
        string tempText =
            "<color=white>Возраст:</color>" + age + "\n" +
            "<color=white>Деятельность:</color>" + activity + "\n" +
            "<color=white>Любимые книги:</color>" + favorite_book + "\n" +
            "<color=white>О себе:</color>" + about + "\n";
        otherInfoUI.text = tempText;
    }
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log(hitColliders[i].name);
            i++;
        }
    }
}
