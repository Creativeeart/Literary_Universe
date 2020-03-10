using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Arrow : MonoBehaviour
{
    //public float distanceRay = 1.5f;
    Vector3 forward, posForward;
    public Vector3 centerOfMass;
    public TrailRenderer TrailRenderer;

    Rigidbody RigidBody;
    Bow Bow;
    FortBoyardGameController FortBoyardGameController;

    Transform Trans;
    Vector3 vel, pos, plu;

    //private void Update()
    //{
    //    forward = transform.TransformDirection(Vector3.forward) * distanceRay;
    //    posForward = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //    Debug.DrawRay(posForward, forward, Color.yellow);
    //}

    public void Start()
    {
        Trans = transform;
        plu = new Vector3(0, 0, -2);
        Bow = Bow.Instance;
        FortBoyardGameController = FortBoyardGameController.Instance;
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.angularDrag = 0.5f;
        RigidBody.centerOfMass = centerOfMass;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = "#ffffffff".ToColor();
        Gizmos.DrawSphere(transform.position + centerOfMass, 0.01f);
    }

    public void SetToRope(Transform ropeTransform)
    {
        Time.timeScale = 1f;
        transform.parent = ropeTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //transform.LookAt(Bow.Instance.aim);
        if (RigidBody != null)
        {
            RigidBody.isKinematic = true;
            RigidBody.useGravity = false;
        }
        TrailRenderer.enabled = false;
    }



    public void Shot(float velocity)
    {
        Time.timeScale = 1f;
        transform.parent = null;
        RigidBody.isKinematic = false;
        RigidBody.useGravity = true;
        RigidBody.velocity = transform.forward * velocity;
        //RigidBody.AddForce(transform.forward * velocity);//-типа выстрел:)
        TrailRenderer.Clear();
        TrailRenderer.enabled = true;
    }



    void FixedUpdate()
    {
        //pos = Trans.position + Trans.TransformDirection(plu);//-мировая позиия оперения
        //vel = Trans.InverseTransformDirection(RigidBody.velocity);//-локальная скорость снаряда
        //vel.z = 0;//-убираем скорось направленную вперед/назад(относительно стрелы)-это ведь не парашют:)
        //vel = -vel / 1000;//-поворачиваем скорость на 180* (берем ее отрицатьное значение) и уменьшаем(чем больше делитель тем плавнее поворот)
        //vel = Trans.TransformDirection(vel);//-скорость торможения в мировой системе координат
        //RigidBody.AddForceAtPosition(vel, pos, ForceMode.VelocityChange);//-добавляем скорость торможения в позиции оперения
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
            if (other.tag == "Target") //Попадание
            {
                other.transform.parent.GetComponent<TargetController>().isTargetHit = true;
                other.transform.parent.parent.GetComponent<Animator>().enabled = false;
                transform.parent = other.transform;
                other.transform.GetComponent<Collider>().enabled = false;
                Collider[] colliders;
                colliders = gameObject.GetComponents<Collider>();
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].enabled = false;
                }
                Bow.curentHitTargets++;
                Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Попадание";
                Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 171, 0, 255);
                Bow.countHitTargets.text = "Мишеней поражено: " + Bow.curentHitTargets + "/5";

                GameObject ins = Instantiate(Bow.hitPrefab, new Vector3(other.transform.parent.transform.position.x, other.transform.parent.transform.position.y - 0.3f, other.transform.parent.transform.position.z), Quaternion.identity);
                Destroy(ins, 1);
            }
            else //Промах
            {
                Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Промах";
                Bow.hitPrefab.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
                GameObject ins = Instantiate(Bow.Instance.hitPrefab, transform.position, Quaternion.identity);
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
        
    }
    IEnumerator ShowCenterRotationKey()
    {
        yield return new WaitForSeconds(1);
        Bow.keyRotationCenter.SetActive(true);
        yield return new WaitForSeconds(2);
        FortBoyardGameController.WinnerRoom("Keys");
    }
}

