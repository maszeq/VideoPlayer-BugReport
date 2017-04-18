using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TapTutorial : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    public VideoClip videoClip;

    void Start()
    {
        GameObject camera = GameObject.Find("HoloLensCamera");
        videoPlayer = camera.AddComponent<VideoPlayer>();
        audioSource = camera.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        videoPlayer.clip = videoClip;

        // Video settings
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        videoPlayer.aspectRatio = VideoAspectRatio.FitVertically;
        videoPlayer.targetCameraAlpha = 1.0F;
        videoPlayer.isLooping = false;

        // Audio settings
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.loopPointReached += EndReached;

        audioSource.Play();
        videoPlayer.Play();

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("End reached!");
        StartCoroutine(fadeOutVideo());
    }

    IEnumerator fadeOutVideo()
    {
        while (videoPlayer.targetCameraAlpha > 0.0f)
        {
            videoPlayer.targetCameraAlpha -= 0.01f;
            yield return null;
        }
    }

    IEnumerator fadeInVideo()
    {
        while (videoPlayer.targetCameraAlpha <= 0.0f)
        {
            videoPlayer.targetCameraAlpha += 0.01f;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Frame " + videoPlayer.frame);
    }
}
