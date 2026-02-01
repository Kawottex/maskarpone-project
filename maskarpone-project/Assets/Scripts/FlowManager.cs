using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FlowManager : MonoBehaviour
{
    [SerializeField]
    private SituationSO m_currentSituation;

    [SerializeField]
    private PlayableDirector m_playableDirector = null!;

    [SerializeField]
    private MaskUIRoot m_maskUIRoot = null;

    private void Start()
    {
        StartCoroutine(MainFlow());
    }

    private IEnumerator MainFlow()
    {
        m_playableDirector.Play();
        yield return new WaitWhile(() => m_playableDirector.state == PlayState.Playing);
        DisplayMasks();
    }

    private void DisplayMasks()
    {
        m_maskUIRoot.DisplayMasks(m_currentSituation.AvailableMasks);
    }

    public IEnumerator TriggerMaskSelected(MaskSO selectedMask)
    {
        m_playableDirector.playableAsset = selectedMask.Answer.TimelineToLoad;
        m_playableDirector.Play();
        yield return new WaitWhile(() => m_playableDirector.state == PlayState.Playing);
        SituationSO nextSituation = selectedMask.Answer.GetNextSituation();
        StartCoroutine(LoadNextSituationScene(nextSituation));
    }

    private IEnumerator LoadNextSituationScene(SituationSO nextSituation)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSituation.Place.LoadedScene.name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
