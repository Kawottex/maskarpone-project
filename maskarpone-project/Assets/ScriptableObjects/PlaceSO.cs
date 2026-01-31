using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaceSO", menuName = "Scriptable Objects/PlaceSO")]
public class PlaceSO : ScriptableObject
{
    [SerializeField]
    private string m_name;
    public string Name { get => m_name; }

    [SerializeField]
    private SceneAsset m_loadedScene;
    public SceneAsset LoadedScene { get => m_loadedScene; }
}
