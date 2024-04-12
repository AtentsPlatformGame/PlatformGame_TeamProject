using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LastBossFade : MonoBehaviour
{
    public UnityEvent playerMoveTrueAct;
    public UnityEvent playerMoveFalseAct;

    [SerializeField] float fadeTime = 1.0f;

    float curTime = 0.0f;
    [SerializeField] CanvasGroup cg;
    Coroutine FadeCoroutine;
   

    // Start is called before the first frame update
    void Awake()
    {

    }

    public void StartFadeIn()
    {
        if (FadeCoroutine != null)
        {
            StopAllCoroutines();
            FadeCoroutine = null;
        }
        playerMoveTrueAct?.Invoke();
        FadeCoroutine = StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        if (FadeCoroutine != null)
        {
            StopAllCoroutines();
            FadeCoroutine = null;
        }
        playerMoveFalseAct?.Invoke();
        FadeCoroutine = StartCoroutine(FadeOut());
    }

    public void StartFadeOutAndIn()
    {
        if (FadeCoroutine != null)
        {
            StopAllCoroutines();
            FadeCoroutine = null;
        }
        FadeCoroutine = StartCoroutine(FadingOut());
    }
    IEnumerator FadeOut()
    {
        curTime = 0.0f;
        while(curTime <= fadeTime)
        {
            cg.alpha = Mathf.Lerp(0.0f, 1.0f, curTime / fadeTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 1.0f;
    }

    IEnumerator FadeIn()
    {
        curTime = 0.0f;
        while (curTime <= fadeTime)
        {
            cg.alpha = Mathf.Lerp(1.0f, 0.0f, curTime / fadeTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 0.0f;
    }

    IEnumerator FadingOut()
    {
        playerMoveFalseAct?.Invoke();
        curTime = 0.0f;
        while (curTime <= fadeTime)
        {
            cg.alpha = Mathf.Lerp(0.0f, 1.0f, curTime / fadeTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 1.0f;
        yield return StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn()
    {
        curTime = 0.0f;
        while (curTime <= fadeTime)
        {
            cg.alpha = Mathf.Lerp(1.0f, 0.0f, curTime / fadeTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 0.0f;
        playerMoveTrueAct?.Invoke();
    }
}
