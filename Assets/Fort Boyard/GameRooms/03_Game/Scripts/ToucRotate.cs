using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToucRotate : MonoBehaviour {

    [Header("Degree of rotation offset. *360")]
    public float offset = 0f;

    Vector3 startDragDir;
    Vector3 currentDragDir;
    Quaternion initialRotation;
    float angleFromStart;

    void OnMouseDown()
    {

        startDragDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.localPosition;

        initialRotation = transform.localRotation;
        Debug.Log(initialRotation);
    }


    void OnMouseDrag()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.localPosition;

        difference.Normalize();


        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(0f, 0f, rotationZ - (90 + offset));
        Debug.Log("Dragging");
    }
}
