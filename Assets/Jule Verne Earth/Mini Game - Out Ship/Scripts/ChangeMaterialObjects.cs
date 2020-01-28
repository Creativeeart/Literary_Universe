using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialObjects : MonoBehaviour {
	Material m_Material;
//	private VerifyChecked _verifyChecked;
	void Start()
	{
//		_verifyChecked = gameObject.GetComponent<VerifyChecked>();
		m_Material = GetComponent<Renderer>().material;
	}


	void OnMouseOver()
	{
		m_Material.color = Color.yellow;
	}

	void OnMouseExit()
	{
//		if (_verifyChecked.itemClicked == false) {
//			m_Material.color = Color.white;
//		} 
	}
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 100f)) {
				if (hit.transform.GetComponent<Rigidbody> () != null) {
					MaterialChange ();
				} 
			}

		}
	}
	public void MaterialChange(){
//		if (_verifyChecked.itemClicked == true) {
//			m_Material.color = Color.yellow;
//		} else {
//			m_Material.color = Color.white;
//		}
	}
}
