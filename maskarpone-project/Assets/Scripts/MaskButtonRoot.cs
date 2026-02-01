using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaskButtonRoot : MonoBehaviour
{

    [SerializeField]
    private Button m_button;
    public Button MainButton { get => m_button; }

    [SerializeField]
    private TextMeshProUGUI m_maskNameText;
    public TextMeshProUGUI MaskNameText { get => m_maskNameText; }

    [SerializeField]
    private Image m_maskSprite;
    public Image MaskSprite { get => m_maskSprite; }
}
