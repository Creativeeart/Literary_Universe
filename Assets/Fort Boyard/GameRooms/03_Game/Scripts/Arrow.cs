using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace cakeslice
{
    public class Arrow : MonoBehaviour
    {
        public float speed = 5f;
        public float distanceRay = 10f;
        public Vector3 sdvigLucha;
        public Vector3 centerOfMass;
        public Bow bow;
        public Rigidbody Rigidbody;
        public TrailRenderer TrailRenderer;

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
            Rigidbody.centerOfMass = centerOfMass;
        }
        public void SetToRope(Transform ropeTransform)
        {
            Time.timeScale = 1f;
            transform.parent = ropeTransform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            //transform.LookAt(bow.aim);
            Rigidbody.isKinematic = true;
            Rigidbody.useGravity = false;
            TrailRenderer.enabled = false;
        }

        public void Shot(float velocity)
        {
            Time.timeScale = 1f;
            transform.parent = null;
            Rigidbody.isKinematic = false;
            Rigidbody.useGravity = true;
            Rigidbody.velocity = transform.forward * velocity;
            TrailRenderer.Clear();
            TrailRenderer.enabled = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Arrow")
            {
                Rigidbody.velocity = Vector3.zero;
                Rigidbody.isKinematic = true;
                Rigidbody.useGravity = false;
                bow.ArrowHit.pitch = Random.Range(1f, 1.2f);
                bow.ArrowHit.Play();
                if (other.name != "Target")
                {
                    Debug.Log("Miss");
                    bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Промах";
                    bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
                    var ins = Instantiate(bow.hitPrefab, transform.position, Quaternion.identity);
                    Destroy(ins, 1);
                    if (bow.ArrowIndex == 10)
                    {
                        Debug.Log("No arrows. You failed");
                        bow._fortBoyardGameController.LoseRoom("Стрелы закончились!\nК сожалению вы не справились с испытанием");
                        Cursor.visible = true;
                    }
                }
            }
            if (other.name == "Target")
            {
                other.transform.parent.GetComponent<TargetController>().isTargetHit = true;
                other.transform.parent.parent.GetComponent<Animator>().enabled = false;
                transform.parent = other.transform;
                bow.curentHitTargets++;
                Debug.Log("Hit");
                bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Попадание";
                bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 171, 0, 255);
                bow.countHitTargets.text = "Мишеней поражено: " + bow.curentHitTargets + "/5";
                var ins = Instantiate(bow.hitPrefab, new Vector3(other.transform.parent.transform.position.x, other.transform.parent.transform.position.y - 0.3f, other.transform.parent.transform.position.z), Quaternion.identity);
                Destroy(ins, 1);
                if (bow.curentHitTargets >= 5)
                {
                    Debug.Log("Done! All targets hits.");
                    bow.isWinner = true;
                    StartCoroutine(ShowCenterRotationKey());
                    Cursor.visible = true;
                }
                if (bow.ArrowIndex == 10 && bow.curentHitTargets < 5)
                {
                    bow._fortBoyardGameController.LoseRoom("Стрелы закончились!\nК сожалению вы не справились с испытанием");
                    Cursor.visible = true;
                }
            }
        }
        IEnumerator ShowCenterRotationKey()
        {
            yield return new WaitForSeconds(1);
            bow.keyRotationCenter.SetActive(true);
            yield return new WaitForSeconds(2);
            bow._fortBoyardGameController.WinnerRoom("Keys");
        }
    }
}
