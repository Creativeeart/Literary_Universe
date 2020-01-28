using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderVideo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public WorldSpaceVideo worldSpaceVideo;
    private Slider progress;
    bool slide = false;
    private void Awake()
    {
        progress = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        slide = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        float frame = progress.value * worldSpaceVideo.videoPlayer.frameCount;
        worldSpaceVideo.videoPlayer.frame = (long)frame;
        slide = false;
    }

    private void Update()
    {
        if (!slide && worldSpaceVideo.videoPlayer.isPlaying)
        {
            progress.value = worldSpaceVideo.videoPlayer.frame / (float)worldSpaceVideo.videoPlayer.frameCount;
        }
    }

}

    
