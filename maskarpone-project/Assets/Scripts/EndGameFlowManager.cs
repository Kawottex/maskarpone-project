using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[Serializable]
public struct SceneConsequence
{
    public ConsequenceSO m_consequence;
    public string m_scene;
}

public class EndGameFlowManager : MonoBehaviour
{
    [SerializeField]
    private List<ConsequenceSO> m_consequences;

    [SerializeField]
    private List<SceneConsequence> m_sceneConsequences;

    [SerializeField]
    private FlowManager m_flowManager;

    private void Start()
    {
        m_flowManager.RegisterEndGameFlowManager(this);
    }

    public string GetFinalSceneToLoad()
    {
        int max = 0;
        ConsequenceSO mostConsequence = null;
        foreach (ConsequenceSO cons in m_consequences)
        {
            if (cons.m_value > max)
            {
                mostConsequence = cons;
                max = cons.m_value;
            }
        }
        foreach (SceneConsequence sceneConsequence in m_sceneConsequences)
        {
            if (sceneConsequence.m_consequence == mostConsequence)
            {
                return sceneConsequence.m_scene;
            }
        }
        return null;
    }
}
