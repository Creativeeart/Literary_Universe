using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace cakeslice
{
    public class RotatePipe : MonoBehaviour
    {
        Outline _outLine;
        readonly float rotationSpeed = 130f; // Скорость поворота 
        readonly float targetAngle = 90f; // Угол на который надо повернуться 
        public int fakeAngles = 0;

        public bool isColumn = false;
        public bool isCorrectAngle = false;
        GameRoom2_Controller _gameRoom2_Controller;

        float[] angles = { -270, -90, -180 };

        void Start()
        {
            _gameRoom2_Controller = GameObject.Find("Controller").GetComponent<GameRoom2_Controller>();
            _outLine = gameObject.transform.GetChild(0).GetComponent<Outline>();
            //Random
            int index = Random.Range(0, angles.Length);
            if (_gameRoom2_Controller.random)
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
                _gameRoom2_Controller.audioSource.PlayOneShot(_gameRoom2_Controller.rotatePipe);
            }
        }

        void OnMouseEnter()
        {
            if (!FortBoyardGameController.Instance.IsRoomPause)
            {
                _outLine.enabled = true;
            }
        }

        void OnMouseExit()
        {
            if (!FortBoyardGameController.Instance.IsRoomPause)
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
            _gameRoom2_Controller.CheckSumm();
        }
    }
}
