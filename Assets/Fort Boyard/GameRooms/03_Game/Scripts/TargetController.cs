using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {
    [HideInInspector]
    public bool isTargetHit = false;
    public float minSpeed;
    public float maxSpeed;
    private AudioSource _audioSource;
    public AudioClip movingSound;

    void Start () {
        gameObject.AddComponent<AudioSource>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.pitch = Random.Range(1, 1.5f);
        _audioSource.volume = Random.Range(0.2f, 0.4f);
        _audioSource.clip = movingSound;
        _audioSource.loop = true;
        _audioSource.Play();
        var randomValue = Random.Range(minSpeed, maxSpeed);
        transform.parent.GetComponent<Animator>().speed = randomValue;
    }

	void Update () {
        if (isTargetHit)
        {
            float step = 5f * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(0, 90f, 0), step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
