using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimatorScript : MonoBehaviour {
	public Sprite [] allSprites;
	public Image currentImageForSrites;
	public float SpeedAnimation = 50f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentImageForSrites.sprite = allSprites [(int)(Time.time * SpeedAnimation) % allSprites.Length];
	}
}
