using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager Instance;
    public Slider volumeSlider;
    public Slider bgVolumeSlider;
    public UnityEvent<float> SetVolumeAct; // 효과음 변경 함수 << 사운드매니저가 하는게 아니라 각자 개체들이 함
    public UnityEvent<float> SetBGVolumeAct; // 배경음 변경 함수 << 사운드매니저가 하는게 아니라 개체가 함
    public float soundValue = 1.0f; // 효과음 값
    public float bgSoundValue = 1.0f; // 배경음 값
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

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length);
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
        SetBGVolumeAct?.Invoke(bgSoundValue);
    }
    public void SetVolume() // 효과음
    {
        soundValue = volumeSlider.value;
        SetVolumeAct?.Invoke(soundValue);
    }
}
