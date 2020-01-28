using UnityEngine;
using System.Linq;
public class VerifyChecked : MonoBehaviour {
	[Header("Является ли объект положительным? Да/Нет")]
	public bool Yes;
	public bool No;

	private EscapeFromShip _escapeFromShip;
	private cakeslice.Toggle_Mini_Game _toggle;

	public void AddCount(){
		_escapeFromShip = GameObject.Find("Game Controller").GetComponent<EscapeFromShip>();
		_toggle = gameObject.GetComponent<cakeslice.Toggle_Mini_Game>();
		if (Yes == true) {
			if (_toggle.selectObject == true) {
				_escapeFromShip.SuccessfulCount += 1;
			} else {
				_escapeFromShip.SuccessfulCount -= 1;
			}

		} 
		if (No == true) {
			if (_toggle.selectObject == true) {
				_escapeFromShip.ErrorsCount += 1;
			} else {
				_escapeFromShip.ErrorsCount -= 1;
			}

		}
	}
}