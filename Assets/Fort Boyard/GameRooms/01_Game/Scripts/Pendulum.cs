using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {
    public bool is3D = true;
    public GameObject BlockTop;
    public GameObject BlockDown;
    public Rigidbody body3D;
    public Rigidbody2D body;
    public float LeftPush;
    public float RightPush;
    public float VelocityThreshold;
    public float Force;

    // Use this for initialization
    void Start () {
        //body.angularVelocity = VelocityThreshold;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            BlockDown.SetActive(true);
            if (is3D)
            {
                body3D.isKinematic = false;
                body3D.AddTorque(Vector3.right * Force * 1000, ForceMode.Impulse);
            }
            else
            {
                body.simulated = true;
                body.AddTorque(Force, ForceMode2D.Impulse);
            }
        }
        //Push();
    }

    void Push()
    {
        if (transform.rotation.z > 0
            && transform.rotation.z < RightPush
            && (body.angularVelocity > 0)
            && body.angularVelocity < VelocityThreshold)
        {
            body.angularVelocity = VelocityThreshold;
        }
        else if (transform.rotation.z < 0
            && transform.rotation.z > LeftPush
            && (body.angularVelocity < 0)
            && body.angularVelocity > VelocityThreshold * -1)
        {
            body.angularVelocity = VelocityThreshold * -1;
        }
    }
}
