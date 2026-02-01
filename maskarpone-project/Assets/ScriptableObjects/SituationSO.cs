using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SituationSO", menuName = "Scriptable Objects/SituationSO")]
public class SituationSO : ScriptableObject
{
    [SerializeField]
    private List<BoolSO> m_conditions = new List<BoolSO>();
    public List<BoolSO> Conditions { get => m_conditions; }

    [SerializeField]
    private PlaceSO m_place;
    public PlaceSO Place { get => m_place; }

    [SerializeField]
    private List<MaskSO> m_availableMasks = new List<MaskSO>();
    public List<MaskSO> AvailableMasks { get => m_availableMasks; }

    public bool HasConditions()
    {
        return Conditions.Count > 0;
    }

    public bool HasMetCondition()
    {
        foreach (BoolSO boolSo in m_conditions)
        {
            if (boolSo.m_value)
            {
                return true;
            }
        }
        return false;
    }
}
