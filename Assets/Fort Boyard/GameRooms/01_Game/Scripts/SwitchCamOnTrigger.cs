using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class SwitchCamOnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (Game_01.Instance.camFollowShip.activeSelf == true)
            {
                Game_01.Instance.slowTime = true;
                Game_01.Instance.camViewKey.SetActive(true);
                Game_01.Instance.camFollowShip.SetActive(false);
            }
            else
            {
                Game_01.Instance.slowTime = false;
                Game_01.Instance.camViewKey.SetActive(false);
                Game_01.Instance.camFollowShip.SetActive(true);
            }
        }
        void Update()
        {
            Game_01.Instance.camViewKey.transform.LookAt(Game_01.Instance.ObjectForCam2View.transform);
            if (Game_01.Instance.slowTime)
            {
                Time.timeScale = 0.2f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
