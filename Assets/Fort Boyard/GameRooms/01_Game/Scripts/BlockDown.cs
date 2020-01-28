using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class BlockDown : MonoBehaviour
    {
        public Game_01 _game_01;
        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Touch Block Down");
            _game_01.isBounce = true;
            _game_01.isReady = true;
            _game_01.audioSource.Stop();
            if (_game_01.isReady && _game_01.isTouchKey)
            {
                StartCoroutine(ShowCenterRotationKey());
            }
        }
        IEnumerator ShowCenterRotationKey()
        {
            yield return new WaitForSeconds(1);
            _game_01.keyRotationCenter.SetActive(true);
            yield return new WaitForSeconds(2);
            _game_01._fortBoyardGameController.WinnerRoom("Keys");
        }
    }
}
