using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Arrow : MonoBehaviour
{
    public float speed = 5f;
    public float distanceRay = 10f;
    public Vector3 sdvigLucha;
    public Vector3 centerOfMass;
    Rigidbody RigidBody;
    public TrailRenderer TrailRenderer;
    Bow Bow;
    FortBoyardGameController FortBoyardGameController;
    private void Update()
    {
        Vector3 down = transform.TransformDirection(Vector3.forward) * distanceRay;
        Debug.DrawRay(transform.position, down, Color.yellow);
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, down, out hit, distanceRay))
        //{
        //    if ((hit.transform.name == "Target") && isShooting)
        //    {
        //        Rigidbody.isKinematic = true;
        //        transform.parent = hit.transform;
        //        hit.transform.parent.GetComponent<TargetController>().isActive = true;
        //        hit.transform.parent.parent.GetComponent<Animator>().enabled = false;
        //        transform.localPosition = hit.transform.localPosition - new Vector3(0,0,distanceRay-1f);
        //        isShooting = false;
        //        Debug.Log("RayCast: " + hit.transform.name);
        //    }

        //}
    }
    public void Start()
    {
        Bow = Bow.Instance;
        FortBoyardGameController = FortBoyardGameController.Instance;
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.centerOfMass = centerOfMass;
    }
    public void SetToRope(Transform ropeTransform)
    {
        Time.timeScale = 1f;
        transform.parent = ropeTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //transform.LookAt(Bow.Instance.aim);
        RigidBody.isKinematic = true;
        RigidBody.useGravity = false;
        TrailRenderer.enabled = false;
    }

    public void Shot(float velocity)
    {
        Time.timeScale = 1f;
        transform.parent = null;
        RigidBody.isKinematic = false;
        RigidBody.useGravity = true;
        RigidBody.velocity = transform.forward * velocity;
        TrailRenderer.Clear();
        TrailRenderer.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Arrow")
        {
            RigidBody.velocity = Vector3.zero;
            RigidBody.isKinematic = true;
            RigidBody.useGravity = false;
            Bow.ArrowHit.pitch = Random.Range(1f, 1.2f);
            Bow.ArrowHit.Play();
            if (other.name != "Target")
            {
                Debug.Log("Miss");
                Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Промах";
                Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
                var ins = Instantiate(Bow.Instance.hitPrefab, transform.position, Quaternion.identity);
                Destroy(ins, 1);
                if (Bow.ArrowIndex == 10)
                {
                    Debug.Log("No arrows. You failed");
                    FortBoyardGameController.LoseRoom("Стрелы закончились!\nК сожалению вы не справились с испытанием");
                    Cursor.visible = true;
                }
            }
        }
        if (other.name == "Target")
        {
            other.transform.parent.GetComponent<TargetController>().isTargetHit = true;
            other.transform.parent.parent.GetComponent<Animator>().enabled = false;
            transform.parent = other.transform;
            Collider[] colliders;
            colliders = gameObject.GetComponents<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
            Bow.curentHitTargets++;
            Debug.Log("Hit");
            Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Попадание";
            Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 171, 0, 255);
            Bow.countHitTargets.text = "Мишеней поражено: " + Bow.curentHitTargets + "/5";
            
            var ins = Instantiate(Bow.hitPrefab, new Vector3(other.transform.parent.transform.position.x, other.transform.parent.transform.position.y - 0.3f, other.transform.parent.transform.position.z), Quaternion.identity);
            Destroy(ins, 1);
            if (Bow.curentHitTargets >= 5)
            {
                Debug.Log("Done! All targets hits.");
                Bow.isWinner = true;
                StartCoroutine(ShowCenterRotationKey());
                Cursor.visible = true;
            }
            if (Bow.ArrowIndex == 10 && Bow.curentHitTargets < 5)
            {
                FortBoyardGameController.LoseRoom("Стрелы закончились!\nК сожалению вы не справились с испытанием");
                Cursor.visible = true;
            }
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

