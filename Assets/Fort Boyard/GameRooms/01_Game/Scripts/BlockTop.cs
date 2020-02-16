using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class BlockTop : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Touch Block Top");
            Game_01.Instance.DownShip();
            Game_01.Instance.metalHit.Play();
        }
    }
}
