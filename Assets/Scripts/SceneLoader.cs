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


    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadScenesWithFadeInAsync(LoadSceneMode.Additive, "BackgroundScene", "UIScene"));
    }

    public IEnumerator LoadSceneAsync(LoadSceneMode mode, string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        // 마지막 Scene을 active 해주는 코드
        // Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        // SceneManager.SetActiveScene(loadedScene);
    }

    public IEnumerator LoadSceneWithFadeInAsync(LoadSceneMode mode, string sceneName)
    {
        yield return LoadSceneAsync(LoadSceneMode.Additive, "FadeInScene");
        yield return LoadSceneAsync(mode, sceneName);
    }

    public IEnumerator LoadScenesWithFadeInAsync(LoadSceneMode mode, params string[] sceneNames)
    {
        yield return LoadSceneAsync(LoadSceneMode.Additive, "FadeInScene");
        foreach (var sceneName in sceneNames)
        {
            yield return LoadSceneAsync(mode, sceneName);
        }

    }

    public void LoadScene(LoadSceneMode mode, string sceneName)
    {
        SceneManager.LoadScene(sceneName, mode);

        // 마지막 Scene을 active 해주는 코드
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
    }

    public void LoadSceneWithFadeIn(LoadSceneMode mode, string sceneName)
    {
        LoadScene(mode, sceneName);
        StartCoroutine(LoadSceneAsync(LoadSceneMode.Additive, "FadeInScene"));
    }

    public void LoadScenesWithFadeIn(LoadSceneMode mode, params string[] sceneNames)
    {
        foreach (var sceneName in sceneNames)
        {
            LoadScene(mode, sceneName);
        }
        StartCoroutine(LoadSceneAsync(LoadSceneMode.Additive, "FadeInScene"));

    }
}
