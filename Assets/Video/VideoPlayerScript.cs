using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip videoClip;

    void Start()
    {
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
    }
}
