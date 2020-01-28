using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextHoverUltimate : MonoBehaviour {
	public Color hoverColor;
	public Color defaultColor;
	// Use this for initialization
	public void TextHoverFunc(TextMeshProUGUI textMeshPro){ //При наведении мыши на текст
		textMeshPro.color = hoverColor;
	}
	public void TextDefaultFunc(TextMeshProUGUI textMeshPro){ //При отведение мыши от текста
		textMeshPro.color = defaultColor;
	}
}
