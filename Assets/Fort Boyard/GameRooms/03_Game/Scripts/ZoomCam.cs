using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour {
    public GameObject bowGameObject;
    public float speed = 1f;
    public int zoom = 20;
    public int normal = 60;
    public float smooth = 5f;

    [HideInInspector]
    public bool isZoomed = false;
    public bool isPressedLeftKeyMouse = false;
    Camera MainCamera;

    void Start()
    {
        MainCamera = gameObject.GetComponent<Camera>();
    }

    void Update () {
		if (Input.GetMouseButtonDown(1))
        {
            isZoomed = true;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isZoomed = false;
            Cursor.visible = true;
        }
        if (Input.GetMouseButton(0))
        {
            isPressedLeftKeyMouse = true;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPressedLeftKeyMouse = false;
            Cursor.visible = true;
        }
        if (isZoomed)
        {
            bowGameObject.transform.rotation = Quaternion.RotateTowards(bowGameObject.transform.rotation, Quaternion.Euler(0,0,0), Time.deltaTime * speed);
            bowGameObject.transform.localEulerAngles = new Vector3(0, 0, bowGameObject.transform.localEulerAngles.z);

            MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, zoom, Time.deltaTime * smooth);
        }
        else
        {
            bowGameObject.transform.rotation = Quaternion.RotateTowards(bowGameObject.transform.rotation, Quaternion.Euler(0, 0, -35f), Time.deltaTime * speed);
            bowGameObject.transform.localEulerAngles = new Vector3(0, 0, bowGameObject.transform.localEulerAngles.z);

            MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, normal, Time.deltaTime * smooth);
        }
	}
}
