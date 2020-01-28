using UnityEngine; 
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Linq;
using System.Collections;
using TMPro;
namespace cakeslice
{
    public class MovingCameraFromAuthors : MonoBehaviour
    {
        public GameObject textFreeToCam;
        public float timeSpawn;
        public bool followcam;
        public Vector3 targetPos = Vector3.zero;
        [Header("Authors")]
        public GameObject[] authors;
        [Header("Stars")]
        public GameObject[] stars;
        public AudioClip soundMove;
        public AudioSource audioClip;
        bool Checked = false;
        public float orbitCamDistance = 20.0f;
        public CanvasGroup interfaceCanvasGroup;

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
            textFreeToCam = GameObject.FindGameObjectWithTag("SkipText");

            interfaceCanvasGroup = GameObject.Find("UIManager").GetComponent<CanvasGroup>();
            authors = GameObject.FindGameObjectsWithTag("Target");
            authors = authors.OrderBy(go => go.name).ToArray();
            for (int i = 0; i < authors.Length; i++)
            {
                authors[i].transform.localScale = new Vector3(0, 0, 0);
            }
            interfaceCanvasGroup.alpha = 0;
            if (_supportScripts._states.isAnimationMainAuthors == false)
            {
                for (int i = 0; i < authors.Length; i++)
                {
                    authors[i].transform.localScale = targetPos;
                }
                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].SetActive(true);
                }
                textFreeToCam.GetComponent<TextMeshProUGUI>().enabled = false;
                textFreeToCam.GetComponent<Animator>().enabled = false;
                _supportScripts._orbitCam.Distance = orbitCamDistance;
                Checked = true;
                interfaceCanvasGroup.alpha = 1;
                _supportScripts._states.isAnimationMainAuthors = false;
            }
        }

        public void SkipToMovingCamera()
        {
            for (int i = 0; i < authors.Length; i++)
            {
                authors[i].transform.localScale = targetPos;
            }
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(true);
            }
            textFreeToCam.GetComponent<TextMeshProUGUI>().enabled = false;
            textFreeToCam.GetComponent<Animator>().enabled = false;
            _supportScripts._orbitCam.Distance = orbitCamDistance;
            Checked = true;
            interfaceCanvasGroup.alpha = 1;
            _supportScripts._states.isAnimationMainAuthors = false;
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SkipToMovingCamera();
            }
            float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
            if (scroll < -0.1)
            {
                SkipToMovingCamera();
            }
            if (!Checked)
            {
                if (authors[0].transform.localScale.x < 1.5f)
                {
                    authors[0].transform.DOScale(targetPos, timeSpawn);
                    if (followcam)
                    {
                        _supportScripts._orbitCam.Target = authors[0].transform.GetComponent<Transform>();
                    }
                    else
                    {
                        _supportScripts._orbitCam.Distance = orbitCamDistance;
                    }

                }
                for (int i = 0, j = 1; i < authors.Length - 1; i++, j++)
                {
                    if (authors[i].transform.localScale.x >= 1.4f)
                    {
                        authors[j].transform.DOScale(targetPos, timeSpawn);
                        stars[j].SetActive(true);

                        if (followcam)
                        {
                            _supportScripts._orbitCam.Target = authors[j].transform.GetComponent<Transform>();
                        }
                        else
                        {
                            _supportScripts._orbitCam.Distance = orbitCamDistance;
                        }
                    }
                }

                if (authors[6].transform.localScale.x >= 1.4)
                {
                    SkipToMovingCamera();
                }
            }
        }
    }
}
