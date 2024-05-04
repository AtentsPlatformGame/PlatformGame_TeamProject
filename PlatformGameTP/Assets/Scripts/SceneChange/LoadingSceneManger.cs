using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3.0f);
        //SceneManager.LoadScene(SceneChanger.instance.nextSceneName);

        AsyncOperation async = SceneManager.LoadSceneAsync(SceneChanger.instance.nextSceneName);

        yield return async;
    }
}
