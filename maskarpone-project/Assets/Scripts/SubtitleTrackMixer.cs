using MPUIKIT;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public const float DELAY = 0.03f;
    public const float MAX_OPACITY = 0.7f;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TMP_Text textComponent = playerData as TMP_Text;
        
        if (textComponent == null)
        {
            return;
        }

        MPImage image = textComponent.transform.parent.GetComponentInChildren<MPImage>();

        string textValue = string.Empty;
        float currentAlpha = 0;

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; ++i)
        {
            float inputWeight = playable.GetInputWeight(i);

            if (0 < inputWeight)
            {
                var inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);

                SubtitleBehaviour subtitleBehaviour = inputPlayable.GetBehaviour();
                textValue = GetDynamicText(inputPlayable.GetTime(), subtitleBehaviour);
                currentAlpha = inputWeight;

                break;
            }
        }

        textComponent.text = textValue;
        textComponent.color = new Color(1, 1, 1, currentAlpha);

        Color color = image.color;
        color.a = currentAlpha * MAX_OPACITY;
        image.color = color;
    }

    private string GetDynamicText(double _time, SubtitleBehaviour _subtitleBehaviour)
    {
        string res = $"{_subtitleBehaviour.m_user}: ";

        double increment = 0;

        foreach (char c in _subtitleBehaviour.m_text)
        {
            res += c;
            increment += DELAY;

            if (_time < increment)
            {
                break;
            }
        }

        return res;
    }
}