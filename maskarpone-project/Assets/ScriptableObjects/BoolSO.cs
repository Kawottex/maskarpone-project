using UnityEngine;

[CreateAssetMenu(fileName = "BoolSO", menuName = "Scriptable Objects/BoolSO")]
public class BoolSO : ScriptableObject
{
    public bool m_value { get; private set; }

    [SerializeField]
    private bool ValueToApply = false;

    public void ApplyValue()
    {
        m_value = ValueToApply;
    }
}
