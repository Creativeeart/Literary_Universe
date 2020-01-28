using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiotest : MonoBehaviour {
    private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    public bool isPlay;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlay && !audioSource.isPlaying)
            audioSource.Play();
        else if (!isPlay && audioSource.isPlaying)
            audioSource.Stop();
    }
}
