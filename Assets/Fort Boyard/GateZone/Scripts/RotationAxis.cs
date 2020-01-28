using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAxis : MonoBehaviour {
    public float speed = 20.0f;
    public bool back;
    public bool down;
    public bool forward;
    public bool left;
    public bool right;
    public bool up;

    // Update is called once per frame
    void Update () {
        if (back)
        {
            transform.Rotate(Vector3.back, Time.deltaTime * speed);
        }
        else if (down)
        {
            transform.Rotate(Vector3.down, Time.deltaTime * speed);
        }
        else if (forward)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * speed);
        }
        else if (left)
        {
            transform.Rotate(Vector3.left, Time.deltaTime * speed);
        }
        else if (right)
        {
            transform.Rotate(Vector3.right, Time.deltaTime * speed);
        }
        else if (up)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * speed);
        }
    }
}
