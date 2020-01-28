using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
public class FB_CamMovingController : MonoBehaviour {
    [Header("This place to moving camera from the scene")]
    public Transform cameraToMovingFromScene; //Камера которая будет двигаться в сцен

    [Header("Points for moving from camera")]
    public Transform pointToStartPositionA;
    public Transform pointToStartPositionB;
    public Transform targetForCamRotation;
    [Space]
    public Transform pointToBriefing;
    [Space]
    public Transform pointToDoor1;
    public Transform pointToDoor2;
    public Transform pointToDoor3;
    public Transform pointToDoor4;
    public Transform pointToDoor5;
    public Transform pointToDoor6;
    [Space]
    public Transform pointToGateZoneA;
    public Transform pointToGateZoneB;
    [Space]
    public Transform pointToAlphabetZoneA;
    public Transform pointToAlphabetZoneB;
    [Space]
    public Transform pointToTreasure_Zone;
    [Space]
    public Transform pointToTreasure_Calculate_Zone_A;
    public Transform pointToTreasure_Calculate_Zone_B;

    [Header("Switch")]
    public bool isMovingToStartPositionA = false;
    public bool isMovingToStartPositionB = false;
    public bool isRotateOnTarget = false;
    [Space]
    public bool isMovingToBriefing = false;
    [Space]
    public bool isMovingToDoor1 = false;
    public bool isMovingToDoor2 = false;
    public bool isMovingToDoor3 = false;
    public bool isMovingToDoor4 = false;
    public bool isMovingToDoor5 = false;
    public bool isMovingToDoor6 = false;
    [Space]
    public bool isMovingToGateZoneA = false;
    public bool isMovingToGateZoneB = false;
    [Space]
    public bool isMovingToAlphabetZoneA = false;
    public bool isMovingToAlphabetZoneB = false;
    [Space]
    public bool isMovingToTreasureZone = false;
    [Space]
    public bool isMovingToTreasureCalculateZoneA = false;
    public bool isMovingToTreasureCalculateZoneB = false;

    [Header("Main Settings")]
    public float speedDurationMovingCamera = 2; //Скорость движения камеры от точки к точке

    [Header("Rotation Camera Settings")]
    public Vector3 offset; //Сдвиг камеры от цели
    public float speedRotation = 10; // Скорость вращения камеры
    public float speedZoom = 5; // Скорость при увеличении, колесиком мышки
    public float zoomMax = 120; // Макс. увеличение
    public float zoomMin = 40; // Мин. увеличение
    private float X;

    void Start()
    {
        MoveToStartPositionB();
    }

    void Update()
    {
        if (isRotateOnTarget)
        {
            CameraRotation();
        }
    }

    void MoveToStartPositionB() //Переход к позиции StartPositionB и начало вращения камеры вокруг цели
    {
        isMovingToStartPositionB = true;
        if (isMovingToStartPositionB)
        {
            cameraToMovingFromScene.DOMove(pointToStartPositionB.transform.position, speedDurationMovingCamera)
                .OnComplete(() =>
                {
                    isMovingToStartPositionB = false;
                    isRotateOnTarget = true;
                })
                .Play();
            cameraToMovingFromScene.DORotate(pointToStartPositionB.transform.eulerAngles, speedDurationMovingCamera)
                .OnComplete(() =>
                {
                    isMovingToStartPositionB = false;
                    isRotateOnTarget = true;
                })
                .Play();
        }
    }

    void CameraRotation() //Вращение камеры вокруг "цели"
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += speedZoom;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= speedZoom;
        offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
        if (Input.GetMouseButton(1))
        {
            X = cameraToMovingFromScene.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * speedRotation;
        }
        else
        {
            X = cameraToMovingFromScene.transform.localEulerAngles.y + Time.deltaTime * -speedRotation;
        }
        cameraToMovingFromScene.transform.localEulerAngles = new Vector3(offset.y, X, 0);
        cameraToMovingFromScene.transform.position = cameraToMovingFromScene.transform.localRotation * offset + targetForCamRotation.position;
    }
    void DisableChecks()
    {
        isMovingToStartPositionA = false;
        isMovingToStartPositionB = false;
        isRotateOnTarget = false;
        isMovingToBriefing = false;
        isMovingToDoor1 = false;
        isMovingToDoor2 = false;
        isMovingToDoor3 = false;
        isMovingToDoor4 = false;
        isMovingToDoor5 = false;
        isMovingToDoor6 = false;
        isMovingToGateZoneA = false;
        isMovingToGateZoneB = false;
        isMovingToAlphabetZoneA = false;
        isMovingToAlphabetZoneB = false;
        isMovingToTreasureZone = false;
        isMovingToTreasureCalculateZoneA = false;
        isMovingToTreasureCalculateZoneB = false;
    }
    public void CameraMovingToPoint(Transform point) //Движение камеры к точкам
    {
        switch (point.name)
        {
            case "Point_StartPosition_A":
                DisableChecks();
                isMovingToStartPositionA = true;
                break;
            case "Point_StartPosition_B":
                DisableChecks();
                isMovingToStartPositionB = true;
                break;
            case "Target_ForCamRotation":
                DisableChecks();
                isRotateOnTarget = true;
                break;
            case "Point_Briefing":
                DisableChecks();
                isMovingToBriefing = true;
                break;
            case "Point_Door1":
                DisableChecks();
                isMovingToDoor1 = true;
                break;
            case "Point_Door2":
                DisableChecks();
                isMovingToDoor2 = true;
                break;
            case "Point_Door3":
                DisableChecks();
                isMovingToDoor3 = true;
                break;
            case "Point_Door4":
                DisableChecks();
                isMovingToDoor4 = true;
                break;
            case "Point_Door5":
                DisableChecks();
                isMovingToDoor5 = true;
                break;
            case "Point_Door6":
                DisableChecks();
                isMovingToDoor6 = true;
                break;
            case "Point_GateZone_A":
                DisableChecks();
                isMovingToGateZoneA = true;
                break;
            case "Point_GateZone_B":
                DisableChecks();
                isMovingToGateZoneB = true;
                break;
            case "Point_AlphabetZone_A":
                DisableChecks();
                isMovingToAlphabetZoneA = true;
                break;
            case "Point_AlphabetZone_B":
                DisableChecks();
                isMovingToAlphabetZoneB = true;
                break;
            case "Point_Treasure_Zone":
                DisableChecks();
                isMovingToTreasureZone = true;
                break;
            case "Point_Treasure_Calculate_Zone_A":
                DisableChecks();
                isMovingToTreasureCalculateZoneA = true;
                break;
            case "Point_Treasure_Calculate_Zone_B":
                DisableChecks();
                isMovingToTreasureCalculateZoneB = true;
                break;
        }
        cameraToMovingFromScene.DOMove(point.transform.position, speedDurationMovingCamera).Play();
        cameraToMovingFromScene.DORotate(point.transform.eulerAngles, speedDurationMovingCamera).Play();
    }
}
