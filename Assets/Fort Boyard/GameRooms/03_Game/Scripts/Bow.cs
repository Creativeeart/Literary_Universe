using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace cakeslice
{
    public class Bow : MonoBehaviour
    {
        public FortBoyardGameController _fortBoyardGameController;
        public TextMeshProUGUI countArrow, countHitTargets;
        int fakeCount = 0;
        public GameObject hitPrefab;
        public float depth = 10f;
        public Camera mainCam;
        private Vector3 mousePos;
        public Vector3 aim;
        public float Tension;

        public Transform RopeTransform;

        public Vector3 RopeNearLocalPosition;
        public Vector3 RopeFarLocalPosition;

        public AnimationCurve RopeReturnAnimation;

        public float ReturnTime;

        Arrow CurrentArrow;
        public int ArrowIndex = 0;
        public float ArrowSpeed;

        public AudioSource BowTensionAudioSource;
        public AudioSource BowWhistlingAudioSource;
        public AudioSource ArrowHit;

        public Arrow[] ArrowsPool;

        public float curentHitTargets = 0;

        public GameObject keyRotationCenter;
        public bool isWinner = false;
        void Start()
        {
            fakeCount = ArrowsPool.Length;
            CurrentArrow = ArrowsPool[ArrowIndex];
            RopeNearLocalPosition = RopeTransform.localPosition;
        }

        void Update()
        {
            if (!FortBoyardGameController.Instance.IsRoomPause)
            {
                if (!isWinner) {
                    mousePos = Input.mousePosition;
                    mousePos += mainCam.transform.forward * depth;
                    aim = mainCam.ScreenToWorldPoint(mousePos);

                    if (ArrowIndex < ArrowsPool.Length)
                    {
                        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
                        {
                            BowTensionAudioSource.pitch = Random.Range(0.8f, 1.2f);
                            BowTensionAudioSource.Play();
                        }
                        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
                        {
                            if (Tension < 1f) Tension += Time.deltaTime / 1.4f;
                            RopeTransform.localPosition = Vector3.Lerp(RopeNearLocalPosition, RopeFarLocalPosition, Tension);
                            CurrentArrow = ArrowsPool[ArrowIndex];
                            CurrentArrow.SetToRope(RopeTransform);
                        }
                        else if (Input.GetMouseButtonUp(0) && Input.GetMouseButtonUp(1))
                        {
                            if (Tension > 0.1f)
                                Shoot();
                        }
                        //else
                        //{
                        //AbortShoot(); //Медленный возврат тетивы и стрелы на место
                        //}

                        if ((Input.GetMouseButton(1) && Input.GetMouseButtonUp(0)) || (Input.GetMouseButton(0) && Input.GetMouseButtonUp(1)))
                        {
                            if (Tension > 0.1f)
                                Shoot();
                        }
                    }
                }
            }
        }
        void Shoot()
        {
            fakeCount--;
            countArrow.text = "Осталось стрел: " + fakeCount;
            ArrowIndex++;
            StartCoroutine(RopeReturn());
            CurrentArrow.Shot(ArrowSpeed * Tension);
            Tension = 0;
            BowTensionAudioSource.Stop();
            BowWhistlingAudioSource.pitch = Random.Range(0.8f, 1.2f);
            BowWhistlingAudioSource.Play();
            BowTensionAudioSource.Stop();
        }
        
        void AbortShoot()
        {
            if (Tension > 0f)
            {
                Tension -= Time.deltaTime * 2;
                if (Tension < 0) Tension = 0;
            }
            RopeTransform.localPosition = Vector3.Lerp(RopeNearLocalPosition, RopeFarLocalPosition, Tension);
        }
        public IEnumerator RopeReturn()
        {
            Vector3 startLocalPosition = RopeTransform.localPosition;
            for (float f = 0; f < 1f; f += Time.deltaTime / ReturnTime)
            {
                RopeTransform.localPosition = Vector3.LerpUnclamped(startLocalPosition, RopeNearLocalPosition, RopeReturnAnimation.Evaluate(f));
                yield return null;
            }
            RopeTransform.localPosition = RopeNearLocalPosition;
        }
    }
}
