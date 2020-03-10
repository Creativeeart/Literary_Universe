using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{

    public float HorizontalLimit;
    public float VerticalLimit;
    Bow Bow;
    FortBoyardGameController FortBoyardGameController;
    [Range(1,10)]
    public float sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        Bow = Bow.Instance;
        FortBoyardGameController = FortBoyardGameController.Instance;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FortBoyardGameController.IsRoomPause)
        {
            if (!Bow.isWinner)
            {
                float mousex = Input.GetAxis("Mouse X");
                float mousey = Input.GetAxis("Mouse Y");

                transform.eulerAngles += new Vector3(-mousey * sensitivity, mousex * sensitivity, 0);

                transform.eulerAngles = new Vector3(ClampAngle(transform.eulerAngles.x, -VerticalLimit, VerticalLimit),
                    ClampAngle(transform.eulerAngles.y, -HorizontalLimit, HorizontalLimit), transform.eulerAngles.z);
            }
        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
}
