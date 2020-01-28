using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCoinFall : MonoBehaviour {
    AudioSource audioSource;
    public AudioClip coinSound;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(coinSound);
    }
}
