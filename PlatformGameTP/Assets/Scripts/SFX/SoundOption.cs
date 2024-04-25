using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public Slider bgVolumeSlider;
    public Slider volumeSlider;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.bgVolumeSlider = bgVolumeSlider;
            SoundManager.Instance.volumeSlider = volumeSlider;

            bgVolumeSlider.value = SoundManager.Instance.bgSoundValue;
            volumeSlider.value = SoundManager.Instance.soundValue;
        }   
    }

}
