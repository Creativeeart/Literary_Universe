using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/OrbitCam-Keyboard")]
public class OrbitCamKeys : MonoBehaviour
{
    public bool AllowRotate;
    public float RotateSpeed;

    public bool AllowZoom;
    public float ZoomSpeed;

    public bool AllowTilt;
    public float TiltSpeed;

    public string RotateInputAxis = "Horizontal";

    public bool ZoomUsesInputAxis = false;
    public string ZoomInputAxis = "KbCameraZoom";
    public KeyCode ZoomOutKey = KeyCode.F;
    public KeyCode ZoomInKey = KeyCode.R;

    public string TiltInputAxis = "Vertical";

    private OrbitCam _orbitCamera;

    protected void Reset()
    {
        AllowRotate = true;
        RotateSpeed = 100f;

        AllowZoom = true;
        ZoomSpeed = 10f;

        AllowTilt = true;
        TiltSpeed = 50f;
    }

    protected void Start()
    {
        _orbitCamera = gameObject.GetComponent<OrbitCam>();
    }

    protected void Update()
    {
        if (_orbitCamera == null)
            return; 
        if (AllowRotate)
        {
            var rot = Input.GetAxisRaw(RotateInputAxis);
            if (Mathf.Abs(rot) > 0.001f)
            {
                _orbitCamera.Rotation += rot * RotateSpeed * Time.deltaTime;
            }
        }

        if (AllowZoom)
        {
            if (ZoomUsesInputAxis)
            {
                var zoom = Input.GetAxisRaw(ZoomInputAxis);
                if (Mathf.Abs(zoom) > 0.001f)
                {
                    _orbitCamera.Distance += zoom * ZoomSpeed * Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetKey(ZoomOutKey))
                {
                    _orbitCamera.Distance += ZoomSpeed * Time.deltaTime;
                }
                if (Input.GetKey(ZoomInKey))
                {
                    _orbitCamera.Distance -= ZoomSpeed * Time.deltaTime;
                }
            }
        }

        if (AllowTilt)
        {
            var tilt = Input.GetAxisRaw(TiltInputAxis);
            if (Mathf.Abs(tilt) > 0.001f)
            {
                _orbitCamera.Tilt += tilt * TiltSpeed * Time.deltaTime;
            }
        }
    }
}
