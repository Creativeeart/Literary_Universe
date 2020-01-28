using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
	{
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
	}
    public void AudioPlay()
    {
        foreach (Sound s in sounds)
        {
            if (s.soundsSliders != null)
            {
                s.source.volume = s.soundsSliders.value;
            }
            else
            {
                s.source.volume = 0.3f;
            }
        }
    }
	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
        if (s != null)
        {
            AudioPlay();
            s.source.Play();
        }
	}
    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s != null)
        {
            s.source.Stop();
        }
    }
    public bool IsPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s != null)
        {
            return s.source.isPlaying;
        }
        else
        {
            return false;
        }
        
    }
}
