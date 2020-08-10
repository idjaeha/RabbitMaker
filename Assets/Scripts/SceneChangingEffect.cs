using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 장면 전환 효과를 제공해주는 객체
/// </summary>
public class SceneChangingEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject sceneChangingCanvas;

    [Range(0.5f, 2.0f)]
    public float changingDuration = 1.0f; // 장면 전환 처리 시간

    void Start()
    {
        StartCoroutine(FadeIn());
    }
    public IEnumerator FadeIn()
    {
        CanvasGroup sceneChangingCanvasGroup = sceneChangingCanvas.GetComponentInChildren<CanvasGroup>();
        float finalAlpha = 0.0f;
        float fadeInSpeed = Mathf.Abs(sceneChangingCanvasGroup.alpha - finalAlpha) / changingDuration;

        while (!Mathf.Approximately(sceneChangingCanvasGroup.alpha, finalAlpha))
        {
            sceneChangingCanvasGroup.alpha = Mathf.MoveTowards(sceneChangingCanvasGroup.alpha, finalAlpha, fadeInSpeed * Time.deltaTime);
            yield return null;
        }

        SceneManager.UnloadSceneAsync("FadeInScene");
    }
}


