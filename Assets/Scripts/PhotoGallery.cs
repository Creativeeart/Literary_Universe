using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoGallery : MonoBehaviour {
	public Image JuleVerneButton;
	public Image LandmarksButton;
	public Sprite NormalButton;
	public Sprite HoverButton;
	//-----------------------//
	public GameObject JuleVerneImages;
	//
	public GameObject Museum; //parent Dom_muzey & Park_mashin
	public GameObject Dom_muzey; //child Museum
	public GameObject Park_mashin; //child Museum
	public GameObject Dom_muzey_bigButton;
	public GameObject Park_mashin_bigButton;
	public GameObject museumTitle;
	bool isChildMuseum;
	//
	public GameObject MonumentoImages;
	public GameObject FriendsImages;
	public GameObject FamilyImages;
	public GameObject BigButtons;
	public GameObject BackButton;
	//-----------------------//
	public bool Resize = false;
	public Image fullScreenImage;
	public RectTransform fullScreenImageTransform;
	public GameObject ImageFullScreen;
	public TextMeshProUGUI annotationTextMeshPro;


//	void Start() {
//		Resolution[] resolutions = Screen.resolutions;
//		foreach (Resolution res in resolutions) {
//			print(res.width + "x" + res.height);
//		}
//		Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
//	}
	// Update is called once per frame
	public void JuleVerneActiveButton(){
		JuleVerneButton.sprite = HoverButton;
		LandmarksButton.sprite = NormalButton;
	}
	public void LandmarkActiveButton(){
		LandmarksButton.sprite = HoverButton;
		JuleVerneButton.sprite = NormalButton;
	}


	void Update () {
		if (Resize == true) {
			if ((fullScreenImageTransform.sizeDelta.x > Screen.width) || (fullScreenImageTransform.sizeDelta.y > Screen.height)) {
				fullScreenImageTransform.sizeDelta = new Vector2 (fullScreenImageTransform.sizeDelta.x / 2.0f, fullScreenImageTransform.sizeDelta.y / 2.0f);
			}
		}
//		if ((fullScreenImageTransform.sizeDelta.x < 500) || (fullScreenImageTransform.sizeDelta.y < 500)) {
//			fullScreenImageTransform.sizeDelta = new Vector2 (fullScreenImageTransform.sizeDelta.x * 2.0f, fullScreenImageTransform.sizeDelta.y * 2.0f);
//		}
	}
	public void ShowDomMusey(){
		isChildMuseum = true;
		Dom_muzey.SetActive (true);
		Park_mashin.SetActive (false);
		Dom_muzey_bigButton.SetActive (false);
		Park_mashin_bigButton.SetActive (false);
		museumTitle.SetActive (false);
	}
	public void ShowParkMashin(){
		isChildMuseum = true;
		Park_mashin.SetActive (true);
		Dom_muzey.SetActive (false);
		Dom_muzey_bigButton.SetActive (false);
		Park_mashin_bigButton.SetActive (false);
		museumTitle.SetActive (false);
	}

	public void ShowBigButtons(){
		if (isChildMuseum == true) {
			Museum.SetActive (true);
			museumTitle.SetActive (true);
			Park_mashin.SetActive (false);
			Dom_muzey.SetActive (false);
			Dom_muzey_bigButton.SetActive (true);
			Park_mashin_bigButton.SetActive (true);
			isChildMuseum = false;
		} else {
			BigButtons.SetActive (true);
			JuleVerneImages.SetActive (false);
			Museum.SetActive (false);
			MonumentoImages.SetActive (false);
			FriendsImages.SetActive (false);
			FamilyImages.SetActive (false);
			BackButton.SetActive (false);
		}

	}
		
	public void SwitchJuleVerneImages(){
		JuleVerneImages.SetActive (true);
		Museum.SetActive (false);
		MonumentoImages.SetActive (false);
		FriendsImages.SetActive (false);
		FamilyImages.SetActive (false);
		BigButtons.SetActive (false);
		BackButton.SetActive (true);
	}
	public void SwitchMuseumImages(){
		JuleVerneImages.SetActive (false);
		Museum.SetActive (true);
		MonumentoImages.SetActive (false);
		FriendsImages.SetActive (false);
		FamilyImages.SetActive (false);
		BigButtons.SetActive (false);
		BackButton.SetActive (true);
	}
	public void SwitchMonumentoImages(){
		JuleVerneImages.SetActive (false);
		Museum.SetActive (false);
		MonumentoImages.SetActive (true);
		FriendsImages.SetActive (false);
		FamilyImages.SetActive (false);
		BigButtons.SetActive (false);
		BackButton.SetActive (true);
	}
	public void SwitchFriendsImages(){
		JuleVerneImages.SetActive (false);
		Museum.SetActive (false);
		MonumentoImages.SetActive (false);
		FriendsImages.SetActive (true);
		FamilyImages.SetActive (false);
		BigButtons.SetActive (false);
		BackButton.SetActive (true);
	}
	public void SwitchFamilyImages(){
		JuleVerneImages.SetActive (false);
		Museum.SetActive (false);
		MonumentoImages.SetActive (false);
		FriendsImages.SetActive (false);
		FamilyImages.SetActive (true);
		BigButtons.SetActive (false);
		BackButton.SetActive (true);
	}

	public void SelectImage(Image selectedImage){
		fullScreenImage.sprite = selectedImage.sprite;
//		fullScreenImage.SetNativeSize ();
		ImageFullScreen.SetActive (true);

	}
	public void CloseFullScreen(){
		ImageFullScreen.SetActive (false);
	}

	public void AnnotationTextImages(string textInput){
		annotationTextMeshPro.text = textInput.ToString();
	}
}

