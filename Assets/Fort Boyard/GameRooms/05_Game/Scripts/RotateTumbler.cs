using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class RotateTumbler : MonoBehaviour
    {
        public bool isRotateTumbler = false;
        public int numberBulb = 0;
        public Game_05_Controller _game_05_Controller;
        readonly float rotationSpeed = 200f; // Скорость поворота 
        readonly float targetAngle = 90f; // Угол на который надо повернуться 

        public int fakeAngles = 0;
        readonly float[] angles = { 270, 90, 180 };

        void OnMouseDown()
        {
            if (_game_05_Controller.isActivateTumbler)
            {
                _game_05_Controller.ToggleTumblerSound();
                fakeAngles = fakeAngles + 90;
                if (fakeAngles > 270)
                    fakeAngles = 0;
                StartCoroutine(RotateMeNow());
                if (fakeAngles == 0)
                {
                    _game_05_Controller.playerBulbs[numberBulb].GetComponent<MeshRenderer>().material = _game_05_Controller.defaultMaterial;
                    Debug.Log("Default Color");
                }
                if (fakeAngles == 90)
                {
                    _game_05_Controller.playerBulbs[numberBulb].GetComponent<MeshRenderer>().material = _game_05_Controller.materials[0];
                    Debug.Log("Red Color");
                }
                if (fakeAngles == 180)
                {
                    _game_05_Controller.playerBulbs[numberBulb].GetComponent<MeshRenderer>().material = _game_05_Controller.materials[1];
                    Debug.Log("Green Color");
                }
                if (fakeAngles == 270)
                {
                    _game_05_Controller.playerBulbs[numberBulb].GetComponent<MeshRenderer>().material = _game_05_Controller.materials[2];
                    Debug.Log("Blue Color");
                }
                isRotateTumbler = (fakeAngles != 0) ? true : false;
                _game_05_Controller.CheckSumm();
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
                    transform.Rotate(Vector3.up, step);
                    break;
                }
                currentAngle += step;
                transform.Rotate(Vector3.up, step);
                yield return null;
            }
        }

        private void OnMouseOver()
        {
            gameObject.GetComponent<MeshRenderer>().material = _game_05_Controller.emissionTumbler;
        }
        private void OnMouseExit()
        {
            gameObject.GetComponent<MeshRenderer>().material = _game_05_Controller.defaultTumbler;
        }
    }
}