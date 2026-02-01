using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaskSO", menuName = "Scriptable Objects/MaskSO")]
public class MaskSO : ScriptableObject
{
    [SerializeField]
    private Sprite m_maskIcon;
    public Sprite MaskIcon { get => m_maskIcon; }

    [SerializeField]
    private string m_maskName;
    public string MaskName { get => m_maskName; }

    [SerializeField]
    private Sprite m_mask3DSprite;
    public Sprite Mask3DSprite { get => m_mask3DSprite; }

    [SerializeField]
    private List<ConsequenceSO> m_consequences = new List<ConsequenceSO>();
    public List<ConsequenceSO> Consequences { get => m_consequences; }

    [SerializeField]
    private AnswerSO m_answer;
    public AnswerSO Answer { get => m_answer; }
}
