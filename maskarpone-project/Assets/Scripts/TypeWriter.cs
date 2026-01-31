using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterText : MonoBehaviour
{
    public TMP_Text m_text;
    public float m_delay = 0.03f;

    Coroutine typingRoutine;

    public void Play(string _text)
    {
        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(TypeRoutine(_text));
    }

    public IEnumerator TypeRoutine(string _text)
    {
        m_text.text = "";

        foreach (char c in _text)
        {
            m_text.text += c;
            yield return new WaitForSeconds(m_delay);
        }
    }
}
