using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class BlockDownTest : MonoBehaviour
    {
        public bool is3D = true;
        public Rigidbody2D rb2;
        public Rigidbody rb3D;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Touch Block Down Collision");
            if (is3D)
            {
                rb3D.isKinematic = true;
                rb3D.transform.position = new Vector3(0, rb2.transform.position.y, 0);
                rb3D.transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                rb2.simulated = false;
                rb2.transform.position = new Vector3(0, rb2.transform.position.y, 0);
                rb2.transform.localEulerAngles = new Vector3(0, -90, 0);
                Game_01.Instance.isReady = true;
                Game_01.Instance.audioSource.Stop();
                StartCoroutine(Game_01.Instance.RopeReturn(false));
                if (Game_01.Instance.isReady && Game_01.Instance.isTouchKey)
                {
                    StartCoroutine(ShowCenterRotationKey());
                }
            }
        }
        IEnumerator ShowCenterRotationKey()
        {
            yield return new WaitForSeconds(1);
            Game_01.Instance.keyRotationCenter.SetActive(true);
            yield return new WaitForSeconds(2);
            FortBoyardGameController.Instance.WinnerRoom("Keys");
        }
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Touch Block Down Collision");
            if (is3D)
            {
                rb3D.isKinematic = true;
                rb3D.transform.position = new Vector3(0, rb2.transform.position.y, 0);
                rb3D.transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                rb2.simulated = false;
                rb2.transform.position = new Vector3(0, rb2.transform.position.y, 0);
                rb2.transform.localEulerAngles = Vector3.zero;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Touch Block Down Trigger");
        }
    }
}
