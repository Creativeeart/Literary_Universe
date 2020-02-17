using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace cakeslice
{
    public class CameraDoorsController : MonoBehaviour
    {
        public static CameraDoorsController Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void StartGames(int numberRoom)
        {
            FortBoyardGameController.Instance.game_rooms[numberRoom].SetActive(true);
            FortBoyardGameController.Instance.CurrentNumberRoom = numberRoom;
        }
    }
}
