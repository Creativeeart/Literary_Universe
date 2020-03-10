using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TargetHitArrow : MonoBehaviour {
    Bow Bow;
    FortBoyardGameController FortBoyardGameController;
    public void Start()
    {
        Bow = Bow.Instance;
        FortBoyardGameController = FortBoyardGameController.Instance;
    }

    void OnCollisionEnter(Collision other)
    {
        Bow.ArrowHit.pitch = Random.Range(1f, 1.2f);
        Bow.ArrowHit.Play();
        if (other.gameObject.tag == "Arrow") //Попадание
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            transform.parent.GetComponent<TargetController>().isTargetHit = true;
            transform.parent.parent.GetComponent<Animator>().enabled = false;
            other.transform.SetParent(transform);
            transform.GetComponent<Collider>().enabled = false;
            Bow.curentHitTargets++;
            Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Попадание";
            Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 171, 0, 255);
            Bow.countHitTargets.text = "Мишеней поражено: " + Bow.curentHitTargets + "/5";

            GameObject ins = Instantiate(Bow.hitPrefab, new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y - 0.3f, transform.parent.transform.position.z), Quaternion.identity);
            Destroy(ins, 1);
        }
        if (Bow.curentHitTargets >= 5) //Все цели поражены
        {
            Bow.isWinner = true;
            StartCoroutine(ShowCenterRotationKey());
            Cursor.visible = true;
        }
        if (Bow.ArrowsCount <= 0 && Bow.curentHitTargets < 5) //Закончились стрелы и мишени не поражены
        {
            FortBoyardGameController.LoseRoom("Стрелы закончились!\nК сожалению вы не справились с испытанием");
            Cursor.visible = true;
        }
    }

    
    IEnumerator ShowCenterRotationKey()
    {
        yield return new WaitForSeconds(1);
        Bow.keyRotationCenter.SetActive(true);
        yield return new WaitForSeconds(2);
        FortBoyardGameController.WinnerRoom("Keys");
    }
}
