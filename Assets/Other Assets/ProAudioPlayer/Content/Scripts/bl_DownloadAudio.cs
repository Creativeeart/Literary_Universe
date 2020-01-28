using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class bl_DownloadAudio : MonoBehaviour {

    public bl_AudioPlayer m_APlayer = null;
    public List<bl_APAudioWeb> AudioURLs = new List<bl_APAudioWeb>();
    public bool PlayOnLoad = true;
    [Space(5)]
    [Header("UI")]
    public GameObject RootDownloadUI = null;
    public Slider DownloadProgress = null;
    public Text DownloadText = null;

    private int CurrentDownload = -1;
    private bool isDownloading = false;

    /// <summary>
    /// 
    /// </summary>
    void CheckToDownload()
    {
        if (isDownloading)
            return;
        if(AudioURLs.Count <= 0)
            return;

        if (CurrentDownload + 1 <= AudioURLs.Count - 1)
        {
            CurrentDownload++;
        }
        else { RootDownloadUI.SetActive(false);  return; }

        StartCoroutine(DownLoadAudio(AudioURLs[CurrentDownload].URL,AudioURLs[CurrentDownload].AudioTitle));
    }
    /// <summary>
    /// Download all audio in list 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IEnumerator DownLoadAudio(string url,string title)
    {    
        isDownloading = true;
        WWW www = new WWW(url);
        //check if url have a response (audio)
        if (www.error != null)
        {
            Debug.LogWarning(www.error);
            DownloadText.text = www.error;
            isDownloading = false;
            yield return null;
        }
        if (m_APlayer.m_Source.clip == null)
        {
            m_APlayer.m_Source.clip = www.GetAudioClip();
        }
        //while downloading
        while (!www.isDone)
        {
            if (DownloadProgress != null)
            {
                DownloadProgress.value = www.progress;
            }
            DownloadText.text = "Downloading " + title +" "+ (www.progress * 100).ToString("00") +"%..." ;
            //stop in bucle for update progress
            yield return null;
        }
        //create a new audio
        AudioClip c = new AudioClip();
        //when download is donw
        if (www.isDone || www.progress == 1)
        {
            c = www.GetAudioClip();
            c.name = AudioURLs[CurrentDownload].AudioTitle;

            //Just play when load, if not playing and not have a clip 
            if (!m_APlayer.m_Source.isPlaying)
            {
                m_APlayer.NewClip(c);
            }
            //add to play list

            m_APlayer.m_Clip.Add(c);
            if(bl_APlayList.OnNewClip != null)
            bl_APlayList.OnNewClip(c);
        }
       
        //cache the current download audio
        AudioURLs[CurrentDownload].CacheAudio = c;
        DownloadText.text = "Done!";
        //if play on load
        if (PlayOnLoad && !m_APlayer.m_Source.isPlaying) { m_APlayer.PlayPause(); }
        isDownloading = false;
        AudioURLs[CurrentDownload].isDownloaded = true;
        yield return new WaitForSeconds(0.5f);
        //go to download the next audio if exist.
        CheckToDownload();
    }
    /// <summary>
    /// 
    /// </summary>
    public void StartDownload() { CheckToDownload(); }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator DisabledInTime(GameObject obj, float t)
    {
        yield return new WaitForSeconds(t);
        obj.SetActive(false);
    }
}