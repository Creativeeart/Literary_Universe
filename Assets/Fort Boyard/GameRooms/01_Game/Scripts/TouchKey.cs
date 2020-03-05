using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
public class TouchKey : MonoBehaviour
{
    TimerGame TimerGame;
    Game_01 Game_01;

    void Start()
    {
        Game_01 = Game_01.Instance;
        TimerGame = TimerGame.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touch Key");
        Game_01.isTouchKey = true;
        Game_01.key.transform.parent = Game_01.keyHolder.transform;
        Game_01.key.transform.localPosition = Vector3.zero;
        Game_01.key.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Game_01.DownShip();
        TimerGame.Instance.RunTime = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touch Key");
        Game_01.isTouchKey = true;
        Game_01.key.transform.parent = Game_01.keyHolder.transform;
        Game_01.key.transform.localPosition = Vector3.zero;
        Game_01.key.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Game_01.DownShip();
        TimerGame.Instance.RunTime = false;
    }
    private void OnCollision2D(Collision2D other)
    {
        Debug.Log("Touch Key");
        Game_01.isTouchKey = true;
        Game_01.key.transform.parent = Game_01.keyHolder.transform;
        Game_01.key.transform.localPosition = Vector3.zero;
        Game_01.key.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Game_01.DownShip();
        TimerGame.RunTime = false;
    }
}

