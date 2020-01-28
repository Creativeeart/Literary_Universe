using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class WorldSpaceVideo : MonoBehaviour {
    public GameObject scrollView, divider, videoScreenForm;
    public TextMeshProUGUI nameVideo;
    public Image playImage;
    public Sprite playSprite;
    public Sprite pauseSprite;
    [Header("Видеоклипы")]
    public VideoClip[] videoClips;
    [Header("Наст. время и продолж. время видео .")]
    public Text mergedTime;
    public Slider videoSlider;
    public Slider audioSlider;
    public Image progress;
    public GameObject ErrorPanel;

    AudioSource audioSource;
    int videoClipIndex;
    bool isLoop;
    [HideInInspector]
    public VideoPlayer videoPlayer;
    string urlFullVideo;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    void Start()
    {
        videoPlayer.targetTexture.Release();
        videoPlayer.clip = videoClips[0];
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            AudioPlay();
            MergedTime();
            mergedTime.text = MergedTime();
        }
    }

    public void AudioPlay() {
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        audioSource.volume = audioSlider.value;
    }

    public void CloseVideoScreen()
    {
        scrollView.SetActive(true);
        divider.SetActive(true);
        videoScreenForm.SetActive(false);
        nameVideo.text = "ФИЛЬМОГРАФИЯ";
        StopVideo();
    }
	
    public void PlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playImage.sprite = playSprite;
        }
        else
        {
            TemplatePlay();
        }
    }

    public void SetNextClip()
	{
		videoClipIndex++;
		if (videoClipIndex >= videoClips.Length) videoClipIndex = videoClipIndex % videoClips.Length;
		videoPlayer.clip = videoClips [videoClipIndex];
        MergedTime();
        videoPlayer.Play ();
	}
    public void StopVideo()
    {
        videoPlayer.Stop();
        videoSlider.value = 0;
        mergedTime.text = "00:00 / 00:00";
		playImage.sprite = playSprite;
		videoPlayer.targetTexture.Release ();
    }

    void TemplatePlay()
    {
        MergedTime();
        AudioPlay();
        videoPlayer.Play();
        videoPlayer.Prepare();
        playImage.sprite = pauseSprite;
    }

    public void LoadVideoFromURL(string urlVideo)
    {
        videoPlayer.url = urlVideo;
        TemplatePlay();
    }

    public void LoadVideoFromProject(VideoClip videoClip)
    {
        scrollView.SetActive(false);
        divider.SetActive(false);
        videoScreenForm.SetActive(true);
        videoPlayer.clip = videoClip;
        TemplatePlay();
    }
    public void LoadVideo(int numberObject)
    {
        videoPlayer.clip = videoClips[numberObject];
        TemplatePlay();
    }
    
    public void LoopVideo()
    {
        isLoop = !isLoop;
        if (isLoop) videoPlayer.isLooping = true;
        else videoPlayer.isLooping = false;
    }

    string MergedTime()
	{
		string currentMinutes = Mathf.Floor ((int)videoPlayer.time / 60).ToString ("00");
		string currentSeconds = ((int)videoPlayer.time % 60).ToString ("00");
        string currentTime = currentMinutes + ":" + currentSeconds;

        string totalMinutes = Mathf.Floor((int)videoPlayer.clip.length / 60).ToString("00");
        string totalSeconds = ((int)videoPlayer.clip.length % 60).ToString("00");
        string totalTime = totalMinutes + ":" + totalSeconds;

        string mergedTime = currentTime + " / " + totalTime;

        return mergedTime;
    }

    public void NameVideo(string nameVideo)
    {
        this.nameVideo.text = nameVideo;
    }

    public void FullUrl(string fullUrl)
    {
        urlFullVideo = fullUrl;
    }

    public void OpenURLs()
    {
        Application.OpenURL(urlFullVideo);
    }

}