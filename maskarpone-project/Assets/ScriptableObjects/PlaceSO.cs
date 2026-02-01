using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaceSO", menuName = "Scriptable Objects/PlaceSO")]
public class PlaceSO : ScriptableObject
{
    [SerializeField]
    private string m_name;
    public string Name { get => m_name; }

    [SerializeField]
    private string m_loadedScene;
    public string LoadedScene { get => m_loadedScene; }
}
