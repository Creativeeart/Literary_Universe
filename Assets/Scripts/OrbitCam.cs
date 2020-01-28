using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(OrbitCamMouse))]
public class OrbitCam : MonoBehaviour
{
    public Transform Target;

    public float Distance;          // Desired distance (units, ie Meters)
    public float Rotation;          // Desired rotation (degrees)
    public float Tilt;              // Desired tilt (degrees)
    public Vector3 Offset;

    public bool Smoothing;          // Should the camera "slide" between positions and targets?
    public float ZoomDampening;     // How "smooth" should the camera zooms be?  Note: Smaller numbers are smoother
    public float RotationDampening; // How "smooth" should the camera rotations be?  Note: Smaller numbers are smoother
    public float TiltDampening;     // How "smooth" should the camera tilts be?  Note: Smaller numbers are smoother

    public float MinDistance;       // Minimum distance of camera from target
    public float MaxDistance;       // Maximum distance of camera from target

    public float MinRotation;       // Minimum rotation (degrees)
    public float MaxRotation;       // Maximum rotation (degrees)

    public float MinTilt;           // Minimum tilt (degrees)
    public float MaxTilt;           // Maximum tilt (degrees)

    public bool CollisionDetection;              // If set, camera will raycast from target out in order to avoid objects being between target and camera
    public float CameraRadius = 0.5f;
    public bool AutoSetIgnoreLayers = false;
    public LayerMask CollisionIgnoreLayerMask;   // Layer mask to ignore when raycasting to determine camera visbility

    public bool FollowBehind;                           // If set, keyboard and mouse rotation will be disabled when Following a target
    public float FollowRotationOffset;                  // Offset (degrees from zero) when forcing follow behind target

    private float _currDistance;    // actual distance
    private float _currRotation;    // actual rotation
    private float _currTilt;        // actual tilt
    private Vector3 _offset;
    public bool isEnable = true;

    public void Reset()
    {
        Smoothing = true;

        CollisionDetection = true;
        CameraRadius = 0.5f;
        CollisionIgnoreLayerMask = 0;
        AutoSetIgnoreLayers = false;

        Distance = 16f;
        MinDistance = 2f;
        MaxDistance = 32f;

        Rotation = 0f;
        MinRotation = -180f;
        MaxRotation = 180f;

        Tilt = 45f;
        MinTilt = -15f;
        MaxTilt = 85f;

        TiltDampening = 5f;
        ZoomDampening = 5f;
        RotationDampening = 5f;

        FollowBehind = false;
        FollowRotationOffset = 0;
        Offset = new Vector3(0, 0, 0);
    }

    protected void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        _currDistance = Distance;
        _currRotation = Rotation;
        _currTilt = Tilt;
        _offset = Offset;
    }

    protected void LateUpdate()
    {
        if (isEnable)
        {
            if (Target == null)
                return;
            Rotation = WrapAngle(Rotation);
            Tilt = WrapAngle(Tilt);

            Tilt = Mathf.Clamp(Tilt, MinTilt, MaxTilt);
            Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);
            Rotation = Mathf.Clamp(Rotation, MinRotation, MaxRotation);

            if (Smoothing)
            {
                _currRotation = Mathf.LerpAngle(_currRotation, Rotation, Time.deltaTime * RotationDampening);
                _currDistance = Mathf.Lerp(_currDistance, Distance, Time.deltaTime * ZoomDampening);
                _currTilt = Mathf.LerpAngle(_currTilt, Tilt, Time.deltaTime * TiltDampening);
                _offset = Offset;
            }
            else
            {
                _currRotation = Rotation;
                _currDistance = Distance;
                _currTilt = Tilt;
                _offset = Offset;
            }

            if (FollowBehind)
            {
                ForceFollowBehind();
            }

            if (CollisionDetection)
            {
                if (AutoSetIgnoreLayers && CollisionIgnoreLayerMask == 0)
                {
                    CollisionIgnoreLayerMask = 1 << Target.gameObject.layer;
                }

                CheckCollisions();
            }

            UpdateCamera();
        }
    }

    public void SnapTo(float distance, float rotation, float tilt)
    {
        Distance = _currDistance = distance;
        Rotation = _currRotation = rotation;
        Tilt = _currTilt = tilt;
    }

    private void UpdateCamera()
    {
        if (Target == null)
            return;

        var rotation = Quaternion.Euler(_currTilt, _currRotation, 0);
        var v = new Vector3(_offset.x, _offset.y, -_currDistance);
        
        var position = rotation * v + Target.transform.position;

        if (GetComponent<Camera>().orthographic)
        {
            GetComponent<Camera>().orthographicSize = _currDistance;
        }
		transform.rotation = rotation;
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 2);
    }

    private RaycastHit[] _prevHits;
    private void CheckCollisions()
    {
        var direction = (transform.position - Target.transform.position);
        direction.Normalize();

        var distance = Distance;

        RaycastHit hitInfo;

        if (Physics.SphereCast(Target.transform.position, CameraRadius, direction, out hitInfo, distance, ~CollisionIgnoreLayerMask))
        {
            if (hitInfo.transform != Target)
            {
                _currDistance = hitInfo.distance - 0.05f;
            }
        }
    }

    private void ForceFollowBehind()
    {
        var v = Target.transform.forward * -1;
        var angle = Vector3.Angle(Vector3.forward, v);
        var sign = (Vector3.Dot(v, Vector3.right) > 0.0f) ? 1.0f : -1.0f;
        _currRotation = Rotation = 180f + (sign * angle) + FollowRotationOffset;
    }

    private float WrapAngle(float angle)
    {
        while (angle < -180)
        {
            angle += 360;
        }
        while (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }
}
