using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class Crate : MonoBehaviour
    {
        private AudioSource _audioSource;
        public CrateController _crateController;
        public float distanceRay = 10f;

        Vector3 forward, back, left, right;
        Vector3 posForward, posBack, posLeft, posRight;

        void RaycastCustom()
        {
            forward = transform.TransformDirection(Vector3.forward) * distanceRay;
            back = transform.TransformDirection(Vector3.back) * distanceRay;
            left = transform.TransformDirection(Vector3.left) * distanceRay;
            right = transform.TransformDirection(Vector3.right) * distanceRay;

            posForward = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z + 1f);
            posBack = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - 1f);
            posLeft = new Vector3(transform.position.x - 1, transform.position.y + 1f, transform.position.z);
            posRight = new Vector3(transform.position.x + 1, transform.position.y + 1f, transform.position.z);

            Debug.DrawRay(posForward, forward, Color.yellow);
            Debug.DrawRay(posBack, back, Color.red);
            Debug.DrawRay(posLeft, left, Color.blue);
            Debug.DrawRay(posRight, right, Color.green);
        }

        void RaycastHit(bool isMoving)
        {
            if (_crateController.steps > 0)
            {
                RaycastCustom();
                RaycastHit hitForward;
                if (!Physics.Raycast(posForward, forward, out hitForward, distanceRay))
                {
                    if (gameObject.tag == "Crate")
                    {
                        if (isMoving)
                        {
                            if (!_crateController.moving)
                                StartCoroutine(CrateMoving("Forward"));
                        }
                        else
                        {
                            EnabledAnimationEmissionArrow();
                            _crateController.arrow.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 1.5f);
                            _crateController.arrow.transform.rotation = Quaternion.Euler(-90, 0, -90);
                        }
                    }
                }

                RaycastHit hitBack;
                if (!Physics.Raycast(posBack, back, out hitBack, distanceRay))
                {
                    if (gameObject.tag == "Crate")
                    {
                        if (isMoving)
                        {
                            if (!_crateController.moving)
                                StartCoroutine(CrateMoving("Back"));
                        }
                        else
                        {
                            EnabledAnimationEmissionArrow();
                            _crateController.arrow.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z - 1.5f);
                            _crateController.arrow.transform.rotation = Quaternion.Euler(-90, 0, 90);
                        }
                    }
                }

                RaycastHit hitLeft;
                if (!Physics.Raycast(posLeft, left, out hitLeft, distanceRay))
                {
                    if (gameObject.tag == "Crate")
                    {
                        if (isMoving)
                        {
                            if (!_crateController.moving)
                                StartCoroutine(CrateMoving("Left"));
                        }
                        else
                        {
                            EnabledAnimationEmissionArrow();
                            _crateController.arrow.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y + 1.5f, transform.position.z);
                            _crateController.arrow.transform.rotation = Quaternion.Euler(-90, 0, 180);
                        }
                    }
                }

                RaycastHit hitRight;
                if (!Physics.Raycast(posRight, right, out hitRight, distanceRay))
                {
                    if (gameObject.tag == "Crate")
                    {
                        if (isMoving)
                        {
                            if (!_crateController.moving)
                                StartCoroutine(CrateMoving("Right"));
                        }
                        else
                        {
                            EnabledAnimationEmissionArrow();
                            _crateController.arrow.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1.5f, transform.position.z);
                            _crateController.arrow.transform.rotation = Quaternion.Euler(-90, 0, 0);
                        }
                    }
                }
            }
        }

        void EnabledAnimationEmissionArrow()
        {
            _crateController.arrow.SetActive(true);
            _crateController.arrow.transform.SetParent(transform);
            gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            gameObject.GetComponent<Animator>().enabled = true;
        }
        private void Start()
        {
            gameObject.AddComponent<AudioSource>();
            _audioSource = gameObject.GetComponent<AudioSource>();
            _audioSource.pitch = Random.Range(1, 1.5f);
        }
        void Update()
        {
            RaycastCustom();
        }

        void OnMouseOver()
        {
            RaycastHit(false); //false = mouseOver = lightning object
        }

        void OnMouseDown()
        {
            if (!FortBoyardGameController.Instance.IsRoomPause) {
                RaycastHit(true); //true = mouseDown = moving object
            }
        }

        void OnMouseExit()
        {
            if (gameObject.tag == "Crate")
            {
                _crateController.arrow.SetActive(false);
                gameObject.GetComponent<Animator>().Rebind();
                gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
                gameObject.GetComponent<Animator>().enabled = false;
            }
        }

        IEnumerator CrateMoving(string positionName)
        {

            float posToMove = 2.1f;
            float startPosePolojZ = transform.position.z + posToMove;
            float startPoseNegativeZ = transform.position.z - posToMove;
            float startPosePolojX = transform.position.x + posToMove;
            float startPoseNegativeX = transform.position.x - posToMove;
            _crateController.moving = true;
            _audioSource.PlayOneShot(_crateController.crateMovingSound);
            if (positionName == "Forward")
            {
                while (startPosePolojZ > transform.position.z)
                {
                    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y, transform.position.z + posToMove), Time.deltaTime * _crateController.speedCrate);
                    yield return null;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, startPosePolojZ);
            }

            if (positionName == "Back")
            {
                while (startPoseNegativeZ < transform.position.z)
                {
                    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y, transform.position.z - posToMove), Time.deltaTime * _crateController.speedCrate);
                    yield return null;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, startPoseNegativeZ);
            }

            if (positionName == "Left")
            {
                while (startPoseNegativeX < transform.position.x)
                {
                    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x - posToMove, transform.position.y, transform.position.z), Time.deltaTime * _crateController.speedCrate);
                    yield return null;
                }
                transform.position = new Vector3(startPoseNegativeX, transform.position.y, transform.position.z);
            }

            if (positionName == "Right")
            {
                while (startPosePolojX > transform.position.x)
                {
                    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x + posToMove, transform.position.y, transform.position.z), Time.deltaTime * _crateController.speedCrate);
                    yield return null;
                }
                transform.position = new Vector3(startPosePolojX, transform.position.y, transform.position.z);
            }
            _crateController.moving = false;
            _crateController.steps -= 1;
            _crateController.stepsUI.text = "Осталось ходов: " + _crateController.steps;
            if (_crateController.steps == 0 && _crateController._barrelTrigger.triggerEnter == false)
            {
                Debug.Log("Lose");
                _crateController._fortBoyardGameController.LoseRoom("Не осталось ходов!\nК сожалению вы не справились с испытанием");
            }
            if ((_crateController.steps == 0 || _crateController.steps > 0) && _crateController._barrelTrigger.triggerEnter == true)
            {
                Debug.Log("Win");
                StartCoroutine(ShowCenterRotationKey());
            }
        }
        IEnumerator ShowCenterRotationKey()
        {
            yield return new WaitForSeconds(1);
            _crateController.tipsScreenRotation.SetActive(true);
            yield return new WaitForSeconds(2);
            _crateController._fortBoyardGameController.WinnerRoom("Tips");
        }
    }
}