using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTextUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	public string text;
	
	void IPointerEnterHandler.OnPointerEnter(PointerEventData e)
	{
		TooltipV2.text = text;
		TooltipV2.isUI = true;
	}
	
	void IPointerExitHandler.OnPointerExit(PointerEventData e)
	{
		TooltipV2.isUI = false;
	}
}
