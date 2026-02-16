using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SubtitleController : MonoBehaviour
{
    public PlayableDirector Director { get; set; }

    private TimelineAsset TimelineAsset { get; set; }
    private SubtitleTrack SubtitleTrack { get; set; }

    private TimelineClip CurrentClip { get; set; }
    private bool WaitingForInput { get; set; } = false;

    private void Start()
    {
        TimelineAsset = Director.playableAsset as TimelineAsset;

        if (TimelineAsset != null)
        {
            foreach (var track in TimelineAsset.GetOutputTracks())
            {
                if (track is not SubtitleTrack subtitleTrack)
                {
                    continue;
                }

                SubtitleTrack = subtitleTrack;
                return;
            }
        }
    }

    private void Update()
    {
        if (SubtitleTrack == null)
        {
            return;
        }

        GetCurrentSubtitle();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (WaitingForInput)
            //{
            //    WaitingForInput = false;
            //    m_director.Play();
            //}
            //else 
            if (CurrentClip != null)
            {
                Director.time = CurrentClip.end;
            }
        }
    }

    private void GetCurrentSubtitle()
    {
        double time = Director.time;

        foreach (TimelineClip clip in SubtitleTrack.GetClips())
        {
            if (clip.asset is not SubtitleClip)
            {
                continue;
            }

            if (clip.start <= time && time <= clip.end)
            {
                CurrentClip = clip;
            }
        }
    }

    private void DetectEndOfSubtitle()
    {
        if (WaitingForInput)
        {
            return;
        }

        double time = Director.time;

        foreach (TimelineClip clip in SubtitleTrack.GetClips())
        {
            SubtitleClip subtitleClip = (SubtitleClip)clip.asset;
            if (clip.start <= time && time <= clip.end)
            {
                CurrentClip = clip;
            }

            if (clip.end <= time && time <= clip.end + 0.05f)
            {
                CurrentClip = clip;
                WaitingForInput = true;
                Director.Pause();
                return;
            }
        }
    }

    private void GoToNextClip(SubtitleClip _currentSubtitleClip)
    {
        Assert.IsNotNull(CurrentClip);

        double currentTime = Director.time;
        double nextClipTime = double.MaxValue;

        foreach (var clip in SubtitleTrack.GetClips())
        {
            if (clip.start > currentTime && clip.start < nextClipTime)
            {
                nextClipTime = clip.start;
            }
        }

        if (nextClipTime != double.MaxValue)
        {
            WaitingForInput = false;
            Director.time = nextClipTime;
            Director.Play();
        }
    }
}
