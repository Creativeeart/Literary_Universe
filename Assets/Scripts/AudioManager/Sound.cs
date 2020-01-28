using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Sound {
	public string name;
	public GameObject parent;
	public AudioClip clip;

    public Slider soundsSliders; 
	public bool loop = false;

	[HideInInspector]
	public AudioSource source;

}
