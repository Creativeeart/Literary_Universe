using UnityEngine;
namespace cakeslice
{
    public class AuthorSelected : MonoBehaviour
    {
        public bool AllowRotate = true;
        public bool AllowTilt = true;
        public float Distance;
        public float Rotation;
        public float Tilt;
        public Vector3 Offset;
        public bool useParticleSystem = false;
        public ParticleSystem waveParticle;
        public bool MainScene;
        SupportScripts _supportScripts;
        void Start()
        {
            if (GameObject.Find("SupportSripts"))
            {
                _supportScripts = GameObject.Find("SupportSripts").GetComponent<SupportScripts>();
            }
            else
            {
                Debug.Log("SupportSripts - не найден либо отключен!");
                return;
            }
        }

        void Update()
        {
            if (_supportScripts._orbitCamMouse.AllowZoom)
            {
                if (_supportScripts._orbitCam.Target != _supportScripts._orbitCamMouse._OldTarget)
                {
                    if (_supportScripts._orbitCam.Distance >= 9.0f)
                    {
                        ClosePanel();
                    }
                }
            }
        }
        void OnMouseDown()
        {
            if (MainScene)
            {
                _supportScripts._movingCameraFromAuthors.SkipToMovingCamera();
                _supportScripts._tips.Tip(1);
            }
            SelectBlock();
        }

        public void ClosePanel()
        {
            _supportScripts._orbitCam.Target = _supportScripts._orbitCamMouse._OldTarget;
            _supportScripts._orbitCam.Distance = 20.0f;
            _supportScripts._orbitCamMouse.AllowRotate = true;
            _supportScripts._orbitCamMouse.AllowTilt = true;
            _supportScripts._orbitCam.Tilt = 10f;
        }

        public void SelectBlock()
        {
            //_supportScripts._orbitCam.isEnable = !_supportScripts._orbitCam.isEnable;
            //_supportScripts._pause_Game.CheckPause();
            if (_supportScripts._swipe_scroll_menu.checkedButton == false)
            {
                if (_supportScripts._states.isActivateOverlayUI == false) {
                    _supportScripts._orbitCamMouse.AllowRotate = AllowRotate;
                    _supportScripts._orbitCamMouse.AllowTilt = AllowTilt;
                    _supportScripts._orbitCam.Distance = Distance;
                    _supportScripts._orbitCam.Rotation = Rotation;
                    _supportScripts._orbitCam.Tilt = Tilt;
                    _supportScripts._orbitCam.Target = transform.GetComponent<Transform>();
                    _supportScripts._orbitCam.Offset = Offset;
                    if (useParticleSystem)
                    {
                        waveParticle.Play();
                    }
                }
            }
        }
    }
}