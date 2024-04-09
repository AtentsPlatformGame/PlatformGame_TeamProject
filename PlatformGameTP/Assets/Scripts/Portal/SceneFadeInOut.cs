using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;
    private bool isFading = false;

    void Start()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;
        FadeIn();
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndLoadSceneCoroutine(sceneName));
        }
    }

    private IEnumerator FadeOutAndLoadSceneCoroutine(string sceneName)
    {
        isFading = true;
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    private void FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float alpha = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        isFading = false;
    }
}
