using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class BlockTopTest : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Touch Block Top Collision");
            Game_01.Instance.DownShip();
            Game_01.Instance.metalHit.Play();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Touch Block Top Trigger");
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Touch Block Top Collision");
        }
        private void OnTriggerEnter(Collider collision)
        {
            Debug.Log("Touch Block Top Trigger");
        }
    }
}
