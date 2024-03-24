using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Tp : MonoBehaviour
{
    [SerializeField] Transform tp;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject countDown;
    
    public LayerMask mask;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //레이어마스크 검출
        if ((mask & 1 << other.gameObject.layer) != 0)
        {
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


