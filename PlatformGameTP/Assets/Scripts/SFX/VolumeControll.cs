using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControll : MonoBehaviour
{
    public AudioSource myAudioSource;

    private void OnEnable()
    {
        myAudioSource.volume = SoundManager.Instance.soundValue;
    }
}
