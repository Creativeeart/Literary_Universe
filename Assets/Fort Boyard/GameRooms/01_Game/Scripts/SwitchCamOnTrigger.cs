using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwitchCamOnTrigger : MonoBehaviour
{
    Game_01 Game_01;

    void Start()
    {
        Game_01 = Game_01.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Game_01.camFollowShip.activeSelf == true)
        {
            Game_01.slowTime = true;
            Game_01.camViewKey.SetActive(true);
            Game_01.camFollowShip.SetActive(false);
        }
        else
        {
            Game_01.slowTime = false;
            Game_01.camViewKey.SetActive(false);
            Game_01.camFollowShip.SetActive(true);
        }
    }
    void Update()
    {
        Game_01.camViewKey.transform.LookAt(Game_01.ObjectForCam2View.transform);
        if (Game_01.slowTime)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            if (!FortBoyardGameController.Instance.IsRoomPause)
            {
                Time.timeScale = 1;
            }
        }
    }
}

