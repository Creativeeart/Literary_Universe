using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour {
    public Transform target;
    public GameObject bowGameObject;
    public float speed = 1f;
    public int zoom = 20;
    public int normal = 60;
    public float smooth = 5f;
    [HideInInspector]
    public bool isZoomed = false;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
        {
            isZoomed = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isZoomed = false;
        }
        if (isZoomed)
        {
            target.localEulerAngles = new Vector3(0, 0, 0);
            bowGameObject.transform.rotation = Quaternion.RotateTowards(bowGameObject.transform.rotation, target.transform.rotation, Time.deltaTime * speed);
            bowGameObject.transform.localEulerAngles = new Vector3(0, 0, bowGameObject.transform.localEulerAngles.z);

            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
            Cursor.visible = false;
        }
        else
        {
            target.localEulerAngles = new Vector3(0, 0, -35f);
            bowGameObject.transform.rotation = Quaternion.RotateTowards(bowGameObject.transform.rotation, target.transform.rotation, Time.deltaTime * speed);
            bowGameObject.transform.localEulerAngles = new Vector3(0, 0, bowGameObject.transform.localEulerAngles.z);

            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
            Cursor.visible = true;
        }
	}
}
