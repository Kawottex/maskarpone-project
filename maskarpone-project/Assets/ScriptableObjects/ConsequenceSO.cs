using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsequenceSO", menuName = "Scriptable Objects/ConsequenceSO")]
public class ConsequenceSO : ScriptableObject
{
    [SerializeField]
    private int m_valueToAdd = 1;


    public int m_value { get; private set; } = 0;

    public void TriggerAddedValue()
    {
        m_value += m_valueToAdd;
    }

    internal void ResetValue()
    {
        m_value = 0;
    }
}
