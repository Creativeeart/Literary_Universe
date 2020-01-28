using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cakeslice
{
    public class States : MonoBehaviour
    {
        public static States instance;
        [Header("Main settings")]
        public bool isDontDestroyOnLoad = true;
        [Header("Other settings")]
        public bool isAnimationMainAuthors = true;
        public bool isActivateOverlayUI = false;

        void Awake()
        {
            if (isDontDestroyOnLoad)
            {
                if (instance == null)
                {
                    instance = this;
                }
                else
                {
                    Destroy(gameObject);
                    return;
                }
                DontDestroyOnLoad(gameObject);
            }
        }

        public void IsActivateOverlayUI()
        {

            isActivateOverlayUI = !isActivateOverlayUI;
        }
    }
}
