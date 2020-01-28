using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MainMenuVideoLoading: MonoBehaviour {
	public float repeat_time; /* Время в секундах */ 
	private float curr_time;
	public Image playImage;
	public Sprite playSprite;
	public Sprite pauseSprite;
	[Header("Настоящее время видео воспроизведения.")]
	public Text currentMinutes;
	public Text currentSeconds;
	[Header("Длительность видео")]
	public Text totalMinutes;
	public Text totalSeconds;
	public Slider slider;
	public Slider audioSlider;
	public bool TimerRunChecked = false;
	public GameObject cameraMain;
	public GameObject cameraFilm;
	public GameObject mainUI;
	public GameObject filmUI;
	public RectTransform fullscreen;
	public Vector3 mousePosMain;
	public bool checkedAspect;
	[Header("Видеоклипы")]
	[Tooltip("Кидаем сюда видео")]
	public VideoClip[] videoClips;

	private VideoPlayer videoPlayer;
	private AudioSource audioSource;
	private int videoClipIndex;
	public double _Time{get{return videoPlayer.time;}}
	public ulong Duration{get{return (ulong)(videoPlayer.frameCount / videoPlayer.frameRate);}}
	public double NTime{get {return _Time / Duration;}}
	public bool isPrepared{	get{ return videoPlayer.isPrepared;}}

	void OnEnable(){
		videoPlayer.errorReceived += errorReceived;
		videoPlayer.prepareCompleted += prepareCompleted;
		videoPlayer.seekCompleted += seekCompleted;
		videoPlayer.started += started;
	}
	void OnDisable(){
		videoPlayer.errorReceived -= errorReceived;
		videoPlayer.prepareCompleted -= prepareCompleted;
		videoPlayer.seekCompleted -= seekCompleted;
		videoPlayer.started -= started;
	}
	void seekCompleted(VideoPlayer v){
		Debug.Log ("Video player finished seeking");
	}
	void errorReceived(VideoPlayer v, string msg){
		Debug.Log ("Video player error:" + msg);
	}
	void prepareCompleted(VideoPlayer v){
		Debug.Log ("Video player finished preparing");
	}
	void started(VideoPlayer v){
		Debug.Log ("Video player started");
	}
	void Awake ()
	{
		videoPlayer = GetComponent<VideoPlayer> ();
	}

	void Start () 
	{
		filmUI.SetActive (false);
		curr_time = repeat_time;
		audioSource = gameObject.AddComponent<AudioSource>();
		mousePosMain = Input.mousePosition;

	}

	void Update () {
		if (videoPlayer.isPlaying) {
			slider.value = (float)NTime;
			AudioPlay ();
			SetCurrentTimeUI ();
		}
		if (TimerRunChecked == true) {
			if (mousePosMain != Input.mousePosition) {
				curr_time -= Time.unscaledDeltaTime;
				if (checkedAspect == true) {
					Cursor.visible = true;
					filmUI.SetActive (true);
				}
			} 
			if (curr_time <= 0) {
				if (checkedAspect == true) {
					Cursor.visible = false;
					filmUI.SetActive (false);
				}
				mousePosMain = Input.mousePosition;
				curr_time = repeat_time;
			}

		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			checkedAspect = false;
			Cursor.visible = true;
			cameraFilm.SetActive (false);
			cameraMain.SetActive (true);
			mainUI.SetActive (true);
			filmUI.SetActive(false);
			videoPlayer.Stop();
		}
	}

	public void CloseVideoPanel(){
			checkedAspect = false;
			Cursor.visible = true;
			cameraFilm.SetActive (false);
			cameraMain.SetActive (true);
			mainUI.SetActive (true);
			filmUI.SetActive(false);
			videoPlayer.Stop();
	}

	public void Seek(float nTime){
		if (!videoPlayer.canSetTime) return;
		if (!videoPlayer) return;
		if (Input.GetMouseButton(0))
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				nTime = Mathf.Clamp (nTime, 0, 1);
				videoPlayer.time = nTime * Duration;
			}
		}
	}


	public void StopVideo()
	{
		if (!videoPlayer.isPlaying) return;
		videoPlayer.Stop();
		slider.value = 0;
		totalMinutes.text = "00";
		totalSeconds.text = "00";
		currentMinutes.text = "00";
		currentSeconds.text = "00";
		playImage.sprite = playSprite;
	}

	public void PlayPause()
	{
		if (videoPlayer.isPlaying) 
		{
			videoPlayer.Pause ();
			playImage.sprite = playSprite;
		} else 
		{
			AudioPlay ();
			videoPlayer.Play ();
			SetTotalTimeUI ();
			playImage.sprite = pauseSprite;
		}
	}

	public void LoadVideo(int numberObject)
	{
		TimerRunChecked =! TimerRunChecked; 
		filmUI.SetActive(true);
		cameraMain.SetActive (false);
		cameraFilm.SetActive (true);
		videoPlayer.clip = videoClips[numberObject];
		SetTotalTimeUI ();
		AudioPlay ();
		videoPlayer.Play();
		playImage.sprite = pauseSprite;
		fullscreen.DOAnchorPos(new Vector2(fullscreen.anchoredPosition.x, 90.0f),0.7f);
	}
		
	public void AudioPlay(){
		videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		videoPlayer.EnableAudioTrack(0, true);
		videoPlayer.SetTargetAudioSource(0, audioSource);
	}
	public void AspectChange(){
		DOTween.defaultTimeScaleIndependent = true;
		checkedAspect = !checkedAspect;
		if (checkedAspect == true) {
			fullscreen.DOAnchorPos(new Vector2(fullscreen.anchoredPosition.x, 25.0f),0.7f);
			mainUI.SetActive (false);
			videoPlayer.aspectRatio = VideoAspectRatio.Stretch;
		} else {
			fullscreen.DOAnchorPos(new Vector2(fullscreen.anchoredPosition.x, 90.0f),0.7f);
			videoPlayer.aspectRatio = VideoAspectRatio.NoScaling;
			mainUI.SetActive (true);
		}
	}
	void SetCurrentTimeUI()
	{
		string minutes = Mathf.Floor ((int)videoPlayer.time / 60).ToString ("00");
		string seconds = ((int)videoPlayer.time % 60).ToString ("00");
		currentMinutes.text = minutes;
		currentSeconds.text = seconds;
	}

	void SetTotalTimeUI()
	{
		string minutes = Mathf.Floor ((int)videoPlayer.clip.length / 60).ToString ("00");
		string seconds = ((int)videoPlayer.clip.length % 60).ToString ("00");
		totalMinutes.text = minutes;
		totalSeconds.text = seconds;
	}

	double CalculatePlayedFraction()
	{
		double fraction = (double)videoPlayer.frame / (double)videoPlayer.clip.frameCount;
		return fraction;
	}
}