using InventorySampleScene;
using LGH;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class Tp : MonoBehaviour
{
    [SerializeField] Transform tp;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject countDown;

    public Life life;
    public LayerMask mask;

    private void Start()
    {
        
    }

    private void Update()
    {
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //레이어마스크 검출
        if ((mask & 1 << other.gameObject.layer) != 0)
        {
            if(life != null && life.GetLife() > 0)
            // 라이프를 확인해서 그게 0이 아니면 텔포를 한다.
                StartCoroutine(Teleport());
        }

    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.1f);
        Player.transform.position = new Vector3(
            tp.transform.position.x,
            tp.transform.position.y,
            tp.transform.position.z);

        //if (countDown != null) //만약에 null 이 아니라면
        //countDown.SetActive(true); // 카운트다운 시작

        if (countDown.activeSelf == true)
        {
            countDown.SetActive(false);
        }
        else if (countDown.activeSelf == false)
        {
            countDown.SetActive(true);
        }
        //만약에 오브젝트가 꺼져있으면 키고, 켜져있으면 끈다.
        //
    }

}


