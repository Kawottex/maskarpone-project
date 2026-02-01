using System.Collections;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ReloadGame());
    }

    private IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(4.0f);
        SceneLoader.Instance.SwitchScene("GameOverScene", "Scene_intro");
    }
}
