using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextHover : MonoBehaviour {
	public Color hoverColor;
	public Color defaultColor;
	public GameObject[] formCategories;
	public GameObject[] arrows;
	public TextMeshProUGUI[] textCategories;
	int currentForm;
	// Use this for initialization
	public void TextHoverFunc(int i){ //При наведении мыши на текст
		for (int j = 0; j < textCategories.Length; j++){
            if (textCategories[j] == null) continue;
            textCategories [j].color = defaultColor;
		}
		textCategories[i].color = hoverColor;
	}
	public void TextDefaultFunc(int i){ //При отведение мыши от текста
		for (int j = 0; j < textCategories.Length; j++){
            if (textCategories[j] == null) continue;
            textCategories [j].color = defaultColor;
		}
	}
	public void SelectForm(int i){ //Клик по категории
		for (int j=0; j<textCategories.Length; j++){
            if (textCategories[j] == null) continue;
            textCategories [j].color = defaultColor;
		}
	}
	public void TextHoverArrow(int i){ //При клике на текст, появление "стрелки" слева от текста
		for (int j = 0; j < arrows.Length; j++) {
            if (arrows[j] == null) continue;
            arrows [j].SetActive (false);
		}
		arrows [i].SetActive (true);
		currentForm = i;
	}

	public void Reset(){ //Сброс при закрытии меню
		currentForm = 0;
		for (int j = 0; j < arrows.Length; j++) {
            if (arrows[j] == null) continue;
			arrows [j].SetActive (false);
		}
		for (int j = 0; j < formCategories.Length; j++) {
            if (formCategories[j] == null) continue;
            formCategories [j].SetActive (false);
		}
		for (int j = 0; j < textCategories.Length; j++) {
            if (textCategories[j] == null) continue;
            textCategories [j].color = defaultColor;
		}

    }

	void Update () {
		if (arrows [currentForm].activeSelf) {
			textCategories [currentForm].color = hoverColor;
		}
	}
}
