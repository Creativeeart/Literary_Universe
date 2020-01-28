using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour {
    public Camera cam;
    public float maximumLength;

    Ray rayMouse;
    Vector3 pos, direction;
    Quaternion rotation;
	
	void Update () {
		if (cam != null)
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximumLength))
            {
                RotateMouseDirection(gameObject, hit.point);
            }
            else
            {
                var pos = rayMouse.GetPoint(maximumLength);
                RotateMouseDirection(gameObject, pos);
            }
        }
	}

    void RotateMouseDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, Time.deltaTime * 5);
    }
}
