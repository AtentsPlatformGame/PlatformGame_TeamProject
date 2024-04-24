using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LightsIntensityController : MonoBehaviour
{
    public Transform GlobalLight;
    public LayerMask playerMask;
    public Transform globalPostProcessManager;
    public PostProcessProfile coolTonePostProcessProfile;
    public PostProcessProfile warmTonePostProcessProfile;
    public PostProcessProfile stage1Profile;
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

                increaseCT = StartCoroutine(IncreasingLightsIntensity());

            }
            else if (this.gameObject.tag == "DecreaseLights")
            {
                if (increaseCT != null)
                {
                    StopCoroutine(increaseCT);
                    increaseCT = null;
                }

                decreaseCT = StartCoroutine(DecreasingLightsIntensity());


            }
        }
    }

    public void StartIncreaseLights()
    {
        globalPostProcessManager.GetComponent<PostProcessVolume>().profile = warmTonePostProcessProfile;
        StartCoroutine(IncreasingLightsIntensity());
    }

    public void StartDecreaseLights()
    {
        globalPostProcessManager.GetComponent<PostProcessVolume>().profile = coolTonePostProcessProfile;
        StartCoroutine(DecreasingLightsIntensity());
    }


    IEnumerator IncreasingLightsIntensity()
    {
        float increaseDelay = 0.0f;
        float curIntensity = GlobalLight.GetComponent<Light>().intensity;
        /*ColorGrading colorGrading;
        stage1Profile.TryGetSettings(out colorGrading);
        float curTemp = colorGrading.temperature.value;*/
        while (increaseDelay <= changeLightDelay)
        {
            increaseDelay += Time.deltaTime;
            GlobalLight.GetComponent<Light>().intensity = Mathf.Lerp(curIntensity, 1f, increaseDelay / changeLightDelay);
            //colorGrading.temperature.value = Mathf.Lerp(curTemp, -15f, increaseDelay / changeLightDelay);
            yield return null;
        }
        GlobalLight.GetComponent<Light>().intensity = 1.0f;
        //globalPostProcessManager.GetComponent<PostProcessVolume>().profile = groundPostProcessProfile;
        yield return null;
    }

    IEnumerator DecreasingLightsIntensity()
    {
        float increaseDelay = 0.0f;
        float curIntensity = GlobalLight.GetComponent<Light>().intensity;
        /*ColorGrading colorGrading;
        stage1Profile.TryGetSettings(out colorGrading);
        float curTemp = colorGrading.temperature.value;*/
        while (increaseDelay <= changeLightDelay)
        {
            increaseDelay += Time.deltaTime;
            GlobalLight.GetComponent<Light>().intensity = Mathf.Lerp(curIntensity, 0.1f, increaseDelay / changeLightDelay);
            //colorGrading.temperature.value = Mathf.Lerp(curTemp, 15f, increaseDelay / changeLightDelay);
            yield return null;
        }
        GlobalLight.GetComponent<Light>().intensity = 0.1f;
        //globalPostProcessManager.GetComponent<PostProcessVolume>().profile = undergroundPostProcessProfile;
        yield return null;
    }
}
