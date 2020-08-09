using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장면 전환 효과를 제공해주는 객체
/// </summary>
public class SceneChangingEffect
{
    public IEnumerator FadeIn(GameObject sceneChangingCanvas, float changingDuration = 1.0f)
    {
        CanvasGroup sceneChangingCanvasGroup = sceneChangingCanvas.GetComponentInChildren<CanvasGroup>();
        float finalAlpha = 0.0f;
        float fadeInSpeed = Mathf.Abs(sceneChangingCanvasGroup.alpha - finalAlpha) / changingDuration;

        while (!Mathf.Approximately(sceneChangingCanvasGroup.alpha, finalAlpha))
        {
            sceneChangingCanvasGroup.alpha = Mathf.MoveTowards(sceneChangingCanvasGroup.alpha, finalAlpha, fadeInSpeed * Time.deltaTime);
            yield return null;
        }

        UnityEngine.GameObject.Destroy(sceneChangingCanvas);
    }
}


