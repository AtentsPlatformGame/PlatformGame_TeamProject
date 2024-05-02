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
    [Header("Æ÷Å»ÀÌµ¿ »ç¿îµå")]
    public AudioSource teleportSource;
    public AudioClip teleportClip;
    public GameObject InventoryCheck;

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
                    Debug.Log("È°¼ºÈ­");
                    Fade();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (Input.GetKeyDown(KeyCode.G)&& InventoryCheck.activeSelf == false)
            {

                Debug.Log("È°¼ºÈ­");
                Fade();
                if (teleportSource != null)
                {
                    teleportSource.clip = teleportClip ;
                    teleportSource.PlayOneShot(teleportClip);
                }
                if (teleportSource.isPlaying) Debug.Log("Æ÷Å» ½´½µ");
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
        time = 0f;
        Color alpha = Panel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;

        PlayerTeleport.gameObject.transform.position = ExternalPortal.gameObject.transform.position;
        LightChangeAct?.Invoke();
        yield return new WaitForSeconds(2.0f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
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
