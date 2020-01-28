using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public class BoolEvent : UnityEvent<bool> { }

public class ToggleController : MonoBehaviour 
{
	public bool isOn;
    public RectTransform toggle;
    public Image toggleBgImage;
    public Sprite onImageBg, offImageBg, handleON, handleOFF;
	public GameObject handle, onIcon, offIcon;
	public float handleOffset;
    public BoolEvent boolEvent;

    private RectTransform handleTransform;
    private float handleSize, onPosX, offPosX;

    void Awake()
	{
		handleTransform = handle.GetComponent<RectTransform>();
		RectTransform handleRect = handle.GetComponent<RectTransform>();
		handleSize = handleRect.sizeDelta.x;
		float toggleSizeX = toggle.sizeDelta.x;
		onPosX = (toggleSizeX / 2) - (handleSize/2) - handleOffset;
		offPosX = onPosX * -1;
	}

	void Start()
	{
        if (isOn) IsOn(); else IsOff();
    }
    
	void Update()
	{
        if (isOn) IsOn(); else IsOff();
    }

    void IsOn()
    {
        toggleBgImage.sprite = onImageBg;
        handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
        onIcon.gameObject.SetActive(true);
        offIcon.gameObject.SetActive(false);
        handle.GetComponent<Image>().sprite = handleON;
        if (boolEvent != null)
        {
            boolEvent.Invoke(true);
        }
    }

    void IsOff()
    {
        toggleBgImage.sprite = offImageBg;
        handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
        onIcon.gameObject.SetActive(false);
        offIcon.gameObject.SetActive(true);
        handle.GetComponent<Image>().sprite = handleOFF;
        if (boolEvent != null)
        {
            boolEvent.Invoke(false);
        }
    }

}
