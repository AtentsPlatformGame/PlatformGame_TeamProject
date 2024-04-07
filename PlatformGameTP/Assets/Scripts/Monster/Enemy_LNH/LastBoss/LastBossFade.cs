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
    CanvasGroup cg;
    Coroutine FadeCoroutine;
   

    // Start is called before the first frame update
    void Awake()
    {
        cg = GetComponent<CanvasGroup>();   
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

    IEnumerator FadeIn()
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

    IEnumerator FadeOut()
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
}
