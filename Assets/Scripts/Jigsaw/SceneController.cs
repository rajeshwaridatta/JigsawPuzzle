using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    private void OnEnable() { }
    private void OnDisable() { }

    public CanvasGroup fadeCanvasGroup; // Assign in Inspector
    public float fadeDuration = 1f; // Duration of the fade

    private void Start()
    {
        StartCoroutine(FadeIn()); 
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName)); 
    }

    private IEnumerator FadeIn()
    {
        float t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            fadeCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0;
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
