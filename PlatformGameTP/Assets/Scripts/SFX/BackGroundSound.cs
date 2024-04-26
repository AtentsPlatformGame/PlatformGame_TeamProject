using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundSound : MonoBehaviour
{
    public AudioSource myAudioSource;
    Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        //myAudioSource = GetComponent<AudioSource>();
        if (SoundManager.Instance != null && myAudioSource != null)
        {
            myAudioSource.volume = SoundManager.Instance.bgSoundValue;
            SoundManager.Instance.SetBGVolumeAct.AddListener(SetVolumeSlider);

        }

    }

    public void SetBGAudioSourceVolume(float value)
    {
        myAudioSource.volume = value;
    }

    public void SetVolumeSlider(Slider _slider)
    {
        volumeSlider = _slider;
        Debug.Log("배경음 슬라이더 세팅");
        volumeSlider.onValueChanged.AddListener(SetBGAudioSourceVolume);
    }
}
