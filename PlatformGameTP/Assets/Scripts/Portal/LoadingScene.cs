using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene instance;
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
            // ���� ��Ȳ ������Ʈ
            // ��: ���� ��Ȳ�� �����ִ� �� ������Ʈ
            yield return null;
        }
    }
}