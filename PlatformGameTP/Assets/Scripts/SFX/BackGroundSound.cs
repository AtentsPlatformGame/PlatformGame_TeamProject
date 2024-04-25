using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSound : MonoBehaviour
{
    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        if (SoundManager.Instance != null && myAudioSource != null)
            SoundManager.Instance.SetBGVolumeAct?.AddListener(SetBGAudioSourceVolume);
    }

    void SetBGAudioSourceVolume(float soundValue)
    {
        myAudioSource.volume = soundValue;
    }
}
