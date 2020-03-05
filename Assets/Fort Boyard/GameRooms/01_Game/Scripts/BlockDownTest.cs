﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
public class BlockDownTest : MonoBehaviour
{
    FortBoyardGameController FortBoyardGameController;
    Game_01 Game_01;

    void Start()
    {
        FortBoyardGameController = FortBoyardGameController.Instance;
        Game_01 = Game_01.Instance;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Touch Block Down Collision");
        Game_01.Ship.GetComponent<Rigidbody2D>().simulated = false;
        Game_01.Ship.GetComponent<Rigidbody2D>().transform.localPosition = new Vector3(0, Game_01.Ship.GetComponent<Rigidbody2D>().transform.localPosition.y, 0);
        Game_01.Ship.GetComponent<Rigidbody2D>().transform.localEulerAngles = new Vector3(0, -90, 0);
        Game_01.isReady = true;
        Game_01.audioSource.Stop();
        StartCoroutine(Game_01.RopeReturn(false));
        if (Game_01.isReady && Game_01.isTouchKey)
        {
            StartCoroutine(ShowCenterRotationKey());
        }
    }

    IEnumerator ShowCenterRotationKey()
    {
        yield return new WaitForSeconds(1);
        Game_01.keyRotationCenter.SetActive(true);
        yield return new WaitForSeconds(2);
        FortBoyardGameController.WinnerRoom("Keys");
    }
}
