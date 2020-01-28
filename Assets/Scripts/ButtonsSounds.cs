using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonsSounds : MonoBehaviour {
    //public Button [] buttons;
    private AudioManager _audioManager;
    void Start()
    {
        //buttons = FindObjectsOfType(typeof(Button)) as Button[];
        _audioManager = GetComponent<AudioManager>();
    }

    void Update()
    {
        GameObject data = PointerRaycast(Input.mousePosition);
        if (data != null)
        {
            if (data.gameObject.GetComponentInParent<Button>())
            {
                if (data.gameObject.GetComponentInParent<Button>().interactable)
                {
                    if (Input.GetMouseButtonUp(0)) {
                        _audioManager.Play("ClickButtonSound");
                    }
                }
            }

        }
    }

    GameObject PointerRaycast(Vector2 position)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> resultsData = new List<RaycastResult>();
        pointerData.position = position;
        EventSystem.current.RaycastAll(pointerData, resultsData);
        if (resultsData.Count > 0)
            return resultsData[0].gameObject;
        return null;
    }
}
