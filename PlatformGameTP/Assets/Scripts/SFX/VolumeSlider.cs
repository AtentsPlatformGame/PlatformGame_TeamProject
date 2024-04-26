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
                SoundManager.Instance.SetBGVolumeSlider(this.GetComponent<Slider>());
            else if(this.gameObject.tag == "SFXSlider")
                SoundManager.Instance.SetSFXVolumeSlider(this.GetComponent<Slider>());
        }
    }
}
