using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCanvas : MonoBehaviour
{
    public UnityEvent playerMoveTrueAct;
    public UnityEvent playerMoveFalseAct;

    [SerializeField] Transform portalTextImg;
    [SerializeField] Transform portalFadeImg;

    [SerializeField] float fadeTime = 3.0f;

    float curTime = 0.0f;
    CanvasGroup cg;
    Coroutine FadeCoroutine;
    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void TurnOnPortalTextImg()
    {
        if (portalTextImg != null) portalTextImg.gameObject.SetActive(true);
    }

    public void TurnOffPortalTextImg()
    {
        if (portalTextImg != null) portalTextImg.gameObject.SetActive(false);
    }

    public void TurnOnFadeImg()
    {
        if (portalFadeImg != null) portalFadeImg.gameObject.SetActive(true);
        
    }

    public void GoToEndScene()
    {
        if (FadeCoroutine != null)
        {
            StopAllCoroutines();
            FadeCoroutine = null;
        }

        TurnOffPortalTextImg();
        TurnOnFadeImg();
        playerMoveFalseAct?.Invoke();
        FadeCoroutine = StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        cg.alpha = 0;
        curTime = 0.0f;
        while (curTime <= fadeTime)
        {
            cg.alpha = Mathf.Lerp(0.0f, 1.0f, curTime / fadeTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 1.0f;

        yield return StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(3);
    }
}
