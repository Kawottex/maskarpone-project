using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string m_firstMapToLoad;

    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(LoadFirstMap());
    }

    private IEnumerator LoadFirstMap()
    {
        if (SceneManager.loadedSceneCount <= 1)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_firstMapToLoad, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }

    public void SwitchScene(string sceneToUnload, string sceneToLoad)
    {
        StartCoroutine(SwitchSceneAsync(sceneToUnload, sceneToLoad));
    }

    private IEnumerator SwitchSceneAsync(string sceneToUnload, string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneToUnload);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
