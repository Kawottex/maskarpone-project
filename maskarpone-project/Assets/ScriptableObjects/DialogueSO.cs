using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [SerializeField]
    private string m_speakerName;
    public string SpeakerName { get => m_speakerName; }

    [SerializeField]
    private string m_dialogueText;
    public string DialogueText { get => m_dialogueText; }

    [SerializeField]
    private bool m_isSpeakerMainCharacter;
    public bool IsSpeakerMainCharacter { get => m_isSpeakerMainCharacter; }
}
