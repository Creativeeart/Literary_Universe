using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class RotatePipe : MonoBehaviour
{
    Outline _outLine;
    readonly float rotationSpeed = 130f; // Скорость поворота 
    readonly float targetAngle = 90f; // Угол на который надо повернуться 
    public int fakeAngles = 0;

    public bool isColumn = false;
    public bool isCorrectAngle = false;

    float[] angles = { -270, -90, -180 };

    FortBoyardGameController FortBoyardGameController;
    GameRoom2_Controller GameRoom2_Controller;

    void Start()
    {
        FortBoyardGameController = FortBoyardGameController.Instance;
        GameRoom2_Controller = GameRoom2_Controller.Instance;
        _outLine = gameObject.transform.GetChild(0).GetComponent<Outline>();
        //Random
        int index = Random.Range(0, angles.Length);
        if (GameRoom2_Controller.random)
        {
            transform.Rotate(0, 0, (int)angles[index]);
            //Random

            fakeAngles = (int)angles[index];
        }
        CheckCorrectRotation();
        _outLine.enabled = false;
    }

    void OnMouseDown()
    {
        if (!FortBoyardGameController.Instance.IsRoomPause)
        {
            fakeAngles = fakeAngles + -90;
            if (fakeAngles < -270)
                fakeAngles = 0;

            StartCoroutine(RotateMeNow());
            CheckCorrectRotation();
            _outLine.enabled = true;
            GameRoom2_Controller.audioSource.PlayOneShot(GameRoom2_Controller.rotatePipe);
        }
    }

    void OnMouseEnter()
    {
        if (!FortBoyardGameController.IsRoomPause)
        {
            _outLine.enabled = true;
        }
    }

    void OnMouseExit()
    {
        if (!FortBoyardGameController.IsRoomPause)
        {
            _outLine.enabled = false;
        }
    }

    IEnumerator RotateMeNow()
    {
        float currentAngle = 0f;
        while (true)
        {
            float step = rotationSpeed * Time.deltaTime;
            if (currentAngle + step > targetAngle)
            {
                // Докручиваем до нашего угла 
                step = targetAngle - currentAngle;
                transform.Rotate(Vector3.back, step);
                break;
            }
            currentAngle += step;
            transform.Rotate(Vector3.back, step);
            yield return null;
        }
    }

    void CheckCorrectRotation()
    {
        if (isColumn)
            isCorrectAngle = ((fakeAngles == 0) || (fakeAngles == -180)) ? true : false;
        else
            isCorrectAngle = (fakeAngles == 0) ? true : false;
        GameRoom2_Controller.CheckSumm();
    }
}

