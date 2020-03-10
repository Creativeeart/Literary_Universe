using UnityEngine;
public class ArrowV2: MonoBehaviour
{
    public Vector3 centerOfMass;
    public TrailRenderer TrailRenderer;

    Rigidbody RigidBody;

    public void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.angularDrag = 0.5f;
        RigidBody.centerOfMass = centerOfMass;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = "#ffffffff".ToColor();
        Gizmos.DrawSphere(transform.position + centerOfMass, 0.01f);
    }

    public void SetToRope(Transform ropeTransform)
    {
        transform.parent = ropeTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (RigidBody != null)
        {
            RigidBody.isKinematic = true;
            RigidBody.useGravity = false;
        }
        TrailRenderer.enabled = false;
    }

    public void Shot(float velocity)
    {
        transform.parent = null;
        RigidBody.isKinematic = false;
        RigidBody.useGravity = true;
        RigidBody.velocity = transform.forward * velocity;
        TrailRenderer.Clear();
        TrailRenderer.enabled = true;
    }
}

