using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class TouchKey : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Touch Key");
            Game_01.Instance.isTouchKey = true;
            Game_01.Instance.key.transform.parent = Game_01.Instance.keyHolder.transform;
            Game_01.Instance.key.transform.localPosition = Vector3.zero;
            Game_01.Instance.key.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Game_01.Instance.DownShip();
            TimerGame.Instance.RunTime = false;
        }
        
    }
}
