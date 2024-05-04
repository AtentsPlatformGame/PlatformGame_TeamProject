using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private void OnEnable()
    {
        if(SoundManager.Instance != null)
        {
            if(this.gameObject.tag == "BGSFXSlider")
            {
                SoundManager.Instance.SetBGVolumeSlider(this.GetComponent<Slider>());
                this.GetComponent<Slider>().value = SoundManager.Instance.bgSoundValue;
                this.GetComponent<Slider>().onValueChanged.AddListener(SoundManager.Instance.SetBGSVolume);
            }
            else if(this.gameObject.tag == "SFXSlider")
            {
                SoundManager.Instance.SetSFXVolumeSlider(this.GetComponent<Slider>());
                this.GetComponent<Slider>().value = SoundManager.Instance.soundValue;
                this.GetComponent<Slider>().onValueChanged.AddListener(SoundManager.Instance.SetVolume);
            }
                
        }
    }
}
