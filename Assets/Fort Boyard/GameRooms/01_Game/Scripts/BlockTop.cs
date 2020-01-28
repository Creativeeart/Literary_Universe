using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class BlockTop : MonoBehaviour
    {
        public Game_01 _game_01;
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Touch Block Top");
            _game_01.DownShip();
            _game_01.metalHit.Play();
        }
    }
}
