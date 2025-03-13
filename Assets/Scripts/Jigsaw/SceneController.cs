using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneController : Singleton<SceneController>
{
    public static event Action<string> OnSceneLoaded;
    private void OnEnable() { }
    private void OnDisable() { }

    private CanvasGroup fadeCanvasGroup; 
    public float fadeDuration = 1f; 

    private void Start()
    {
        fadeCanvasGroup = GameObject.FindGameObjectWithTag(Constants.SceneLoaderTagName).GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn()); 
    }

    public void LoadNextScene(string sceneName)
    {
        fadeCanvasGroup = GameObject.FindGameObjectWithTag(Constants.SceneLoaderTagName).GetComponent<CanvasGroup>();

        StartCoroutine(FadeOut(sceneName));
        OnSceneLoaded.Invoke(sceneName);
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
        LoadScene(sceneName);
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
       

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; 

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
          

            if (operation.progress >= 0.9f) 
            {
                yield return new WaitForSeconds(1f); 
                operation.allowSceneActivation = true; 
            }

            yield return null;
        }
    }
}
