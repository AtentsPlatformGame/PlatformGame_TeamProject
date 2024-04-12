using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BossEntrance : MonoBehaviour
{
    [SerializeField, Header("입구 관련")] Transform entranceObject;
    [SerializeField, Header("G키 팝업창")] Transform gKeyPopup;
    [SerializeField, Header("입장 여부 물어보는 캔버스")] Transform entranceCanvas;
    [SerializeField, Header("플레이어")] Transform player;
    [SerializeField, Header("플레이어 텔레포트 시키기")] Transform warpPoint;
    [SerializeField, Header("1관 보스")] Transform boss1;

    [Header("플레이어 움직임 제한하는 함수")] public UnityEvent playerMoveFalse;
    [Header("플레이어 움직이게 하는 함수")] public UnityEvent playerMoveTrue;

    public LayerMask playerMask;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerMask) != 0)
        {
            if(gKeyPopup != null)
            {
                gKeyPopup.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if(entranceCanvas != null)
                {
                    playerMoveFalse?.Invoke();
                    entranceCanvas.gameObject.SetActive(true);
                    gKeyPopup.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (gKeyPopup != null)
            {
                gKeyPopup.gameObject.SetActive(false);
            }
        }
    }

    public void EntranceBossRoom()
    {
        playerMoveTrue?.Invoke();
        player.position = warpPoint.position;
        boss1.gameObject.SetActive(true);
        entranceObject.gameObject.SetActive(false);
    }

    public void ExitCanvas()
    {
        playerMoveTrue?.Invoke();
        entranceCanvas.gameObject.SetActive(false);
    }
}
