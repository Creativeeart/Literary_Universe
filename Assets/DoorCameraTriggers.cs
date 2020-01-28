using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class DoorCameraTriggers : MonoBehaviour
    {
        public DoorOpen[] doorOpens;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.name == "DoorTrigger#0")
            {
                for (int i = 0; i < doorOpens.Length; i++)
                {
                    doorOpens[i].isLocked = true;
                }
                doorOpens[0].isLocked = false;
            }
            if (other.gameObject.name == "DoorTrigger#1")
            {
                for (int i = 0; i < doorOpens.Length; i++)
                {
                    doorOpens[i].isLocked = true;
                }
                doorOpens[1].isLocked = false;
            }
            if (other.gameObject.name == "DoorTrigger#3")
            {
                for (int i = 0; i < doorOpens.Length; i++)
                {
                    doorOpens[i].isLocked = true;
                }
                doorOpens[2].isLocked = false;
            }
            if (other.gameObject.name == "DoorTrigger#4")
            {
                for (int i = 0; i < doorOpens.Length; i++)
                {
                    doorOpens[i].isLocked = true;
                }
                doorOpens[3].isLocked = false;
            }
            if (other.gameObject.name == "DoorTrigger#5")
            {
                for (int i = 0; i < doorOpens.Length; i++)
                {
                    doorOpens[i].isLocked = true;
                }
                doorOpens[4].isLocked = false;
            }
            if (other.gameObject.name == "DoorTrigger#6")
            {
                for (int i = 0; i < doorOpens.Length; i++)
                {
                    doorOpens[i].isLocked = true;
                }
                doorOpens[5].isLocked = false;
            }
        }
    }
}
