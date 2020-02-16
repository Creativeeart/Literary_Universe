using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class BlockDown : MonoBehaviour
    {        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Touch Block Down");
            Game_01.Instance.isReady = true;
            Game_01.Instance.audioSource.Stop();
            StartCoroutine(Game_01.Instance.RopeReturn(false));
            if (Game_01.Instance.isReady && Game_01.Instance.isTouchKey)
            {
                StartCoroutine(ShowCenterRotationKey());
            }
        }
        IEnumerator ShowCenterRotationKey()
        {
            yield return new WaitForSeconds(1);
            Game_01.Instance.keyRotationCenter.SetActive(true);
            yield return new WaitForSeconds(2);
            FortBoyardGameController.Instance.WinnerRoom("Keys");
        }
    }
}
