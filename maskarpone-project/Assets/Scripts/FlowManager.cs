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

    [SerializeField]
    private GameObject m_3DMaskPrefab = null;

    [SerializeField]
    private GameObject m_miiObject = null;

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
        spawnedMask.transform.localScale = new Vector3(0, 0, 0);
        spawnedMask.GetComponent<Animator>().SetTrigger("TriggerMovement");
        Renderer targetRenderer = spawnedMask.GetComponent<Renderer>();
        if (targetRenderer)
        {
            targetRenderer.material.mainTexture = selectedMask.Mask3DSprite.texture;
        }

        yield return new WaitForSeconds(3.0f);
        spawnedMask.GetComponent<Animator>().enabled = false;
        spawnedMask.transform.SetParent(m_miiObject.transform, true);

        // fix degueux, pas le temps d'investiguer plus
        spawnedMask.transform.position = new Vector3(spawnedMask.transform.position.x, spawnedMask.transform.position.y, spawnedMask.transform.position.z + 0.5f);
    }

    private void LoadNextSituationScene(SituationSO nextSituation)
    {
        string currentSceneName = m_currentSituation.Place.LoadedScene.name;
        string nextSceneName = nextSituation.Place.LoadedScene.name;
        SceneLoader.Instance.SwitchScene(currentSceneName, nextSceneName);
    }
}
