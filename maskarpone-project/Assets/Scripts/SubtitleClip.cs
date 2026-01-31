using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SubtitleClip : PlayableAsset
{
    public const float DELAY = 0.03f;
    public const float MIN_DURATION = 1;

    public string m_user;
    public string m_text;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

        SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();
        subtitleBehaviour.m_user = m_user;
        subtitleBehaviour.m_text = m_text;
        
        return playable;
    }
}