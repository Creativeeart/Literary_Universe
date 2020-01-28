using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBibliographyParentChild : MonoBehaviour {
    public GameObject parentForm, childForm, containerButtons, returnButton;


    public void SwitchToChild() {
        parentForm.SetActive(false);
        childForm.SetActive(true);
        returnButton.SetActive(true);
        containerButtons.SetActive(false);
    }
    public void SwitchToParent() {
        parentForm.SetActive(true);
        childForm.SetActive(false);
        returnButton.SetActive(true);
        containerButtons.SetActive(false);
    }
    public void ReturnToButtons() {
        parentForm.SetActive(false);
        childForm.SetActive(false);
        returnButton.SetActive(false);
        containerButtons.SetActive(true);
    }
}
