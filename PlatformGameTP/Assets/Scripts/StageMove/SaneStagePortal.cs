using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using InventorySystem;

public class SaneStagePortal : MonoBehaviour
{
    public UnityEvent LightChangeAct;
    public GameObject PlayerTeleport;
    public GameObject ExternalPortal;
    public LayerMask playerMask;
    public Image Panel;
    float time = 0f;
    float F_time = 1;
    GameObject obj;
    PlayerController player;
    public GameObject InventoryCheck;
    [Header("포탈이동 사운드")]
    public AudioSource teleportSource;
    public AudioClip teleportClip;

    [Header("배경음 플레이어")]
    public AudioSource bgmAudioSource;
    [Header("교체 할 배경음")]
    public AudioClip bgmClip;
    public int GetGKeyCount = 0;

    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (InventoryCheck.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Debug.Log("활성화");
                    Fade();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (Input.GetKeyDown(KeyCode.G)&& InventoryCheck.activeSelf == false && GetGKeyCount == 0)
            {
                GetGKeyCount = 1;
                Debug.Log("활성화");
                Fade();
                if (teleportSource != null)
                {
                    teleportSource.clip = teleportClip ;
                    teleportSource.PlayOneShot(teleportClip);
                }
                if (teleportSource.isPlaying) Debug.Log("포탈 슈슉");
            }
        }
   
    }

    public void Fade()
    {
        StartCoroutine(FadeInFlow());
        
    }
    
    IEnumerator FadeInFlow()
    {


        Panel.gameObject.SetActive(true);
        Stopmoving();
        float volume = 1.0f;
        if (bgmAudioSource != null) volume = bgmAudioSource.volume;
        time = 0f;
        Color alpha = Panel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            if (bgmAudioSource != null) bgmAudioSource.volume = Mathf.Lerp(volume, 0.0f, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;
        
        PlayerTeleport.gameObject.transform.position = ExternalPortal.gameObject.transform.position;
        LightChangeAct?.Invoke();
        if (bgmAudioSource != null && bgmClip != null)
        {
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.Play();
        }
        yield return new WaitForSeconds(2.0f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            if (bgmAudioSource != null) bgmAudioSource.volume = Mathf.Lerp(0.0f, volume, time);
            Panel.color = alpha;
            yield return null;
        }
        
        DoMoving();
        Panel.gameObject.SetActive(false);
        
        if (player != null)
        {
            if (player.GetCA_HpPenalty())
            {
                player.TakeDamage(-1f);
            }
        }


        yield return new WaitForSeconds(3.0f);
        GetGKeyCount = 0;
        yield return null;
    }
        
    void Stopmoving()
    {
        PlayerTeleport.GetComponent<PlayerController>().MoveFalse();
        
    }
    void DoMoving()
    {
        PlayerTeleport.GetComponent<PlayerController>().MoveTrue();

    }



}
