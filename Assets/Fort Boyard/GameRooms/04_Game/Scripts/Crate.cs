using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Crate : MonoBehaviour
{
    Vector3 forward, back, left, right;
    Vector3 posForward, posBack, posLeft, posRight;


    void RaycastCustom()
    {
        forward = transform.TransformDirection(Vector3.forward) * CrateController.Instance.distanceRay;
        back = transform.TransformDirection(Vector3.back) * CrateController.Instance.distanceRay;
        left = transform.TransformDirection(Vector3.left) * CrateController.Instance.distanceRay;
        right = transform.TransformDirection(Vector3.right) * CrateController.Instance.distanceRay;

        posForward = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z + 1f);
        posBack = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - 1f);
        posLeft = new Vector3(transform.position.x - 1, transform.position.y + 1f, transform.position.z);
        posRight = new Vector3(transform.position.x + 1, transform.position.y + 1f, transform.position.z);

        Debug.DrawRay(posForward, forward, Color.yellow);
        Debug.DrawRay(posBack, back, Color.red);
        Debug.DrawRay(posLeft, left, Color.blue);
        Debug.DrawRay(posRight, right, Color.green);
    }

    void RaycastHit()
    {
        if (CrateController.Instance.steps > 0)
        {
            if (gameObject.tag == "Crate")
            {
                if (!CrateController.Instance.moving)
                {
                    RaycastCustom();
                    RaycastHit hit;
                    if (!Physics.Raycast(posForward, forward, out hit, CrateController.Instance.distanceRay))
                    {
                        StartCoroutine(CrateMoving("Forward"));
                    }

                    if (!Physics.Raycast(posBack, back, out hit, CrateController.Instance.distanceRay))
                    {
                        StartCoroutine(CrateMoving("Back"));
                    }

                    if (!Physics.Raycast(posLeft, left, out hit, CrateController.Instance.distanceRay))
                    {
                        StartCoroutine(CrateMoving("Left"));
                    }

                    if (!Physics.Raycast(posRight, right, out hit, CrateController.Instance.distanceRay))
                    {
                        StartCoroutine(CrateMoving("Right"));
                    }
                }
            }
        }
    }


    void ShowArrow() //Показать стрелку которая указывает куда можно двигать ящик
    {
        if (CrateController.Instance.steps > 0)
        {
            if (gameObject.tag == "Crate")
            {
                RaycastCustom();
                RaycastHit hit;
                if (!Physics.Raycast(posForward, forward, out hit, CrateController.Instance.distanceRay)) //Если луч не касается объекта (ящика)
                {
                    EnabledAnimationEmissionArrow();
                    ArrowOffsetAndRotation(-90, 0, 1.5f); //Угол, позиция X, позиция Z
                }

                if (!Physics.Raycast(posBack, back, out hit, CrateController.Instance.distanceRay))
                {
                    EnabledAnimationEmissionArrow();
                    ArrowOffsetAndRotation(90, 0, -1.5f);
                }

                if (!Physics.Raycast(posLeft, left, out hit, CrateController.Instance.distanceRay))
                {
                    EnabledAnimationEmissionArrow();
                    ArrowOffsetAndRotation(180, -1.5f, 0);
                }

                if (!Physics.Raycast(posRight, right, out hit, CrateController.Instance.distanceRay))
                {
                    EnabledAnimationEmissionArrow();
                    ArrowOffsetAndRotation(0, 1.5f, 0);
                }
            }
        }
    }
    

    void EnabledAnimationEmissionArrow()
    {
        CrateController.Instance.arrow.SetActive(true);
        CrateController.Instance.arrow.transform.SetParent(transform);
        gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        gameObject.GetComponent<Animator>().enabled = true;
    }

    void ArrowOffsetAndRotation(int angle, float posX, float posZ)
    {
        CrateController.Instance.arrow.transform.position = new Vector3(transform.position.x + posX, transform.position.y + CrateController.Instance.offsetYposArrow, transform.position.z + posZ);
        CrateController.Instance.arrow.transform.rotation = Quaternion.Euler(-90, 0, angle);
    }


    void Update()
    {
        if (!FortBoyardGameController.Instance.IsRoomPause)
        {
            RaycastCustom();
        }
    }

    void OnMouseOver()
    {
        if (!FortBoyardGameController.Instance.IsRoomPause)
        {
            ShowArrow();
        }
    }

    void OnMouseDown()
    {
        if (!FortBoyardGameController.Instance.IsRoomPause)
        {
            RaycastHit();
        }
    }

    void OnMouseExit()
    {
        if (!FortBoyardGameController.Instance.IsRoomPause)
        {
            if (gameObject.tag == "Crate")
            {
                CrateController.Instance.arrow.SetActive(false);
                gameObject.GetComponent<Animator>().Rebind();
                gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
                gameObject.GetComponent<Animator>().enabled = false;
            }
        }
    }

    IEnumerator CrateMoving(string positionName)
    {
        float posToMove = 2.1f;
        float startPosePolojX = transform.position.x + posToMove;
        float startPoseNegativeX = transform.position.x - posToMove;
        float startPosePolojZ = transform.position.z + posToMove;
        float startPoseNegativeZ = transform.position.z - posToMove;
        CrateController.Instance.moving = true;
        CrateController.Instance.AudioSource.PlayOneShot(CrateController.Instance.crateMovingSound);

        switch (positionName) {
            case "Forward":
                while (startPosePolojZ > transform.position.z)
                {
                    transform.position = Vector3.MoveTowards(transform.position, 
                        new Vector3(transform.position.x, transform.position.y, transform.position.z + posToMove), Time.deltaTime * CrateController.Instance.speedCrate);
                    yield return null;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, startPosePolojZ);
                break;

            case "Back":
                while (startPoseNegativeZ < transform.position.z)
                {
                    transform.position = Vector3.MoveTowards(transform.position, 
                        new Vector3(transform.position.x, transform.position.y, transform.position.z - posToMove), Time.deltaTime * CrateController.Instance.speedCrate);
                    yield return null;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, startPoseNegativeZ);
                break;

            case "Left":
                while (startPoseNegativeX < transform.position.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, 
                        new Vector3(transform.position.x - posToMove, transform.position.y, transform.position.z), Time.deltaTime * CrateController.Instance.speedCrate);
                    yield return null;
                }
                transform.position = new Vector3(startPoseNegativeX, transform.position.y, transform.position.z);
                break;

            case "Right":
                while (startPosePolojX > transform.position.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, 
                        new Vector3(transform.position.x + posToMove, transform.position.y, transform.position.z), Time.deltaTime * CrateController.Instance.speedCrate);
                    yield return null;
                }
                transform.position = new Vector3(startPosePolojX, transform.position.y, transform.position.z);
                break;
        }

        CrateController.Instance.moving = false;
        CrateController.Instance.steps -= 1;
        CrateController.Instance.stepsUI.text = "Осталось ходов: " + CrateController.Instance.steps;

        if (CrateController.Instance.steps == 0 && CrateController.Instance._barrelTrigger.triggerEnter == false)
        {
            Debug.Log("Lose");
            CrateController.Instance._fortBoyardGameController.LoseRoom("Не осталось ходов!\nК сожалению вы не справились с испытанием");
        }
        if ((CrateController.Instance.steps == 0 || CrateController.Instance.steps > 0) && CrateController.Instance._barrelTrigger.triggerEnter == true)
        {
            Debug.Log("Win");
            StartCoroutine(ShowCenterRotationKey());
        }
    }
    IEnumerator ShowCenterRotationKey()
    {
        yield return new WaitForSeconds(1);
        CrateController.Instance.tipsScreenRotation.SetActive(true);
        yield return new WaitForSeconds(2);
        CrateController.Instance._fortBoyardGameController.WinnerRoom("Tips");
    }
}
