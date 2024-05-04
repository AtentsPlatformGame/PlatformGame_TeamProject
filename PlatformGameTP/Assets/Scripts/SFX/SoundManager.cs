using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager Instance;
    public GameObject Op;
    public Slider volumeSlider;
    public Slider bgVolumeSlider;
    public UnityEvent<Slider> SetVolumeAct; // 효과음 변경 함수 << 사운드매니저가 하는게 아니라 각자 개체들이 함
    public UnityEvent<Slider> SetBGVolumeAct; // 배경음 변경 함수 << 사운드매니저가 하는게 아니라 개체가 함
    public float soundValue = 1.0f;
    public float bgSoundValue = 1.0f;
    public AudioSource bgSound;
    public AudioClip[] bglist;
    public AudioMixer mixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    
    public void SetBGSVolume() // 배경음
    {
        bgSoundValue = bgVolumeSlider.value;
    }
    public void SetBGSVolume(float value) // 배경음
    {
        bgSoundValue = value;
    }
    public void SetVolume() // 효과음
    {
        soundValue = volumeSlider.value;
    }
    public void SetVolume(float value) // 효과음
    {
        soundValue = value;
    }
    public void SetBGVolumeSlider(Slider _slider)
    {
        bgVolumeSlider = _slider;
        SetBGVolumeAct?.Invoke(bgVolumeSlider);
    }

    public void SetSFXVolumeSlider(Slider _slider)
    {
        volumeSlider = _slider;
        SetVolumeAct?.Invoke(volumeSlider);
        Debug.Log("SetSFXSlider");
    }
}
