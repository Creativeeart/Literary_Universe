using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waypoint : MonoBehaviour {
    [Tooltip ("asd")]
    
    public GameObject hud;
    public GameObject hudMarker;
    public GameObject target;
    public GameObject nextTarget;
    TextMeshProUGUI distanceText;
    Camera camMain;
    GameObject wp;
    float distance;
    GameObject oldTarget;

    void Start () {
        camMain = Camera.main;
        wp = Instantiate(hudMarker);
        wp.GetComponent<RectTransform>().SetParent(hud.transform);
        oldTarget = target;
        distanceText = GetComponentInChildren<TextMeshProUGUI>();
	}

	void Update () {
        if (target != null)
        {
            distance = Vector3.Distance(target.transform.position, camMain.transform.position);
            distanceText.text = distance.ToString("F1") + " m";
            if (distance < 7)
            {
                //wp.SetActive(false);
                target = nextTarget;
            }
            if (distance > 7)
            {
                wp.SetActive(true);
                target = oldTarget;
            }
            float check = Vector3.Dot((target.transform.position - camMain.transform.position).normalized, camMain.transform.forward);
            if (check <= 0)
            {
                //wp.SetActive(false);
            }
            else
            {
                //wp.SetActive(true);
                wp.GetComponent<RectTransform>().position = camMain.WorldToScreenPoint(new Vector3(
                    target.transform.position.x, target.transform.position.y, target.transform.position.z
                    ));
            }
        }
	}
}
