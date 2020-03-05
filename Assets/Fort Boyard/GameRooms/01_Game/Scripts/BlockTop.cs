using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
public class BlockTop : MonoBehaviour
{
    Game_01 Game_01;

    void Start()
    {
        Game_01 = Game_01.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touch Block Top");
        Game_01.DownShip();
        Game_01.metalHit.Play();
    }
}
