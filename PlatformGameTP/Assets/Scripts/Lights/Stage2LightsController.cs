using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Stage2LightsController : MonoBehaviour
{
    public Transform GlobalLight;
    public LayerMask playerMask;
    public Transform globalPostProcessManager;
    [Header("밝은 분위기로 바꿀 프로파일")]public PostProcessProfile IncreaseTemperatureProfile;
    [Header("어두운 분위기로 바꿀 프로파일")] public PostProcessProfile DecreaseTemperatureProfile;
    [Header("밝히고자 하는 라이트 인텐시티 값")]public float increaseIntensityValue;
    [Header("어둡게 하고자 하는 라이트 인텐시티 값")] public float decreaseIntensityValue;
    public float changeLightDelay;

    Coroutine increaseCT;
    Coroutine decreaseCT;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (this.gameObject.tag == "IncreaseLights")
            {
                if (decreaseCT != null)
                {
                    StopCoroutine(decreaseCT);
                    decreaseCT = null;
                }

                increaseCT = StartCoroutine(IncreasingLightsIntensity(IncreaseTemperatureProfile));

            }
            else if (this.gameObject.tag == "DecreaseLights")
            {
                if (increaseCT != null)
                {
                    StopCoroutine(increaseCT);
                    increaseCT = null;
                }

                decreaseCT = StartCoroutine(DecreasingLightsIntensity(DecreaseTemperatureProfile));


            }
        }
    }

    public void StartIncreaseLights()
    {
        StartCoroutine(IncreasingLightsIntensity(IncreaseTemperatureProfile));
    }

    public void StartDecreaseLights()
    {
        StartCoroutine(DecreasingLightsIntensity(DecreaseTemperatureProfile));
    }


    IEnumerator IncreasingLightsIntensity(PostProcessProfile _profile)
    {
        globalPostProcessManager.GetComponent<PostProcessVolume>().profile = _profile;
        float increaseDelay = 0.0f;
        float curIntensity = GlobalLight.GetComponent<Light>().intensity;
        
        while (increaseDelay <= changeLightDelay)
        {
            increaseDelay += Time.deltaTime;
            GlobalLight.GetComponent<Light>().intensity = Mathf.Lerp(curIntensity, increaseIntensityValue, increaseDelay / changeLightDelay);
            yield return null;
        }
        GlobalLight.GetComponent<Light>().intensity = increaseIntensityValue;
        yield return null;
    }

    IEnumerator DecreasingLightsIntensity(PostProcessProfile _profile)
    {
        globalPostProcessManager.GetComponent<PostProcessVolume>().profile = _profile;
        float increaseDelay = 0.0f;
        float curIntensity = GlobalLight.GetComponent<Light>().intensity;
        while (increaseDelay <= changeLightDelay)
        {
            increaseDelay += Time.deltaTime;
            GlobalLight.GetComponent<Light>().intensity = Mathf.Lerp(curIntensity, decreaseIntensityValue, increaseDelay / changeLightDelay);
            yield return null;
        }
        GlobalLight.GetComponent<Light>().intensity = decreaseIntensityValue;
        yield return null;
    }
}
