using UnityEngine;
using UnityEngine.UI;


public class OrbitCamMouse : MonoBehaviour
{
    public KeyCode MouseOrbitButton, MouseOrbitButton_left;

    public bool AlwaysOrbit = false;
    public bool LockCursorOnMouseDown = true;

    public bool AllowRotate = true;
    public float RotateSpeed;

    public bool AllowTilt = true;
    public float TiltSpeed;

    public bool AllowZoom = true;
    public float ZoomSpeed;

    public string RotateInputAxis = "Mouse X";
    public string TiltInputAxis = "Mouse Y";
    public string ZoomInputAxis = "Mouse ScrollWheel";
	public Camera cam;
	public Transform _OldTarget;

    private OrbitCam _orbitCamera;
	public float testFloat;
	public bool IsEarthPlanet;
    protected void Reset()
    {
        MouseOrbitButton = KeyCode.Mouse1;
        MouseOrbitButton_left = KeyCode.Mouse2;

        AlwaysOrbit = false;
        LockCursorOnMouseDown = true;

        AllowRotate = true;
        RotateSpeed = 200f;

        AllowTilt = true;
        TiltSpeed = 100f;

        AllowZoom = true;
        ZoomSpeed = 500f;

        RotateInputAxis = "Mouse X";
        TiltInputAxis = "Mouse Y";
        ZoomInputAxis = "Mouse ScrollWheel";

    }

    protected void Start()
    {
        _orbitCamera = gameObject.GetComponent<OrbitCam>();
//		_orbitCamera.Distance = 20.0f;
//		_orbitCamera.Tilt = 0f;

    }

    protected void Update()
    {
        if (_orbitCamera.isEnable)
        {
            if (_orbitCamera == null) return;
            if (AllowZoom)
            {
                var scroll = Input.GetAxisRaw(ZoomInputAxis);
                _orbitCamera.Distance -= scroll * ZoomSpeed * Time.deltaTime;
                if (IsEarthPlanet == true)
                {
                    if (cam == null)
                        return;
                    cam.farClipPlane = -cam.transform.position.z;
                }
            }

            if (AlwaysOrbit || Input.GetKey(MouseOrbitButton) || Input.GetKey(MouseOrbitButton_left))
            {
                if (LockCursorOnMouseDown)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

                if (AllowTilt)
                {
                    var tilt = Input.GetAxisRaw(TiltInputAxis);
                    _orbitCamera.Tilt -= tilt * TiltSpeed * Time.deltaTime;
                }

                if (AllowRotate)
                {
                    var rot = Input.GetAxisRaw(RotateInputAxis);
                    _orbitCamera.Rotation += rot * RotateSpeed * Time.deltaTime;
                }
            }
            else
            {
                if (LockCursorOnMouseDown)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }
    

}
