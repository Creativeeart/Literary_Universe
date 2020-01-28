using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitleObjectsInfo : MonoBehaviour {
	public TextMeshProUGUI textName;
	public GameObject nameObject_prefab;
	public string nameObj;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 100f)) {
				if (hit.transform.GetComponent<Rigidbody> () != null) {
					nameObj = hit.transform.gameObject.name;
					GameObject ins = Instantiate (nameObject_prefab, hit.transform.position, nameObject_prefab.transform.rotation) as GameObject ;
					ins.transform.parent = hit.transform;
					ins.transform.position = hit.transform.position+Vector3.up/1.6f;
					switch (nameObj) {
					case ("Axe_prefab(Clone)"):
						textName.text = "ТопорЛОХ";
						break;
					}

				}
			}
		}
	}
}
