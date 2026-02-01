using System;
using System.Collections;
using UnityEditor;
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

    [SerializeField]
    private GameObject m_3DMaskPrefab = null;

    [SerializeField]
    private GameObject m_maskRoot = null;

    [SerializeField]
    private bool m_isBeforeFinalScene = false;

    private EndGameFlowManager m_endGameFlowManager = null;

    private void Start()
    {
        StartCoroutine(MainFlow());
    }

    private IEnumerator MainFlow()
    {
        m_playableDirector.Play();
        yield return new WaitWhile(() => m_playableDirector.state == PlayState.Playing);
        if (m_isBeforeFinalScene)
        {
            EndGameFlow();
        }
        else
        {
            DisplayMasks();
        }
    }

    private void DisplayMasks()
    {
        m_maskUIRoot.DisplayMasks(m_currentSituation.AvailableMasks);
    }

    public IEnumerator TriggerMaskSelected(MaskSO selectedMask)
    {
        foreach (ConsequenceSO consequence in selectedMask.Consequences)
        {
            consequence.TriggerAddedValue();
        }
        yield return Spawn3DMaskOnPlayer(selectedMask);
        m_playableDirector.playableAsset = selectedMask.Answer.TimelineToLoad;
        m_playableDirector.Play();
        yield return new WaitWhile(() => m_playableDirector.state == PlayState.Playing);
        SituationSO nextSituation = selectedMask.Answer.GetNextSituation();
        LoadNextSituationScene(nextSituation);
    }

    private IEnumerator Spawn3DMaskOnPlayer(MaskSO selectedMask)
    {
        GameObject spawnedMask = Instantiate(m_3DMaskPrefab);
        Renderer targetRenderer = spawnedMask.GetComponent<Renderer>();
        if (targetRenderer)
        {
            targetRenderer.material.mainTexture = selectedMask.Mask3DSprite.texture;
        }

        Vector3 pointAPos = Vector3.zero;
        Vector3 pointBPos = GetMaskFinalPos();

        Vector3 scaleA = Vector3.zero;
        Vector3 scaleB = new Vector3(500.0f, 500.0f, 500.0f);

        float duration = 2.0f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            spawnedMask.transform.position = Vector3.Lerp(pointAPos, pointBPos, timeElapsed / duration);
            spawnedMask.transform.localScale = Vector3.Lerp(scaleA, scaleB, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            pointBPos = GetMaskFinalPos();
            yield return null;
        }

        spawnedMask.transform.position = GetMaskFinalPos();
        spawnedMask.transform.SetParent(m_maskRoot.transform, true);
    }

    private Vector3 GetMaskFinalPos()
    {
        Vector3 pointBPos = m_maskRoot.transform.position;
        return pointBPos;
    }

    private void LoadNextSituationScene(SituationSO nextSituation)
    {
        string currentSceneName = m_currentSituation.Place.LoadedScene;
        string nextSceneName = nextSituation.Place.LoadedScene;
        SceneLoader.Instance.SwitchScene(currentSceneName, nextSceneName);
    }

    public void RegisterEndGameFlowManager(EndGameFlowManager endGameFlowManager)
    {
        m_endGameFlowManager = endGameFlowManager;
    }

    private void EndGameFlow()
    {
        string currentSceneName = m_currentSituation.Place.LoadedScene;
        string finalScene = m_endGameFlowManager.GetFinalSceneToLoad();
        SceneLoader.Instance.SwitchScene(currentSceneName, finalScene);
    }
}
