using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateTumbler : MonoBehaviour
{
    public bool isRotateTumbler = false;
    public int numberBulb = 0;
    readonly float rotationSpeed = 200f; // Скорость поворота 
    readonly float targetAngle = 90f; // Угол на который надо повернуться 

    public int RotateAngle = 0;
    //readonly float[] angles = { 270, 90, 180 };
    Game_05_Controller Game_05_Controller;

    void Start()
    {
        Game_05_Controller = Game_05_Controller.Instance;
    }

    void OnMouseDown()
    {
        if (Game_05_Controller.isActivateTumbler)
        {
            Game_05_Controller.ToggleTumblerSound();
            RotateAngle += 90;
            if (RotateAngle > 270) RotateAngle = 0;
            StartCoroutine(RotateMeNow());
            switch (RotateAngle)
            {
                case 0:
                    Game_05_Controller.playerBulbs[numberBulb].GetComponent<MeshRenderer>().material = Game_05_Controller.defaultMaterial;
                    Debug.Log("Default Color");
                    break;
                case 90:
                    Game_05_Controller.playerBulbs[numberBulb].GetComponent<MeshRenderer>().material = Game_05_Controller.materials[0];
                    Debug.Log("Red Color");
                    break;
                case 180:
                    Game_05_Controller.playerBulbs[numberBulb].GetComponent<MeshRenderer>().material = Game_05_Controller.materials[1];
                    Debug.Log("Green Color");
                    break;
                case 270:
                    Game_05_Controller.playerBulbs[numberBulb].GetComponent<MeshRenderer>().material = Game_05_Controller.materials[2];
                    Debug.Log("Blue Color");
                    break;
            }
            isRotateTumbler = (RotateAngle != 0) ? true : false;
            Game_05_Controller.CheckSumm();
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
        gameObject.GetComponent<MeshRenderer>().material = Game_05_Controller.emissionTumbler;
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = Game_05_Controller.defaultTumbler;
    }
}
