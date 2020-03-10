using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowV2 : MonoBehaviour {
    public float Speed = 2000.0f;
    public Transform Tip;
    Rigidbody RigidBody = null;
    bool IsStopped = true;
    Vector3 LastPosition = Vector3.zero;
	// Use this for initialization
	void Awake () {
        RigidBody = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        if (IsStopped) return;

        RigidBody.MoveRotation(Quaternion.LookRotation(RigidBody.velocity, transform.up));

        if (Physics.Linecast(LastPosition, Tip.position))
        {
            Stop();
        }

        LastPosition = Tip.position;
    }

    void Stop()
    {
        IsStopped = true;
        RigidBody.isKinematic = true;
        RigidBody.useGravity = false;
    }

    public void Fire(float PullValue)
    {
        IsStopped = false;
        RigidBody.isKinematic = false;
        RigidBody.useGravity = true;
        RigidBody.AddForce(transform.forward * (PullValue * Speed));
    }
}
