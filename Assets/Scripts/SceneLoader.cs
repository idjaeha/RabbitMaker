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
    private Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();

    private void InitScenes()
    {
        loadScenes.Add("TestScene", LoadSceneMode.Additive);
        loadScenes.Add("WaitingScene", LoadSceneMode.Additive);
    }

    void Awake()
    {
        sceneChangingEffect = new SceneChangingEffect();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitScenes();
    }

    private IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    private void LoadSceneWithFadeIn(string sceneName, LoadSceneMode mode)
    {
        GameObject sceneChangingCanvas = Instantiate<GameObject>(sceneChangingCanvasPrefab);
        CanvasGroup sceneChangingCanvasGroup = sceneChangingCanvas.GetComponentInChildren<CanvasGroup>();

    }
}
