using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using TMPro;
public class Tips : MonoBehaviour {
	public GameObject[] tipsArray;
	public TextMeshProUGUI tipCount;
	private int randomCount = 0;

	void Start () {
//		randomCount = Random.Range (0, tipsArray.Length);
		tipsArray = tipsArray.OrderBy(go => go.name).ToArray();
		tipsArray [randomCount].SetActive (true);
		tipCount.text = randomCount + 1 + "/" + tipsArray.Length; 
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			LeftMoving ();
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			RightMoving ();
		}
	}
	public void LeftMoving(){
		randomCount--;
		if (randomCount < 0) {
			randomCount = tipsArray.Length - 1;
		}
		for (int j = 0; j < tipsArray.Length; j++){
			tipsArray [j].SetActive (false);
		}
		tipsArray [randomCount].SetActive (true);
		tipCount.text = randomCount + 1 + "/" + tipsArray.Length;
	}

	public void RightMoving(){
		randomCount++;
		if (randomCount > tipsArray.Length-1) {
			randomCount = 0;
		}
		for (int j = 0; j < tipsArray.Length; j++){
			tipsArray [j].SetActive (false);
		}
		tipsArray [randomCount].SetActive (true);
		tipCount.text = randomCount + 1 + "/" + tipsArray.Length;
	}
	public void Tip(int enterIndexTip){
		randomCount = enterIndexTip;
		for (int j = 0; j < tipsArray.Length; j++){
			tipsArray [j].SetActive (false);
		}
		tipsArray [randomCount].SetActive (true);
		tipCount.text = randomCount + 1 + "/" + tipsArray.Length;
	}
}
