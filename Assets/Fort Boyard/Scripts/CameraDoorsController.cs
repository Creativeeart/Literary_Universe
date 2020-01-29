using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace cakeslice
{
    public class CameraDoorsController : MonoBehaviour
    {
        public GameObject[] game_room;
        public static CameraDoorsController Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void StartGames(int numberRoom)
        {
            game_room[numberRoom].SetActive(true);
            FortBoyardGameController.Instance.CurrentNumberRoom = numberRoom;
        }
    }
}
