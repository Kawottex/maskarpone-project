using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;

public class FlowAscenseur : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector m_playableAscenseurIntro = null!;

    [SerializeField]
    private TypewriterText m_typewriterText = null!;

    private void Awake()
    {
        Assert.IsNotNull(m_playableAscenseurIntro);
        Assert.IsNotNull(m_typewriterText);

        StartCoroutine(FlowAsync());
    }

    private IEnumerator FlowAsync()
    {
        m_playableAscenseurIntro.Play();

        yield return new WaitUntil(() => m_playableAscenseurIntro.playableGraph.IsPlaying());
        yield return new WaitWhile(() => m_playableAscenseurIntro.playableGraph.IsPlaying());
    }
}
