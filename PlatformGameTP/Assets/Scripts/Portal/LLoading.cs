using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LLoading : MonoBehaviour
{
    public static LLoading instance;
    public string nextSceneName;
    public GameObject Loading;

    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        while (!asyncLoad.isDone)
        {
            // 진행 상황 업데이트
            // 예: 진행 상황을 보여주는 바 업데이트
            yield return null;
        }
    }
}
