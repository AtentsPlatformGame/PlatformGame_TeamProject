using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class JumpTrigger : MonoBehaviour
{
    [SerializeField] Transform gKeyPopUp; // G키 팝업창 오브젝트
    [SerializeField] Canvas portalCanvas; // 포탈 캔버스
    [SerializeField] Button portalButton; // 포탈 캔버스 안에 있는 버튼

    public UnityEvent jumpToTargetSceneAct; // 어느 씬으로 갈지 바인딩 해서 써야함, Jumper에서
    public LayerMask playerLayerMask; // 트리거 판정을 위한 레이어 마스크
    public Transform savePoint; // 특수방에 갔다 돌아올 때 이동할 위치

    Jumper jumper;
    private void Start()
    {
        jumper = FindObjectOfType<Jumper>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerLayerMask) != 0)
        {
            if (gKeyPopUp != null) gKeyPopUp.gameObject.SetActive(true); // 트리거에 플레이어가 닿으면 G키 팝업을 키고
            jumper.SetSavePoint(savePoint); // 특정 위치에 세이브포인트를 저장함
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerLayerMask) != 0)
        {
            if (gKeyPopUp.gameObject.activeSelf && Input.GetKeyDown(KeyCode.G))
            {
                if (portalCanvas != null)portalCanvas.gameObject.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & playerLayerMask) != 0)
        {
            if (gKeyPopUp != null && portalCanvas != null)
            {
                gKeyPopUp.gameObject.SetActive(false);
                portalCanvas.gameObject.SetActive(false);
            }
        }
    }

    
}
