using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class TouchKey : MonoBehaviour
    {
        public Game_01 _game_01;
        

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Touch Key");
            _game_01.isTouchKey = true;
            _game_01.key.transform.parent = _game_01.keyHolder.transform;
            _game_01.key.transform.localPosition = Vector3.zero;
            _game_01.key.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _game_01.DownShip();
            _game_01._fortBoyardGameController._timerGame.RunTime = false;
        }
        
    }
}
