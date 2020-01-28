using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData e)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData e)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
