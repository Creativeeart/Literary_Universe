using UnityEngine; 
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class SliderRightMenu :	MonoBehaviour, IPointerClickHandler
 {
	public GameObject parentObject;
	public float Xpos;
	public GameObject objectMove;
	public float objectMoveXpos;

	public void OnPointerClick (PointerEventData evd)
	{
		parentObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(Xpos, parentObject.GetComponent<RectTransform>().anchoredPosition.y),0.7f);
		objectMove.GetComponent<RectTransform>().DOAnchorPos(new Vector2(objectMoveXpos, objectMove.GetComponent<RectTransform>().anchoredPosition.y),0.7f);
	}


}
