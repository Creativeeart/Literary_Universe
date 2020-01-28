using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoController : MonoBehaviour {
	public VideoClip[] videoClips;
	public VideoPlayer video;
	public Slider slider;
	bool isDone;
	public bool isPlaying{
		get{ return video.isPlaying;}
	}
	public bool isLooping{
		get{ return video.isLooping;}
	}
	public bool isPrepared{
		get{ return video.isPrepared;}
	}
	public bool IsDone{
		get{ return isDone;}
	}
	public double Time{
		get{ return video.time; }
	}
	public ulong Duration{
		get{ return (ulong)(video.frameCount / video.frameRate);}
	}
	public double NTime{
		get{ return Time / Duration;}
	}
	void OnEnable(){
		video.errorReceived += errorReceived;
		video.frameReady += frameReady;
		video.loopPointReached += loopPointReached;
		video.prepareCompleted += prepareCompleted;
		video.seekCompleted += seekCompleted;
		video.started += started;
	}
	void OnDisable(){
		video.errorReceived -= errorReceived;
		video.frameReady -= frameReady;
		video.loopPointReached -= loopPointReached;
		video.prepareCompleted -= prepareCompleted;
		video.seekCompleted -= seekCompleted;
		video.started -= started;
	}
	void errorReceived(VideoPlayer v, string msg){
		Debug.Log ("Video player error:" + msg);
	}
	void frameReady(VideoPlayer v, long frame){

	}
	void loopPointReached(VideoPlayer v){
		Debug.Log ("Video player point loop reached");
		isDone = true;
	}
	void prepareCompleted(VideoPlayer v){
		Debug.Log ("Video player finished preparing");
		isDone = false;
	}
	void seekCompleted(VideoPlayer v){
		Debug.Log ("Video player finished seeking");
		isDone = false;
	}
	void started(VideoPlayer v){
		Debug.Log ("Video player started");
	}
	void Update(){
		if (!isPrepared) return;
		slider.value = (float)NTime;
	}
	public void LoadVideo(int numberObject){
		video.clip = videoClips [numberObject]; 
		video.Prepare ();
		Debug.Log ("can set direct audio volume: " + video.canSetDirectAudioVolume);
		Debug.Log ("can set playback speed: " + video.canSetPlaybackSpeed);
		Debug.Log ("can set skip on drop: " + video.canSetSkipOnDrop);
		Debug.Log ("can set time: " + video.canSetTime);
		Debug.Log ("can step " + video.canStep);
	}
	public void PlayVideo(){
		if (!isPrepared) return;
		video.Play ();
	}
	public void PauseVideo(){
		if (!isPrepared) return;
		video.Pause ();
	}
	public void RestartVideo(){
		if (!isPrepared) return;
		PauseVideo ();
		Seek (0);
	}
	public void LoopVideo(bool toogle){
		if (!isPrepared) {
			return;
		}
		toogle = !toogle;
		video.isLooping = toogle;
	}
	public void Seek(float nTime){
		if (!video.canSetTime)
			return;
		if (!isPrepared) return;
		nTime = Mathf.Clamp (nTime, 0, 1);
		video.time = nTime * Duration;
	}
	public void IncrementPlaybackSpeed(){
		if (!video.canSetPlaybackSpeed)
			return;
		video.playbackSpeed += 1;
		video.playbackSpeed = Mathf.Clamp (video.playbackSpeed, 0, 10);
	}
	public void DecrementPlaybackSpeed(){
		if (!video.canSetPlaybackSpeed)
			return;
		video.playbackSpeed -= 1;
		video.playbackSpeed = Mathf.Clamp (video.playbackSpeed, 0, 10);
	}
}
