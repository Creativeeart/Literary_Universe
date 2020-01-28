using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipUI : MonoBehaviour {
	public TextMeshProUGUI text;

	public void TooltipText(string text2){
		text.text = text2;
	}
}
