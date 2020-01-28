using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class SwitchCamOnTrigger : MonoBehaviour
    {
        public Game_01 _game_01;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_game_01.camFollowShip.activeSelf == true)
            {
                _game_01.slowTime = true;
                _game_01.camViewKey.SetActive(true);
                _game_01.camFollowShip.SetActive(false);
            }
            else
            {
                _game_01.slowTime = false;
                _game_01.camViewKey.SetActive(false);
                _game_01.camFollowShip.SetActive(true);
            }
        }
        void Update()
        {
            _game_01.camViewKey.transform.LookAt(_game_01.ship.transform);
            if (_game_01.slowTime)
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
