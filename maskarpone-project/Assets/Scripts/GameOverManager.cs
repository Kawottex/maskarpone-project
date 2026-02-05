using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private List<ConsequenceSO> m_consequences;

    private void Start()
    {
        StartCoroutine(ReloadGame());
    }

    private IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(4.0f);

        foreach (var consequence in m_consequences)
        {
            consequence.ResetValue();
        }

        SceneLoader.Instance.SwitchScene("GameOverScene", "Scene_intro");
    }
}
