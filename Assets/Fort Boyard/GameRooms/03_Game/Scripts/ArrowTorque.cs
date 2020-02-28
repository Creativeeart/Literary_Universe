using UnityEngine;

public class ArrowTorque : MonoBehaviour {
    
    Rigidbody RigidBody;

    public float VelocityMult;
    public float AngularVelocityMult;
    private void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate () {

        Vector3 cross = Vector3.Cross(transform.forward, RigidBody.velocity.normalized);
        RigidBody.AddTorque(cross * RigidBody.velocity.magnitude * VelocityMult);
        RigidBody.AddTorque((-RigidBody.angularVelocity + Vector3.Project(RigidBody.angularVelocity, transform.forward)) * RigidBody.velocity.magnitude * AngularVelocityMult);

	}
}
