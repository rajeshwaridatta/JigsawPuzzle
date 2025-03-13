using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePopup : MonoBehaviour, IPopup
{
    protected CanvasGroup canvasGroup;
    public string popUpName;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        HideInstantly();
    }

    public virtual void Show(object data = null)
    {
        gameObject.SetActive(true);
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }
        StartCoroutine(FadeIn());
        OnShow(data);
    }

    public virtual void Hide()
    {
        StartCoroutine(FadeOut());
    }

    public bool IsActive => gameObject.activeSelf;
    protected abstract void OnShow(object data);

    private IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * 3; // Adjust speed
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 3;
            yield return null;
        }
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
        //  gameObject.SetActive(false);
    }

    protected void HideInstantly()
    {
        canvasGroup.alpha = 0;
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
        // gameObject.SetActive(false);
    }
}
