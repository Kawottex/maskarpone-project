using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "AnswerSO", menuName = "Scriptable Objects/AnswerSO")]
public class AnswerSO : ScriptableObject
{
    [SerializeField]
    private TimelineAsset m_timelineToLoad;
    public TimelineAsset TimelineToLoad { get => m_timelineToLoad; }

    [SerializeField]
    private List<SituationSO> m_nextSituationList = new List<SituationSO>();
    public List<SituationSO> NextSituationList { get => m_nextSituationList; }

    public SituationSO GetNextSituation()
    {
        SituationSO noConditionSituation = null;

        foreach (SituationSO situation in m_nextSituationList)
        {
            if (situation.HasConditions() && situation.HasMetCondition())
            {
                return situation;
            }
            else if (!situation.HasConditions())
            {
                noConditionSituation = situation;
            }
        }
        return noConditionSituation;
    }
}
