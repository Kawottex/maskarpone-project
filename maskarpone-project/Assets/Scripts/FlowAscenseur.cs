using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FlowAscenseur : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector m_playableAscenseurIntro = null!;

    [SerializeField]
    private SceneAsset m_liftScene;

    [SerializeField]
    private SceneAsset m_introScene;

    private void Awake()
    {
        Assert.IsNotNull(m_playableAscenseurIntro);

        StartCoroutine(FlowAsync());
    }

    private IEnumerator FlowAsync()
    {
        m_playableAscenseurIntro.Play();

        yield return new WaitWhile(() => m_playableAscenseurIntro.state == PlayState.Playing);

        SceneLoader.Instance.SwitchScene(m_introScene.name, m_liftScene.name);
    }
}
