using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class SaneStagePortal : MonoBehaviour
{
    public GameObject PlayerTeleport;
    public GameObject ExternalPortal;
    public LayerMask playerMask;
    public Image Panel;
    float time = 0f;
    float F_time = 1;
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {

            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("활성화");
                Fade();

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {

                Debug.Log("활성화");
                Fade();

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
