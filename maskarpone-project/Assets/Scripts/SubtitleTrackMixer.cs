using MPUIKIT;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public const float MAX_OPACITY = 0.55f;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TMP_Text textComponent = playerData as TMP_Text;
        
        if (textComponent == null)
        {
            return;
        }

        MPImage image = textComponent.transform.parent.GetComponentInChildren<MPImage>();
        Image separatorImage = image.transform.GetChild(0).GetComponent<Image>();
        TextMeshProUGUI narratorText = image.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        string textValue = string.Empty;
        float currentAlpha = 0;
        SubtitleBehaviour subtitleBehaviour = null;

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; ++i)
        {
            float inputWeight = playable.GetInputWeight(i);

            if (0 < inputWeight)
            {
                var inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);

                subtitleBehaviour = inputPlayable.GetBehaviour();
                textValue = GetDynamicText(inputPlayable.GetTime(), subtitleBehaviour);
                currentAlpha = inputWeight;

                break;
            }
        }

        textComponent.text = textValue;
        textComponent.color = new Color(1, 1, 1, currentAlpha);

        if (subtitleBehaviour != null)
        {
            narratorText.text = subtitleBehaviour.m_user;
        }

        Color color = image.color;
        color.a = currentAlpha * MAX_OPACITY;
        image.color = color;

        Color separatorColor = separatorImage.color;
        separatorColor.a = currentAlpha;
        separatorImage.color = separatorColor;

        Color narratorTextColor = narratorText.color;
        narratorTextColor.a = currentAlpha;
        narratorText.color = narratorTextColor;
    }

    private string GetDynamicText(double _time, SubtitleBehaviour _subtitleBehaviour)
    {
        if (_subtitleBehaviour.m_user == string.Empty)
        {
            return _subtitleBehaviour.m_text;
        }

        string res = "";

        double increment = 0;

        for (int i = 0; i < _subtitleBehaviour.m_text.Length; ++i)
        {
            char character = _subtitleBehaviour.m_text[i];
            res += _subtitleBehaviour.m_text[i];

            if (i != _subtitleBehaviour.m_text.Length - 1 && character == '\\')
            {
                ++i;
                res += _subtitleBehaviour.m_text[i];
            }

            increment += SubtitleClip.DELAY;

            if (_time < increment)
            {
                break;
            }
        }

        return res;
    }

}