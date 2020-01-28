using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldLongClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    public float requredHoldTime;
    public AudioSource longAudioSource;
    public AudioClip longAudioClip;
    public UnityEvent onLongClick;

    [SerializeField]
    private Image fillImage;

    void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.unscaledDeltaTime;
            if (pointerDownTimer > requredHoldTime)
            {
                if (onLongClick != null)
                {
                    onLongClick.Invoke();

                }
                Reset();
            }

            fillImage.fillAmount = pointerDownTimer / requredHoldTime;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        longAudioSource.volume = 0.3f;
        longAudioSource.PlayOneShot(longAudioClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Reset();
    }
    public void Reset()
    {
        fillImage.fillAmount = 0;
        pointerDown = false;
        pointerDownTimer = 0;
        longAudioSource.volume = 1f;
        longAudioSource.Stop();
    }
}

