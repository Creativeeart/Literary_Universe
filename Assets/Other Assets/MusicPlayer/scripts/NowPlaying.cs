using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NowPlaying : MonoBehaviour {

	public Text currentTime;
	public Text durationTime;
    public Slider musicLength;
	public HajiyevMusicManager _HajievMusicManager;

    void Update () {
		if (_HajievMusicManager.CurrentTrackNumber() >= 0) {
			string timeText = SecondsToMS(_HajievMusicManager.TimeInSeconds());
			string lengthText = SecondsToMS(_HajievMusicManager.LengthInSeconds());
			musicLength.value = _HajievMusicManager.TimeInSeconds();
			musicLength.maxValue = _HajievMusicManager.LengthInSeconds();
			currentTime.text = timeText;
			durationTime.text = lengthText;
        }
	}

    string SecondsToMS(float seconds) {
        return string.Format("{0:D2}:{1:D2}", ((int)seconds)/60, ((int)seconds)%60);
    }
}
