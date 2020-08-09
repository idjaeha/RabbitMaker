using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene들에 대한 정보를 갖고 Scene들을 불러오는 역할을 하는 객체
/// Scene이 불러질 때 효과 또한 이 곳에서 관리한다.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [Range(0.5f, 2.0f)]
    public float changingDuration = 1.0f; // 장면 전환 처리 시간
    private SceneChangingEffect sceneChangingEffect;

    [SerializeField]
    private GameObject sceneChangingCanvasPrefab; // Scene 전환 효과를 담당하는 Canvas prefab

    void Awake()
    {
        sceneChangingEffect = new SceneChangingEffect();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScenesWithFadeIn(LoadSceneMode.Additive, "UIScene", "BackgroundScene"));
    }

    private IEnumerator LoadScene(LoadSceneMode mode, string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    private IEnumerator LoadSceneWithFadeIn(LoadSceneMode mode, string sceneName)
    {
        GameObject sceneChangingCanvas = Instantiate<GameObject>(sceneChangingCanvasPrefab);

        yield return LoadScene(mode, sceneName);
        yield return StartCoroutine(sceneChangingEffect.FadeIn(sceneChangingCanvas, changingDuration));
    }

    private IEnumerator LoadScenesWithFadeIn(LoadSceneMode mode, params string[] sceneNames)
    {
        GameObject sceneChangingCanvas = Instantiate<GameObject>(sceneChangingCanvasPrefab);

        foreach (var sceneName in sceneNames)
        {
            yield return LoadScene(mode, sceneName);
        }

        yield return StartCoroutine(sceneChangingEffect.FadeIn(sceneChangingCanvas, changingDuration));
    }
}
