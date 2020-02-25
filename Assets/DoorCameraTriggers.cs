using UnityEngine;

public class DoorCameraTriggers : MonoBehaviour
{
    public DoorOpen[] doorOpens;

    void AllDoorLocked(int IndexDoorNotLocked) //Блокируем сначала все двери, затем одну разблокировать
    {
        for (int i = 0; i < doorOpens.Length; i++)
        {
            doorOpens[i].isLocked = true;
        }
        doorOpens[IndexDoorNotLocked].isLocked = false;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "DoorTrigger#0":
                AllDoorLocked(0);
                break;
            case "DoorTrigger#1":
                AllDoorLocked(1);
                break;
            case "DoorTrigger#2":
                AllDoorLocked(2);
                break;
            case "DoorTrigger#3":
                AllDoorLocked(3);
                break;
            case "DoorTrigger#4":
                AllDoorLocked(4);
                break;
            case "DoorTrigger#5":
                AllDoorLocked(5);
                break;
        }
    }
}

