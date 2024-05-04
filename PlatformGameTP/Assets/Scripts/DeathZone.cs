using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public Transform StartPos;
    public LayerMask playerMask;

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerMask)!= 0)
        {
            BattleSystem bs = player.GetComponent<BattleSystem>();
            if(bs != null)
            {
                if (player.GetComponent<PlayerController>().GetResurrectionOneTime())
                {
                    if(StartPos != null) player.transform.position = StartPos.transform.position;
                }
                bs.TakeDamage(100f);
                playerCam.GetComponent<CinemachineVirtualCamera>().Follow = null;
            }
        }
    }
}
