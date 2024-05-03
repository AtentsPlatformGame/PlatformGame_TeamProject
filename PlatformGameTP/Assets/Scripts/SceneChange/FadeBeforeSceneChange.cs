using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeBeforeSceneChange : MonoBehaviour
{
    public CanvasGroup cg;
    public AudioSource bgSFX;
    public float fadeTime;
    float curTime = 0.0f;
    // Start is called before the first frame update

    private void Start()
    {
        if (SceneChanger.instance != null)
            SceneChanger.instance.sceneChangeAct.AddListener(StartFadeIn);
    }

    public void StartFadeIn( )
    {
        StartCoroutine(FadeInBeforeSceneChange());
    }

    IEnumerator FadeInBeforeSceneChange()
    {
        curTime = 0.0f;
        float volume = bgSFX.volume;
        while (curTime <= fadeTime)
        {
            cg.alpha = Mathf.Lerp(0.0f, 1.0f, curTime / fadeTime);
            if(bgSFX != null)bgSFX.volume = Mathf.Lerp(volume, 0.0f, curTime / fadeTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 1.0f;

        SceneManager.LoadScene("LoadingScene");

    }
}
