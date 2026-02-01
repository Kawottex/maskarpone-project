using UnityEngine;

[CreateAssetMenu(fileName = "ConsequenceSO", menuName = "Scriptable Objects/ConsequenceSO")]
public class ConsequenceSO : ScriptableObject
{
    [SerializeField]
    private int m_valueToAdd = 1;

    private int m_value = 0;

    public void TriggerAddedValue()
    {
        m_value += m_valueToAdd;
    }
}
